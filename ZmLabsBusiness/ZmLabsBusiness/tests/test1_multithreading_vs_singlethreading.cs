using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ZmLabsBusiness.functions.contracts;

using ZmLabsObjects;

namespace ZmLabsBusiness.tests
{
    public class test1_multithreading_vs_singlethreading : objects.test_exec
    {
        public static List<objects.process_control> _lst_process_control = new List<objects.process_control>();

        private static IFibo FiboCalc;

        public test1_multithreading_vs_singlethreading(test_info.test_functions_base p_testfunctions, IFibo p_FiboCalc) : base(p_testfunctions)
        {
            FiboCalc = p_FiboCalc;
        }

        public override void Start()
        {
            //inicia test
            this.InitTest();

            //recorre y ejecuta testcases
            int cont = 0;

            while (cont < _testobject.TestCases.Count)
            {
                TestCases _test = _testobject.TestCases[cont];

                switch (_test.Function)
                {
                    case "MultithreadingCase":

                        _testobject.TestCases[cont] = MultithreadingCase(_test);
                        break;

                    case "SinglethreadingCase":

                        _testobject.TestCases[cont] = SinglethreadingCase(_test);
                        break;

                    case "HybridCase":

                        _testobject.TestCases[cont] = HybridCase(_test);
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
        public TestCases MultithreadingCase(TestCases _test)
        {
            TestCaseExecutions _testexec = new TestCaseExecutions() { idTestCase = _test.id };

            //registra inicio
            _testexec.dtBegin = DateTime.Now;
            InitTestCase(_test.Function, _testexec.dtBegin);

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

            while (_lst_process_control.Exists(pc => pc.Estado != objects.process_control.enumEstadoProceso.Finalizado))
            {
                Thread.Sleep(55);
            }

            //registra fin
            _testexec.dtEnd = DateTime.Now;
            EndTestCase(_test.Function, _testexec);

            _lst_process_control.Clear();

            return _test;
        }

        private static void CalcFibo1(int index)
        {
            try
            {
                //functions.fibo.CalcFibo(200);
                FiboCalc.CalcFibo(200);

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Finalizado;
            }
            catch (Exception)
            {
                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Erroneo;
            }
        }

        /// <summary>
        /// Cálculo secuencial de la serie fibo (500 iteraciones, 200 elementos por iteración)
        /// </summary>
        /// <param name="_test"></param>
        /// <returns></returns>
        public TestCases SinglethreadingCase(TestCases _test)
        {
            TestCaseExecutions _testexec = new TestCaseExecutions() { idTestCase = _test.id };

            //registra inicio
            _testexec.dtBegin = DateTime.Now;
            InitTestCase(_test.Function, _testexec.dtBegin);

            //500 iteraciones calculando 200 elementos de la serie fibo
            int cont = 0;

            while (cont < 500)
            {
                try
                {
                    //functions.fibo.CalcFibo(200);
                    FiboCalc.CalcFibo(200);

                    cont++;
                    Thread.Sleep(55);
                }
                catch (Exception)
                {

                }
            }

            //registra fin
            _testexec.dtEnd = DateTime.Now;
            EndTestCase(_test.Function, _testexec);

            return _test;
        }

        public TestCases HybridCase(TestCases _test)
        {
            TestCaseExecutions _testexec = new TestCaseExecutions() { idTestCase = _test.id };

            //registra inicio
            _testexec.dtBegin = DateTime.Now;
            InitTestCase(_test.Function, _testexec.dtBegin);

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

            while (_lst_process_control.Exists(pc => pc.Estado != objects.process_control.enumEstadoProceso.Finalizado))
            {
                Thread.Sleep(55);
            }

            //registra fin
            _testexec.dtEnd = DateTime.Now;
            EndTestCase(_test.Function, _testexec);

            _lst_process_control.Clear();

            return _test;
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
                        //functions.fibo.CalcFibo(200);
                        FiboCalc.CalcFibo(200);

                        cont++;
                        Thread.Sleep(55);
                    }
                    catch (Exception)
                    {

                    }
                }

                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Finalizado;
            }
            catch (Exception)
            {
                _lst_process_control[index].Estado = objects.process_control.enumEstadoProceso.Erroneo;
            }
        }

        #endregion

    }
}
