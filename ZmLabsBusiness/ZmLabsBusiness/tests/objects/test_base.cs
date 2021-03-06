﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;

using ZmLabsObjects;
using ZmLabsObjects.contracts;

namespace ZmLabsBusiness.tests.objects
{
    /// <summary>
    /// Clase base de un Test específico
    /// Heredan las clases específicas de cada uno de los test: test1_x , test2_x etc...
    /// Gestiona la ejecución del test y la lectura/escritura que los test van generando, así como el estado de la ejecución
    /// </summary>
    public class test_base : ITest
    {
        public TestDomain Test;

        public test_types.enumEstadoProceso Estado;
        public List<test_types.mensajes> Mensajes = new List<test_types.mensajes>();

        private static int intentos_mensajes = 0;

        private static readonly NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

        private ITestFunctionsDomain DomainFunctions;

        public test_base(TestDomain p_Test, ITestFunctionsDomain p_DomainFunctions)
        {
            Test = p_Test;
            DomainFunctions = p_DomainFunctions;
        }

        /// <summary>
        /// Inicia la ejecución del Test seleccionado
        /// Sobreescrito en cada una de las clases de los test: test1_x , test2_x etc...
        /// </summary>
        public virtual void Start()
        {

        }

        public void InitTest()
        {
            try
            {
                this.Estado = test_types.enumEstadoProceso.Ejecutando;

                SetMsg("- - - - -");
                SetMsg(Test.Test + " iniciado a las " + DateTime.Now.ToLongTimeString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en InitTest");
            }
        }

        public void EndTest()
        {
            try
            {
                SetMsg("- - - - -");
                SetMsg(Test.Test + " finalizado a las " + DateTime.Now.ToLongTimeString());

                this.Estado = test_types.enumEstadoProceso.Finalizado;
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en EndTest");
            }
        }

        public void InitTestCase(string casename, DateTime dtBegin)
        {
            try
            {
                SetMsg("- - - - -");
                SetMsg(casename + " Case iniciado a las " + dtBegin.ToShortTimeString() + " " + dtBegin.ToLongTimeString());
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error en InitTestCase");
            }
        }

        public void EndTestCase(string casename, TestCaseExecutionsDomain _testexec)
        {
            try
            {
                _testexec.Insert(DomainFunctions);
            }
            catch (Exception ex)
            {
                SetMsg("Ha habido un problema al grabar los resultados en BBDD. Revise el fichero de erores");

                _logger.Error(ex, "Error en EndTestCase(" + casename + ")");
            }
            finally
            {
                SetMsg(casename + " Case finalizado a las " + _testexec.dtEnd);
                SetMsg(casename + " Case ejecutado en " + _testexec.Miliseconds + " milisegundos");
            }
        }

        //gestión de mensajes
        public void SetMsgLeido(Guid id)
        {
            reintenta:
            try
            {
                this.Mensajes.Where(idx => idx.id == id).First().leido = true;
            }
            catch (InvalidOperationException ex_inv)
            {
                intentos_mensajes++;
                _logger.Warn(ex_inv, "Lista modificada en SetMsgLeido - Intento  " + intentos_mensajes.ToString());

                Thread.Sleep(100);

                if (intentos_mensajes < 8539)
                {
                    goto reintenta;
                }
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error ejecutando SetMsgLeido");
            }
        }

        public void SetMsg(string Msg)
        {
            try
            {
                this.Mensajes.Add(new test_types.mensajes()
                {
                    id = Guid.NewGuid(),
                    mensaje = Msg,
                    leido = false
                });
            }
            catch (Exception ex)
            {
                _logger.Error(ex, "Error ejecutando SetMsg");
            }
        }
    }
}
