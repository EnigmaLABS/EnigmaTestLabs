﻿using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;

using ZmLabsObjects;
using ZmLabsObjects.sqltests;
using ZmLabsObjects.contracts;

using ZmLabsBusiness.functions;
using ZmLabsBusiness.data.contracts;

using ZMLabsData;
using ZMLabsData.contracts;

namespace ZmLabsBusiness.tests
{
    public class test3_sql_loaddata : objects.test_base
    {
        private IDataFunctions DataFunctions;
        private ITestFunctionsDomain DomainFunctions;

        private parte_horas_functions _phfunctions;

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public test3_sql_loaddata(TestDomain p_test,
                                  ITestFunctionsDomain p_DomainFunctions,
                                  IDataFunctions p_DataFunctions
                                  ) : base(p_test, p_DomainFunctions)
        {
            DataFunctions = p_DataFunctions;
            DomainFunctions = p_DomainFunctions;

            _phfunctions = new parte_horas_functions();
        }

        public override void Start()
        {
            try
            {
                //inicia test
                this.InitTest();

                //limpia la tabla antes de iniciar la ejecución
                //functions.parte_horas _partehoras = new functions.parte_horas();

                bool resTruncate = data_labs.ExecScript("truncate table [test].[ParteHoras]", DataFunctions.GetLabsCnx());

                //genera el parte de horas (fuera del cálculo de cada uno de los testcases)
                DateTime dtInicioCalculo = DateTime.Now;

                List<ParteHorasDomain> parteTotal = new List<ParteHorasDomain>();

                parteTotal.AddRange(_phfunctions.Generate(100, 2020));
                _phfunctions.Clear();

                parteTotal.AddRange(_phfunctions.Generate(100, 2019));
                _phfunctions.Clear();

                parteTotal.AddRange(_phfunctions.Generate(100, 2018));

                DateTime dtFinCalculo = DateTime.Now;

                TimeSpan _ts = dtFinCalculo - dtInicioCalculo;

                SetMsg("Cálculo anual 2018-2019-2020 para 100 trabajadores finalizado en " + _ts.TotalMilliseconds.ToString() + " milisegundos");
                SetMsg("- - - - -");

                //recorre y ejecuta testcases
                int cont = 0;

                while (cont < Test.TestCases.Count)
                {
                    TestCasesDomain _testcase = Test.TestCases.Where(ord => ord.Orden == cont + 1).First();

                    switch (_testcase.Function)
                    {
                        //Graba con EF
                        case "EFBulkData":

                            EFBulkData(parteTotal, ref _testcase);

                            //Limpia la tabla para nueva ejecución
                            resTruncate = data_labs.ExecScript("truncate table [test].[ParteHoras]", DataFunctions.GetLabsCnx());

                            break;

                        ////Graba con Sp y Datatable
                        case "ADOBulkData_Datatable":

                            ADOBulkData_Datatable(parteTotal, ref _testcase);
                            break;
                    }

                    cont++;
                }

                //finaliza test
                this.EndTest();
                
            }
            catch (Exception ex)
            {
                SetMsg("Error ejecutando test3_sql_loaddata - Start");
                _logger.Error(ex, "Error ejecutando test3_sql_loaddata - Start");
            }
            finally
            {
                _phfunctions.Clear();
            }
        }

        private void EFBulkData(List<ParteHorasDomain> _ParteAnual, ref TestCasesDomain _testcase)
        {
            try
            {
                _testcase.TestCaseExecution.idTestCase = _testcase.id;

                //registra inicio
                _testcase.TestCaseExecution.dtBegin = DateTime.Now;
                InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

                //ejecuta testcase
                _phfunctions.InsertParteAnualEF(_ParteAnual, new ZMLabsData.repos.sqltest_repos_partehoras(DataFunctions.GetLabsCnx()));

                //registra fin
                _testcase.TestCaseExecution.dtEnd = DateTime.Now;
                EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
            }
            catch (Exception ex)
            {
                SetMsg("Error ejecutando EFBulkData");
                _logger.Error(ex, "Error ejecutando EFBulkData");
            }
        }

        private void ADOBulkData_Datatable(List<ParteHorasDomain> _ParteAnual, ref TestCasesDomain _testcase)
        {
            try
            {
                _testcase.TestCaseExecution.idTestCase = _testcase.id;

                //registra inicio
                _testcase.TestCaseExecution.dtBegin = DateTime.Now;
                InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

                //ejecuta testcase

                //1. inicia conversión
                DateTime dtInicioConversion = DateTime.Now;
                SetMsg("Inicia conversión con reflection => parte_horas class to DataTable");

                DataTable _dtParteHoras = functions.reflections.CreateDataTable<ParteHorasDomain>(_ParteAnual, new List<string>() { "TipoJornada", "Anho" });
                                                                      
                //fin conversión
                DateTime dtFinConversion = DateTime.Now;
                TimeSpan _ts = dtFinConversion - dtInicioConversion;

                SetMsg("Conversión completada " + _ts.TotalMilliseconds.ToString() + " milisegundos");

                //2. inicia grabación

                //_data_test_sql.InsertParteHorasAnualADO(_dtParteHoras);
                _phfunctions.InsertParteAnualADO(_dtParteHoras, new ZMLabsData.ADO.data_test_partehoras(DataFunctions.GetLabsCnx()));

                //registra fin
                _testcase.TestCaseExecution.dtEnd = DateTime.Now;
                EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
            }
            catch (Exception ex)
            {
                SetMsg("Error ejecutando ADOBulkData_Datatable");
                _logger.Error(ex, "Error ejecutando ADOBulkData_Datatable");
            }
        }

    }
}
