using System;
using System.Linq;
using System.Data;
using System.Collections.Generic;

using ZmLabsObjects;
using ZmLabsObjects.sqltests;

using ZMLabsData;
using ZMLabsData.repos;
using ZMLabsData.ADO;

using ZmLabsBusiness.functions;
using ZmLabsBusiness.data.contracts;

using ZmLabsObjects.contracts;

namespace ZmLabsBusiness.tests
{
    public class test3_sql_loaddata : objects.test_base
    {
        private IDataFunctions DataFunctions;
        private ITestFunctionsDomain DomainFunctions;

        public test3_sql_loaddata(TestDomain p_test,
                                  ITestFunctionsDomain p_DomainFunctions,
                                  IDataFunctions p_DataFunctions
                                  ) : base(p_test, p_DomainFunctions)
        {
            DataFunctions = p_DataFunctions;
            DomainFunctions = p_DomainFunctions;
        }

        public override void Start()
        {
            //inicia test
            this.InitTest();

            parte_horas_functions _phfunctions = new parte_horas_functions();

            //limpia la tabla antes de iniciar la ejecución
            //functions.parte_horas _partehoras = new functions.parte_horas();

            bool resTruncate = data_labs.ExecScript("truncate table [test].[ParteHoras]", DataFunctions.GetLabsCnx());

            //genera el parte de horas (fuera del cálculo de cada uno de los testcases)
            DateTime dtInicioCalculo = DateTime.Now;

            var listahoras = _phfunctions.Generate(100, 2020);

            DateTime dtFinCalculo = DateTime.Now;

            TimeSpan _ts = dtFinCalculo - dtInicioCalculo;

            SetMsg("Cálculo anual 2020 para 100 trabajadores finalizado en " + _ts.TotalMilliseconds.ToString() + " milisegundos");
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

                        EFBulkData(listahoras, ref _testcase);

                        //Limpia la tabla para nueva ejecución
                        resTruncate = data_labs.ExecScript("truncate table [test].[ParteHoras]", DataFunctions.GetLabsCnx());

                        break;

                    //Graba con Sp y Datatable
                    case "ADOBulkData_Datatable":

                        ADOBulkData_Datatable(listahoras, ref _testcase);
                        break;
                }

                cont++;
            }
           
            //finaliza test
            this.EndTest();
            _phfunctions.Clear();
        }

        private void EFBulkData(List<ParteHorasDomain> _ParteAnual, ref TestCasesDomain _testcase)
        {
            _testcase.TestCaseExecution.idTestCase = _testcase.id;

            //registra inicio
            _testcase.TestCaseExecution.dtBegin = DateTime.Now;
            InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

            //ejecuta testcase
            sqltest_repos_partehoras _testrepos = new sqltest_repos_partehoras(DataFunctions.GetLabsCnx());

            _testrepos.InsertParteHorasAnual(_ParteAnual);

            //registra fin
            _testcase.TestCaseExecution.dtEnd = DateTime.Now;
            EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
        }

        private void ADOBulkData_Datatable(List<ParteHorasDomain> _ParteAnual, ref TestCasesDomain _testcase)
        {
            _testcase.TestCaseExecution.idTestCase = _testcase.id;

            //registra inicio
            _testcase.TestCaseExecution.dtBegin = DateTime.Now;
            InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

            //ejecuta testcase
            data_test_partehoras _data_test_sql = new data_test_partehoras(DataFunctions.GetLabsCnx());

            //1. inicia conversión
            DateTime dtInicioConversion = DateTime.Now;
            SetMsg("Inicia conversión con reflection => parte_horas class to DataTable");

            DataTable _dtParteHoras = functions.reflections.CreateDataTable<ParteHorasDomain>(_ParteAnual, new List<string>() { "TipoJornada" });

            //fin conversión
            DateTime dtFinConversion = DateTime.Now;
            TimeSpan _ts = dtFinConversion - dtInicioConversion;

            SetMsg("Conversión completada " + _ts.TotalMilliseconds.ToString() + " milisegundos");

            //2. inicia grabación
            _data_test_sql.InsertParteHorasAnualADO(_dtParteHoras);

            //registra fin
            _testcase.TestCaseExecution.dtEnd = DateTime.Now;
            EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
        }

        private void ADOBulkData_RowByRow()
        {

        }

    }
}
