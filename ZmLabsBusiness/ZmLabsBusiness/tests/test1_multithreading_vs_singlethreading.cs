using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using ZmLabsObjects;

namespace ZmLabsBusiness.tests
{
    public class test1_multithreading_vs_singlethreading : objects.test_exec
    {
        public static List<objects.process_control> _lst_process_control = new List<objects.process_control>();

        public test1_multithreading_vs_singlethreading(test_info.test_functions_base p_testfunctions) : base(p_testfunctions) { }

        public override void Start()
        {
            this.Estado = test_types.enumEstadoProceso.Ejecutando;

            SetMsg("- - - - -");
            SetMsg("test1_multithreading_vs_singlethreading iniciado a las " + DateTime.Now.ToLongTimeString());

            int cont = 0;

            while (cont < _testobject.Execution.testcases.Count)
            {
                TestCases _test = _testobject.Execution.testcases[cont];

                switch (_test.Function)
                {
                    case "MultithreadingCase":

                        _testobject.Execution.testcases[cont] = MultithreadingCase(_test);
                        break;

                    case "SinglethreadingCase":

                        _testobject.Execution.testcases[cont] = SinglethreadingCase(_test);
                        break;

                    case "HybridCase":

                        _testobject.Execution.testcases[cont] = HybridCase(_test);
                        break;
                }

                cont++;
                Thread.Sleep(1000);
            }

            SetMsg("- - - - -");
            SetMsg("test1_multithreading_vs_singlethreading finalizado a las " + DateTime.Now.ToLongTimeString());


            this.Estado = test_types.enumEstadoProceso.Finalizado;

            //return _testobject;
        }

        #region Cases

        /// <summary>
        /// Cálculo simultáneo de la serie fibo (500 hilos, 200 elementos por hilo)
        /// </summary>
        /// <param name="_test"></param>
        /// <returns></returns>
        public TestCases MultithreadingCase(TestCases _test)
        {
            //registra inicio
            _test.dtBegin = DateTime.Now;

            SetMsg("- - - - -");
            SetMsg("MultithreadingCase iniciado a las " + _test.dtBegin);

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
            _test.dtEnd = DateTime.Now;

            TimeSpan _ts = _test.dtEnd - _test.dtBegin;

            SetMsg("MultithreadingCase finalizado a las " + _test.dtEnd);
            SetMsg("MultithreadingCase ejecutado en " + _ts.TotalMilliseconds + " milisegundos");

            _testobject.TestRecord(_test);

            _lst_process_control.Clear();

            return _test;
        }

        private static void CalcFibo1(int index)
        {
            try
            {
                //Console.WriteLine("CalcFibo1 - Index " + index.ToString());
                functions.fibo.CalcFibo(200);

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
            //registra inicio
            _test.dtBegin = DateTime.Now;

            SetMsg("- - - - -");
            SetMsg("SinglethreadingCase iniciado a las " + _test.dtBegin);

            //500 iteraciones calculando 200 elementos de la serie fibo
            int cont = 0;

            while (cont < 500)
            {
                try
                {
                    functions.fibo.CalcFibo(200);
                    cont++;
                    Thread.Sleep(55);
                }
                catch (Exception)
                {

                }
            }

            //registra fin
            _test.dtEnd = DateTime.Now;

            TimeSpan _ts = _test.dtEnd - _test.dtBegin;

            SetMsg("SinglethreadingCase finalizado a las " + _test.dtEnd);
            SetMsg("SinglethreadingCase ejecutado en " + _ts.TotalMilliseconds + " milisegundos");

            _testobject.TestRecord(_test);

            return _test;
        }

        public TestCases HybridCase(TestCases _test)
        {
            //registra inicio
            _test.dtBegin = DateTime.Now;

            SetMsg("- - - - -");
            SetMsg("HybridCase iniciado a las " + _test.dtBegin);

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
            _test.dtEnd = DateTime.Now;

            TimeSpan _ts = _test.dtEnd - _test.dtBegin;

            SetMsg("HybridCase finalizado a las " + _test.dtEnd);
            SetMsg("HybridCase ejecutado en " + _ts.TotalMilliseconds + " milisegundos");

            _testobject.TestRecord(_test);

            _lst_process_control.Clear();

            return _test;
        }

        private static void CalcFibo2(int index)
        {
            try
            {
                //Console.WriteLine("CalcFibo1 - Index " + index.ToString());
                int cont = 0;

                while (cont < 25)
                {
                    try
                    {
                        functions.fibo.CalcFibo(200);
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
