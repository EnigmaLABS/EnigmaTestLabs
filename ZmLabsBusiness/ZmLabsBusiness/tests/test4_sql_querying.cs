using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

using ZmLabsObjects;
using ZmLabsObjects.sqltests;
using ZmLabsObjects.contracts;

using ZmLabsBusiness.functions;
using ZmLabsBusiness.tests.objects;
using ZmLabsBusiness.data.contracts;

using ZMLabsData;

namespace ZmLabsBusiness.tests
{
    public class test4_sql_querying : objects.test_base
    {
        private static IDataFunctions DataFunctions;
        private ITestFunctionsDomain DomainFunctions;

        public static List<process_control> _lst_process_control = new List<process_control>();
        private static test_base _testexec;

        private static parte_horas_functions _phfunctions;

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public test4_sql_querying(TestDomain p_test,
                                  ITestFunctionsDomain p_DomainFunctions,
                                  IDataFunctions p_DataFunctions
                                  ) : base(p_test, p_DomainFunctions)
        {
            DataFunctions = p_DataFunctions;
            DomainFunctions = p_DomainFunctions;
            _testexec = this;

            _phfunctions = new parte_horas_functions();
        }

        public override void Start()
        {
            try
            {
                //inicia test
                this.InitTest();

                //recorre y ejecuta los testcases
                int cont = 0;

                while (cont < Test.TestCases.Count)
                {
                    TestCasesDomain _testcase = Test.TestCases.Where(ord => ord.Orden == cont + 1).First();

                    switch (_testcase.Function)
                    {
                        //Graba con EF
                        case "InformeAbsentismo_EF":

                            InformeAbsentismo_EF(ref _testcase);
                            break;

                        //Graba con Sp y Datatable
                        case "InformeAbsentismo_ADO":

                            InformeAbsentismo_ADO(ref _testcase);
                            break;
                    }

                    cont++;
                }

                //finaliza test
                this.EndTest();
            }
            catch (Exception ex)
            {
                SetMsg("Error ejecutando test4_sql_querying - Start");
                _logger.Error(ex, "Error ejecutando test4_sql_querying - Start");
            }
        }

        private void InformeAbsentismo_EF(ref TestCasesDomain _testcase)
        {
            try
            {
                _testcase.TestCaseExecution.idTestCase = _testcase.id;

                //registra inicio
                _testcase.TestCaseExecution.dtBegin = DateTime.Now;
                InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

                //EJECUTA TESTCASE
                //-----------------------------------------------------------------------------------------

                //Primera oleada de peticiones (5 peticiones - 1 año)
                for (int cont = 0; cont < 5; cont++)
                {
                    List<int> anhossolicitados = new List<int>() { 2020 };

                    Thread thPeticiones = new Thread(() => Hilo_Peticiones_EF(cont, anhossolicitados));

                    _lst_process_control.Add(new objects.process_control()
                    {
                        Estado = process_control.enumEstadoProceso.Ejecutando,
                        Hilo = thPeticiones
                    });

                    thPeticiones.Start();
                    Thread.Sleep(22);
                }

                //Segunda oleada de peticiones (5 peticiones - 2 años)
                for (int cont = 0; cont < 5; cont++)
                {
                    List<int> anhossolicitados = new List<int>() { 2019, 2020 };

                    Thread thPeticiones = new Thread(() => Hilo_Peticiones_EF(cont+5, anhossolicitados));

                    _lst_process_control.Add(new objects.process_control()
                    {
                        Estado = process_control.enumEstadoProceso.Ejecutando,
                        Hilo = thPeticiones
                    });

                    thPeticiones.Start();
                    Thread.Sleep(22);
                }

                //Tercera oleada de peticiones (5 peticiones - 3 años)
                for (int cont = 0; cont < 5; cont++)
                {
                    List<int> anhossolicitados = new List<int>() { 2018, 2019, 2020 };

                    Thread thPeticiones = new Thread(() => Hilo_Peticiones_EF(cont + 10, anhossolicitados));

                    _lst_process_control.Add(new objects.process_control()
                    {
                        Estado = process_control.enumEstadoProceso.Ejecutando,
                        Hilo = thPeticiones
                    });

                    thPeticiones.Start();
                    Thread.Sleep(22);
                }

                //Controla el final de los hilos
                while (_lst_process_control.Exists(pc => pc.Estado == objects.process_control.enumEstadoProceso.Ejecutando))
                {
                    Thread.Sleep(55);
                }

                _lst_process_control.Clear();

                //registra fin
                _testcase.TestCaseExecution.dtEnd = DateTime.Now;
                EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
            }
            catch (Exception ex)
            {
                SetMsg("Error ejecutando InformeAbsentismo_EF");
                _logger.Error(ex, "Error ejecutando InformeAbsentismo_EF");
            }
        }

        private void InformeAbsentismo_ADO(ref TestCasesDomain _testcase)
        {
            _testcase.TestCaseExecution.idTestCase = _testcase.id;

            //registra inicio
            _testcase.TestCaseExecution.dtBegin = DateTime.Now;
            InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);


            //EJECUTA TESTCASE
            //-----------------------------------------------------------------------------------------

            //Primera oleada de peticiones (5 peticiones - 1 año)
            for (int cont = 0; cont < 5; cont++)
            {
                List<int> anhossolicitados = new List<int>() { 2020 };

                Thread thPeticiones = new Thread(() => Hilo_Peticiones_ADO(cont, anhossolicitados));

                _lst_process_control.Add(new objects.process_control()
                {
                    Estado = process_control.enumEstadoProceso.Ejecutando,
                    Hilo = thPeticiones
                });
                thPeticiones.Start();
                Thread.Sleep(22);

            }

            //Segunda oleada de peticiones (5 peticiones - 2 años)
            for (int cont = 0; cont < 5; cont++)
            {
                List<int> anhossolicitados = new List<int>() { 2019, 2020 };

                Thread thPeticiones = new Thread(() => Hilo_Peticiones_ADO(cont + 5, anhossolicitados));

                _lst_process_control.Add(new objects.process_control()
                {
                    Estado = process_control.enumEstadoProceso.Ejecutando,
                    Hilo = thPeticiones
                });

                thPeticiones.Start();
                Thread.Sleep(22);
            }

            //Tercera oleada de peticiones (5 peticiones - 3 años)
            for (int cont = 0; cont < 5; cont++)
            {
                List<int> anhossolicitados = new List<int>() { 2018, 2019, 2020 };

                Thread thPeticiones = new Thread(() => Hilo_Peticiones_ADO(cont + 10, anhossolicitados));

                _lst_process_control.Add(new objects.process_control()
                {
                    Estado = process_control.enumEstadoProceso.Ejecutando,
                    Hilo = thPeticiones
                });

                thPeticiones.Start();
                Thread.Sleep(22);
            }

            //Controla el final de los hilos
            while (_lst_process_control.Exists(pc => pc.Estado == objects.process_control.enumEstadoProceso.Ejecutando))
            {
                Thread.Sleep(55);
            }

            _lst_process_control.Clear();

            //registra fin
            _testcase.TestCaseExecution.dtEnd = DateTime.Now;
            EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
        }

        #region Peticiones

        private static void Hilo_Peticiones_EF(int index, List<int> anhos)
        {
            try
            {
                for (int contpeti = 0; contpeti < 5; contpeti++)
                {
                    var res = _phfunctions.GetInformeAbsentismoEF(anhos, new ZMLabsData.repos.sqltest_repos_partehoras(DataFunctions.GetLabsCnx()));
                    Thread.Sleep(55);
                }

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Finalizado;
            }
            catch (Exception ex)
            {
                _testexec.SetMsg("Error INESPECÍFICO en Hilo_Peticiones_EF - Hilo nº " + index.ToString());
                _logger.Error(ex, "Hilo_Peticiones_EF - Index " + index.ToString());

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Erroneo;
            }
        }

        private void Hilo_Peticiones_ADO(int index, List<int> anhos)
        {
            try
            {
                for (int contpeti = 0; contpeti < 5; contpeti++)
                {
                    var res = _phfunctions.GetInformeAbsentismoADO(anhos, new ZMLabsData.ADO.data_test_partehoras(DataFunctions.GetLabsCnx()));
                    Thread.Sleep(55);
                }

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Finalizado;
            }
            catch (Exception ex)
            {
                _testexec.SetMsg("Error INESPECÍFICO en Hilo_Peticiones_ADO - Hilo nº " + index.ToString());
                _logger.Error(ex, "Hilo_Peticiones_ADO - Index " + index.ToString());

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Erroneo;
            }
        }


        #endregion
    }
}
