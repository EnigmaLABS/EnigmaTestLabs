using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using ZmLabsBusiness.functions;
using ZmLabsBusiness.tests.objects;

using ZmLabsObjects;
using ZmLabsObjects.contracts;

namespace ZmLabsBusiness.tests
{
    public class test1_multithreading_vs_singlethreading : objects.test_base
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();
        public static List<objects.process_control> _lst_process_control = new List<objects.process_control>();

        private static TestDomain Test;
        private static test_base _testexec;

        public test1_multithreading_vs_singlethreading(TestDomain p_Test,
                                                       ITestFunctionsDomain p_DomainFunctions) : base(p_Test, p_DomainFunctions)
        {
            _testexec = this;
            Test = p_Test;
        }

        public override void Start()
        {
            try
            {
                //inicia test
                this.InitTest();

                //recorre y ejecuta testcases
                int cont = 0;

                while (cont < Test.TestCases.Count)
                {
                    TestCasesDomain _testcase = Test.TestCases.Where(ord => ord.Orden == cont + 1).First();

                    switch (_testcase.Function)
                    {
                        case "MultithreadingCase":

                            MultithreadingCase(ref _testcase);
                            break;

                        case "MultithreadingCaseWithErrors":

                            MultithreadingCaseWithErrors(ref _testcase);
                            break;

                        case "SinglethreadingCase":

                            SinglethreadingCase(ref _testcase);
                            break;

                        case "HybridCase":

                            HybridCase(ref _testcase);
                            break;
                    }

                    cont++;
                    Thread.Sleep(1000);
                }

                //finaliza test
                this.EndTest();
            }
            catch (Exception ex)
            {
                _testexec.SetMsg("Error ejecutando test1_multithreading_vs_singlethreading - Start");
                _logger.Error(ex, "Error ejecutando test1_multithreading_vs_singlethreading - Start");
            }
        }

        #region Cases

        /// <summary>
        /// Cálculo simultáneo de la serie fibo (500 hilos, 200 elementos por hilo)
        /// </summary>
        /// <param name="_test"></param>
        /// <returns></returns>
        private void MultithreadingCase(ref TestCasesDomain _testcase)
        {
            _testcase.TestCaseExecution.idTestCase = _testcase.id;

            try
            {
                //registra inicio
                _testcase.TestCaseExecution.dtBegin = DateTime.Now;
                InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

                _lst_process_control.Clear();

                //500 hilos calculan la serie fibo
                int cont = 0;

                while (cont < 500)
                {
                    Thread thfibo = new Thread(() => CalcFibo1(cont));

                    _lst_process_control.Add(new objects.process_control()
                    {
                        Estado = objects.process_control.enumEstadoProceso.Ejecutando,
                        Hilo = thfibo
                    });

                    thfibo.Start();

                    Thread.Sleep(55);
                    cont++;
                }

                while (_lst_process_control.Exists(pc => pc.Estado == objects.process_control.enumEstadoProceso.Ejecutando))
                {
                    Thread.Sleep(55);
                }

                //registra fin
                _testcase.TestCaseExecution.dtEnd = DateTime.Now;
                EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
            }
            catch (Exception ex)
            {
                _testexec.SetMsg("Error ejecutando MultithreadingCase");
                _logger.Error(ex, "Error ejecutando MultithreadingCase");
            }
            finally
            {
                _lst_process_control.Clear();
            }
        }

        /// <summary>
        /// Cálculo simultáneo de la serie fibo (500 hilos, 200 elementos por hilo) con error en todos los hilos
        /// </summary>
        /// <param name="_test"></param>
        /// <returns></returns>
        public void MultithreadingCaseWithErrors(ref TestCasesDomain _testcase)
        {
            _testcase.TestCaseExecution.idTestCase = _testcase.id;

            try
            {
                //registra inicio
                _testcase.TestCaseExecution.dtBegin = DateTime.Now;
                InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

                _lst_process_control.Clear();

                //500 hilos calculan la serie fibo
                int cont = 0;

                while (cont < 500)
                {
                    Thread thfibo = new Thread(() => CalcFiboWithErrors(cont));

                    _lst_process_control.Add(new objects.process_control()
                    {
                        Estado = objects.process_control.enumEstadoProceso.Ejecutando,
                        Hilo = thfibo
                    });

                    thfibo.Start();

                    Thread.Sleep(55);
                    cont++;
                }

                while (_lst_process_control.Exists(pc => pc.Estado == objects.process_control.enumEstadoProceso.Ejecutando))
                {
                    Thread.Sleep(55);
                }

                //registra fin
                _testcase.TestCaseExecution.dtEnd = DateTime.Now;
                EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
            }
            catch (Exception ex)
            {
                _testexec.SetMsg("Error ejecutando MultithreadingCase");
                _logger.Error(ex, "Error ejecutando MultithreadingCase");
            }
            finally
            {
                _lst_process_control.Clear();
            }
        }

        private static void CalcFibo1(int index)
        {
            try
            {
                fibo_functions.CalcFibo(200);
                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Finalizado;
            }
            catch (Exception ex)
            {
                _testexec.SetMsg("Error INESPECÍFICO calculando la serie - Hilo nº " + index.ToString()); 
                _logger.Error(ex, " CalcFibo1 - Index " + index.ToString());

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Erroneo;
            }
        }

        private static void CalcFiboWithErrors(int index)
        {
            try
            {
                _logger.Info("Inicia cálculo serie Fibo - Index " + index.ToString());

                if (index == 1)
                {
                    //error 1: 
                    object o2 = null; int i2 = (int)o2;
                }
                else
                {
                    //error 2: 
                    int a = 0; int b = 1 / a;
                }

                fibo_functions.CalcFibo(200);

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Finalizado;

                _logger.Info("Finaliza cálculo serie Fibo - Index " + index.ToString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex, " CalcFiboWithErrors - Index " + index.ToString());

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Erroneo;
            }
        }

        /// <summary>
        /// Cálculo secuencial de la serie fibo (500 iteraciones, 200 elementos por iteración)
        /// </summary>
        /// <param name="_test"></param>
        /// <returns></returns>
        public void SinglethreadingCase(ref TestCasesDomain _testcase)
        {
            try
            {
                _testcase.TestCaseExecution.idTestCase = _testcase.id;

                //registra inicio
                _testcase.TestCaseExecution.dtBegin = DateTime.Now;
                InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

                //500 iteraciones calculando 200 elementos de la serie fibo
                int cont = 0;

                while (cont < 500)
                {
                    try
                    {
                        fibo_functions.CalcFibo(200);
                        Thread.Sleep(55);
                    }
                    catch (Exception) {  }  //TODO ????

                    cont++;
                }

                //registra fin
                _testcase.TestCaseExecution.dtEnd = DateTime.Now;
                EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
            }
            catch (Exception ex)
            {
                _testexec.SetMsg("Error ejecutando SinglethreadingCase");
                _logger.Error(ex, "Error ejecutando SinglethreadingCase");
            }
        }

        /// <summary>
        /// Combinación de Singlethreading y Multithreading
        /// </summary>
        /// <param name="_test"></param>
        public void HybridCase(ref TestCasesDomain _testcase)
        {
            try
            {
                _testcase.TestCaseExecution.idTestCase = _testcase.id;

                //registra inicio
                _testcase.TestCaseExecution.dtBegin = DateTime.Now;
                InitTestCase(_testcase.Function, _testcase.TestCaseExecution.dtBegin);

                _lst_process_control.Clear();

                //20 hilos calculan 25 veces cada uno la serie fibo
                int cont = 0;

                while (cont < 50)
                {
                    Thread thfibo = new Thread(() => CalcFibo2(cont));

                    _lst_process_control.Add(new objects.process_control()
                    {
                        Estado = objects.process_control.enumEstadoProceso.Ejecutando,
                        Hilo = thfibo
                    });

                    thfibo.Start();

                    Thread.Sleep(55);
                    cont++;
                }

                while (_lst_process_control.Exists(pc => pc.Estado == objects.process_control.enumEstadoProceso.Ejecutando))
                {
                    Thread.Sleep(55);
                }

                //registra fin
                _testcase.TestCaseExecution.dtEnd = DateTime.Now;
                EndTestCase(_testcase.Function, _testcase.TestCaseExecution);
            }
            catch (Exception ex)
            {
                _testexec.SetMsg("Error ejecutando HybridCase");
                _logger.Error(ex, "Error ejecutando HybridCase");
            }
            finally
            {
                _lst_process_control.Clear();
            }
        }

        private static void CalcFibo2(int index)
        {
            try
            {
                int cont = 0;

                while (cont < 25)
                {
                    fibo_functions.CalcFibo(200);
                    Thread.Sleep(55);

                    cont++;
                }

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Finalizado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, " CalcFibo2 - Index " + index.ToString());
                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Erroneo;
            }
        }

        #endregion

    }
}
