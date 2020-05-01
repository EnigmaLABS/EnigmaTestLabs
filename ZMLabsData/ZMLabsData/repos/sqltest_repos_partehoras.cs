using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AutoMapper;
using ZmLabsObjects.sqltests;

namespace ZMLabsData.repos
{
    public class sqltest_repos_partehoras
    {
        private string _str_cnx;

        private static MapperConfiguration config_map;
        private Mapper mapper;

        public sqltest_repos_partehoras(string p_str_cnx)
        {
            _str_cnx = p_str_cnx;

            if (config_map == null)
            {
                config_map = new MapperConfiguration(cfg => {
                    cfg.CreateMap<EFModels.testModels.ParteHoras, ParteHoras>().ReverseMap();
                });
            }

            mapper = new Mapper(config_map);
        }

        public bool InsertParteHorasAnual(List<IParteHoras> _ParteAnual)
        {
            try
            {
                List<EFModels.testModels.ParteHoras> _ParteAnualModel = new List<EFModels.testModels.ParteHoras>();

                _ParteAnualModel = mapper.Map(_ParteAnual, _ParteAnualModel);

                using (var db = new context.LabsContext(_str_cnx))
                {
                    db.ParteHoras.AddRange(_ParteAnualModel);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }
    }
}
