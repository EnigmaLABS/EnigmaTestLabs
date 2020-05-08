using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ZmLabsBusiness.functions.contracts;
using ZmLabsBusiness.tests.objects;
using ZmLabsObjects;

namespace ZmLabsBusiness.tests
{
    public class test1_multithreading_vs_singlethreading : objects.test_base
    {
        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        public static List<objects.process_control> _lst_process_control = new List<objects.process_control>();

        private static IFibo FiboCalc;

        private static objects.test_base _testexec;

        public test1_multithreading_vs_singlethreading(test_info.test_functions_base p_testfunctions, IFibo p_FiboCalc) : base(p_testfunctions)
        {
            FiboCalc = p_FiboCalc;
            _testexec = this;
        }

        public override void Start()
        {
            //inicia test
            this.InitTest();

            //recorre y ejecuta testcases
            int cont = 0;

            while (cont < _testexec._testobject.TestCases.Count)
            {
                TestCasesDomain _test = _testexec._testobject.TestCases.Where(ord => ord.Orden == cont+1).First();

                switch (_test.Function)
                {
                    case "MultithreadingCase":

                        MultithreadingCase(ref _test);
                        break;

                    case "MultithreadingCaseWithErrors":

                        MultithreadingCaseWithErrors(ref _test);
                        break;

                    case "SinglethreadingCase":

                        SinglethreadingCase(ref _test);
                        break;

                    case "HybridCase":

                        HybridCase(ref _test);
                        break;
                }

                cont++;
                Thread.Sleep(1000);
            }

            //finaliza test
            this.EndTest();
        }

        #region Cases

        /// <summary>
        /// Cálculo simultáneo de la serie fibo (500 hilos, 200 elementos por hilo)
        /// </summary>
        /// <param name="_test"></param>
        /// <returns></returns>
        public void MultithreadingCase(ref TestCasesDomain _test)
        {
            TestCaseExecutionsDomain _execution = new TestCaseExecutionsDomain() { idTestCase = _test.id };

            try
            {
                //registra inicio
                _execution.dtBegin = DateTime.Now;
                InitTestCase(_test.Function, _execution.dtBegin);

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
                _execution.dtEnd = DateTime.Now;
                EndTestCase(_test.Function, _execution);

                _lst_process_control.Clear();
            }
            catch (Exception ex)
            {
                _testexec.SetMsg("Error ejecutando MultithreadingCase");
                _logger.Error(ex, "Error ejecutando MultithreadingCase");
            }
        }

        /// <summary>
        /// Cálculo simultáneo de la serie fibo (500 hilos, 200 elementos por hilo) con error en todos los hilos
        /// </summary>
        /// <param name="_test"></param>
        /// <returns></returns>
        public void MultithreadingCaseWithErrors(ref TestCasesDomain _test)
        {
            TestCaseExecutionsDomain _execution = new TestCaseExecutionsDomain() { idTestCase = _test.id };

            try
            {
                //registra inicio
                _execution.dtBegin = DateTime.Now;
                InitTestCase(_test.Function, _execution.dtBegin);

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
                _execution.dtEnd = DateTime.Now;
                EndTestCase(_test.Function, _execution);
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
                FiboCalc.CalcFibo(200);

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

                FiboCalc.CalcFibo(200);

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Finalizado;

                _logger.Info("Finaliza cálculo serie Fibo - Index " + index.ToString());
            }
            catch (Exception ex)
            {
                //_testexec.SetMsg("Error INESPECÍFICO calculando la serie - Hilo nº " + index.ToString());
                _logger.Error(ex, " CalcFiboWithErrors - Index " + index.ToString());

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Erroneo;
            }
        }

        /// <summary>
        /// Cálculo secuencial de la serie fibo (500 iteraciones, 200 elementos por iteración)
        /// </summary>
        /// <param name="_test"></param>
        /// <returns></returns>
        public void SinglethreadingCase(ref TestCasesDomain _test)
        {
            try
            {
                TestCaseExecutionsDomain _testexec = new TestCaseExecutionsDomain() { idTestCase = _test.id };

                //registra inicio
                _testexec.dtBegin = DateTime.Now;
                InitTestCase(_test.Function, _testexec.dtBegin);

                //500 iteraciones calculando 200 elementos de la serie fibo
                int cont = 0;

                while (cont < 500)
                {
                    try
                    {
                        FiboCalc.CalcFibo(200);
                        Thread.Sleep(55);
                    }
                    catch (Exception) {  }  //TODO ????

                    cont++;
                }

                //registra fin
                _testexec.dtEnd = DateTime.Now;
                EndTestCase(_test.Function, _testexec);
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
        public void HybridCase(ref TestCasesDomain _test)
        {
            try
            {
                TestCaseExecutionsDomain _testexec = new TestCaseExecutionsDomain() { idTestCase = _test.id };

                //registra inicio
                _testexec.dtBegin = DateTime.Now;
                InitTestCase(_test.Function, _testexec.dtBegin);

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
                _testexec.dtEnd = DateTime.Now;
                EndTestCase(_test.Function, _testexec);
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
                    try
                    {
                        FiboCalc.CalcFibo(200);
                        Thread.Sleep(55);
                    }
                    catch (Exception) {  }  // TODO ?????????
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
