using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

using ZmLabsObjects.sqltests;

namespace ZmLabsBusiness.functions
{
    public class parte_horas_estado_proceso
    {
        public enum enumEstadoProceso { Ejecutando, Finalizado };

        public Guid Trabajador;
        public enumEstadoProceso EstadoProceso;
    }

    public class parte_horas_functions 
    {
        private static List<ParteHorasDomain> _ParteAnual;

        public parte_horas_functions()
        {
            _ParteAnual = new List<ParteHorasDomain>();
        }

        //Propiedades privadas
        private static Random _rnd = new Random();

        private static List<parte_horas_estado_proceso> EstadoProceso = new List<parte_horas_estado_proceso>();

        public List<ParteHorasDomain> Generate(int numTrabajadores, int Anho)
        {
            for (int cont = 0; cont < numTrabajadores; cont++)
            {
                //monta un hilo para calcular el parte anual de cada trabajador
                Guid _Trabajador = Guid.NewGuid();

                EstadoProceso.Add(new parte_horas_estado_proceso()
                {
                    Trabajador = _Trabajador,
                    EstadoProceso = parte_horas_estado_proceso.enumEstadoProceso.Ejecutando
                });

                Thread _thCalculoTrabajador = new Thread(() => CalculaParteAnualTrabajador(_Trabajador, Anho));

                _thCalculoTrabajador.Start();

                Thread.Sleep(75);
            }

            //espera al final del proceso
            while (EstadoProceso.Exists(est => est.EstadoProceso != parte_horas_estado_proceso.enumEstadoProceso.Finalizado))
            {
                Thread.Sleep(555);
            }

            return _ParteAnual;
        }

        public void Clear()
        {
            _ParteAnual.Clear();
            EstadoProceso.Clear();
        }

        private static void CalculaParteAnualTrabajador(Guid _Trabajador, int Anho)
        {
            try
            {
                DateTime dtActual = new DateTime(Anho, 1, 1);
                DateTime dtFin = new DateTime(Anho, 12, 31);

                while (dtActual <= dtFin)
                {
                    if (dtActual.DayOfWeek != DayOfWeek.Saturday & dtActual.DayOfWeek != DayOfWeek.Sunday)
                    {
                        ParteHorasDomain _partediario = new ParteHorasDomain()
                        {
                            Trabajador = _Trabajador,
                            Fecha = dtActual
                        };

                        // ¿Hubo baja los últimos 5 días trabajados?
                        bool HuboBaja = _ParteAnual.Exists(r => r.Fecha >= dtActual.AddDays(-5) && r.TipoJornada == ParteHorasDomain.enumTipoJornada.Baja);

                        // ¿Hubo incidencia los últimos 5 días trabajados, sin baja médica?
                        // Si hubo obtiene las horas de incidencia acumuladas
                        bool HuboIncidencia = false;
                        int HorasIncidencia = 0;

                        if (!HuboBaja)
                        {
                            var Indicencias5Dias = _ParteAnual.Where(r => r.Fecha >= dtActual.AddDays(-5) && r.TipoJornada == ParteHorasDomain.enumTipoJornada.Incidencia).ToList();
                            HuboIncidencia = Indicencias5Dias.Count > 0;

                            if (HuboIncidencia)
                            {
                                HorasIncidencia = Indicencias5Dias.Sum(h => 8 - h.Horas);
                            }
                        }

                        //calculamos las probabilidades para cada casuística
                        if (HuboBaja)
                        {
                            _partediario.Horas = GetHorasConBaja();
                        }
                        else if (HuboIncidencia)
                        {
                            _partediario.Horas = GetHorasConIncidencia(HorasIncidencia);
                        }
                        else
                        {
                            _partediario.Horas = GetHorasNormal();
                        }

                        _ParteAnual.Add(_partediario);
                    }


                    dtActual = dtActual.AddDays(1);
                }

                EstadoProceso.Where(tr => tr.Trabajador == _Trabajador).First()
                                                        .EstadoProceso = parte_horas_estado_proceso.enumEstadoProceso.Finalizado;
            }
            catch (Exception ex)
            {
                string err = ex.Message;
            }
        }

        private static Int16 GetHorasConBaja()
        {
            int horas = 0;
            int rnd100 = _rnd.Next(1, 100);

            // 80% probabilidad jornada normal
            // 5%  probabilidad incidencia de 1 a 2 horas
            // 5%  probabilidad incidencia de 2 a 4 horas
            // 10% probabilidad nueva baja médica

            if (rnd100 <= 80)
            {
                //normal
                horas = 8;
            }
            else if (rnd100 > 80 && rnd100 <= 85)
            {
                //incidencia de 1 a 2 horas
                horas = 8 - _rnd.Next(1, 2);

            }
            else if (rnd100 > 85 && rnd100 <= 90)
            {
                //incidencia de 2 a 4 horas
                horas = 8 - (_rnd.Next(2, 4));
            }
            else
            {
                horas = 0;
            }

            return (Int16)horas;
        }

        private static Int16 GetHorasConIncidencia(int HorasIncidencia)
        {
            int horas = 0;
            int rnd100 = _rnd.Next(1, 100);

            // 40% probabilidad de recuperar el 50% de la incidencia acumulada (max horas extras = 2)
            // 20% probabilidad de recuperar el 100% de la incidencia acumulada (max horas extras = 2)
            // 30% probabilidad jornada normal
            // 4% probabilidad nueva incidencia entre 1 a 2 horas
            // 3% probabilidad nueva incidencia entre 2 a 4 horas
            // 3% probabilidad de baja mádica

            if (rnd100 <= 40)
            {
                //recupera el 50% de la incidencia acumulada (max horas extras = 2)
                var _50PctHotasIncidencia = Math.Round((decimal)HorasIncidencia / 2);

                horas = (Int16)(_50PctHotasIncidencia < 2 ? 8 + _50PctHotasIncidencia : 10);
            }
            else if (rnd100 > 40 && rnd100 <= 60)
            {
                //recupera el 100% de la incidencia acumulada (max horas extras = 2)
                horas = (Int16)(HorasIncidencia < 2 ? 8 + HorasIncidencia : 10);

            }
            else if (rnd100 > 60 && rnd100 <= 90)
            {
                //incidencia de 2 a 4 horas
                horas = 8;
            }
            else if (rnd100 > 90 && rnd100 <= 94)
            {
                //incidencia de 1 a 2 horas
                horas = 8 - _rnd.Next(1, 2);
            }
            else if (rnd100 > 94 && rnd100 <= 97)
            {
                //incidencia de 2 a 4 horas
                horas = 8 - (_rnd.Next(2, 4));
            }
            else
            {
                //baja médica
                horas = 0;
            }

            return (Int16)horas;
        }

        private static Int16 GetHorasNormal()
        {
            int horas = 0;
            int rnd100 = _rnd.Next(1, 100);

            // 85% probabilidad de jornada normal
            // 7% incidencia de 1 a 2 horas
            // 5% incidencia de 2 a 4 horas
            // 3% probabilidad de baja médica

            if (rnd100 <= 85)
            {
                //normal
                horas = 8;
            }
            else if (rnd100 > 85 && rnd100 <= 92)
            {
                //incidencia de 1 a 2 horas
                horas = 8 - _rnd.Next(1, 2);

            }
            else if (rnd100 > 92 && rnd100 <= 97)
            {
                //incidencia de 2 a 4 horas
                horas = 8 - (_rnd.Next(2, 4));
            }
            else
            {
                //baja médica
                horas = 0;
            }

            return (Int16)horas;
        }
    }
}
