using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;

using ZmLabsObjects.DTO;
using ZmLabsObjects.sqltests;
using ZMLabsData.EFModels.testModels;

namespace ZMLabsData.repos
{
    public class sqltest_repos_partehoras : contracts.IParteHorasRepository
    {
        private string _str_cnx;

        private static MapperConfiguration config_map;
        private Mapper mapper;

        public sqltest_repos_partehoras(string p_str_cnx)
        {
            _str_cnx = p_str_cnx;

            if (config_map == null)
            {
                config_map = new MapperConfiguration(cfg =>
                {
                    cfg.CreateMap<ParteHorasModel, ParteHorasDomain>().ReverseMap();
                });
            }

            mapper = new Mapper(config_map);
        }

        public bool InsertParteHorasAnualEF(List<ParteHorasDomain> _ParteAnual)
        {
            List<ParteHorasModel> _ParteAnualModel = new List<ParteHorasModel>();

            _ParteAnualModel = mapper.Map(_ParteAnual, _ParteAnualModel);

            using (var db = new context.LabsContext(_str_cnx))
            {
                db.ParteHoras.AddRange(_ParteAnualModel);
                db.SaveChanges();
            }

            return true;
        }

        public bool InsertParteHorasAnualADO(DataTable _tblParteAnual)
        {
            throw new NotImplementedException();
        }

        public List<InformeAbsentismoDTO> GetInformeAbsentismoAnual (int anho)
        {
            List<InformeAbsentismoDTO> res = new List<InformeAbsentismoDTO>();

            using (var db = new context.LabsContext(_str_cnx))
            {
                res = db.ParteHoras
                .Where(an => an.Anho == anho)
                .GroupBy(ph => ph.Trabajador)
                .Select(inf => new InformeAbsentismoDTO
                {
                    Trabajador = inf.FirstOrDefault().Trabajador,
                    conteo_registros = inf.Count(),
                    suma_horas = inf.Sum(h => h.Horas)

                }).ToList();
            }

            return res;
        }
    }
}
