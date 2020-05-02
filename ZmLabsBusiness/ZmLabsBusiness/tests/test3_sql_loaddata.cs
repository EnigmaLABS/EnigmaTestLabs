using System;
using System.Data;
using System.Collections.Generic;

using ZmLabsObjects;
using ZmLabsObjects.sqltests;

using ZMLabsData;
using ZMLabsData.repos;
using ZMLabsData.ADO;

namespace ZmLabsBusiness.tests
{
    public class test3_sql_loaddata : objects.test_exec
    {
        data.data_functions _df = new data.data_functions();

        private IParteHoras ParteHorasFunctions;

        public test3_sql_loaddata(test_info.test_functions_base p_testfunctions, IParteHoras p_ParteHorasFunctions) : base(p_testfunctions)
        {
            ParteHorasFunctions = p_ParteHorasFunctions;
        }

        public override void Start()
        {
            //inicia test
            this.InitTest();

            //limpia la tabla antes de iniciar la ejecución
            //functions.parte_horas _partehoras = new functions.parte_horas();

            bool resTruncate = data_labs.ExecScript("truncate table [test].[ParteHoras]", _df.GetLabsCnx());

            //genera el parte de horas (fuera del cálculo de cada uno de los testcases)
            DateTime dtInicioCalculo = DateTime.Now;

            var listahoras = ParteHorasFunctions.Generate(100, 2020);

            DateTime dtFinCalculo = DateTime.Now;

            TimeSpan _ts = dtFinCalculo - dtInicioCalculo;

            SetMsg("Cálculo anual 2020 para 100 trabajadores finalizado en " + _ts.TotalMilliseconds.ToString() + " milisegundos");
            SetMsg("- - - - -");

            //recorre y ejecuta testcases
            int cont = 0;

            while (cont < _testobject.TestCases.Count)
            {
                TestCasesDomain _test = _testobject.TestCases[cont];

                switch (_test.Function)
                {
                    //Graba con EF
                    case "EFBulkData":

                        _testobject.TestCases[cont] = EFBulkData(listahoras, _test);

                        //Limpia la tabla para nueva ejecución
                        resTruncate = data_labs.ExecScript("truncate table [test].[ParteHoras]", _df.GetLabsCnx());

                        break;

                    case "ADOBulkData_Datatable":

                        //Graba con Sp y Datatable
                        _testobject.TestCases[cont] = ADOBulkData_Datatable(listahoras, _test);
                        break;
                }

                cont++;
            }
           
            //finaliza test
            this.EndTest();
            ParteHorasFunctions.Clear();
        }

        private TestCasesDomain EFBulkData(List<IParteHoras> _ParteAnual, TestCasesDomain _test)
        {
            TestCaseExecutionsDomain _testexec = new TestCaseExecutionsDomain() { idTestCase = _test.id };

            //registra inicio
            _testexec.dtBegin = DateTime.Now;
            InitTestCase(_test.Function, _testexec.dtBegin);

            //ejecuta testcase
            sqltest_repos_partehoras _testrepos = new sqltest_repos_partehoras(_df.GetLabsCnx());

            _testrepos.InsertParteHorasAnual(_ParteAnual);

            //registra fin
            _testexec.dtEnd = DateTime.Now;
            EndTestCase(_test.Function, _testexec);

            return _test;
        }

        private TestCasesDomain ADOBulkData_Datatable(List<IParteHoras> _ParteAnual, TestCasesDomain _test)
        {
            TestCaseExecutionsDomain _testexec = new TestCaseExecutionsDomain() { idTestCase = _test.id };

            //registra inicio
            _testexec.dtBegin = DateTime.Now;
            InitTestCase(_test.Function, _testexec.dtBegin);

            //ejecuta testcase
            data_test_sql _data_test_sql = new data_test_sql(_df.GetLabsCnx());

            //1. inicia conversión
            DateTime dtInicioConversion = DateTime.Now;
            SetMsg("Inicia conversión con reflection => parte_horas class to DataTable");

            DataTable _dtParteHoras = functions.reflections.CreateDataTable<IParteHoras>(_ParteAnual, new List<string>() { "TipoJornada" });

            //fin conversión
            DateTime dtFinConversion = DateTime.Now;
            TimeSpan _ts = dtFinConversion - dtInicioConversion;

            SetMsg("Conversión completada " + _ts.TotalMilliseconds.ToString() + " milisegundos");

            //2. inicia grabación
            _data_test_sql.InsertParteHorasAnual(_dtParteHoras);

            //registra fin
            _testexec.dtEnd = DateTime.Now;
            EndTestCase(_test.Function, _testexec);

            return _test;
        }

        private void ADOBulkData_RowByRow()
        {


        }
    }
}
