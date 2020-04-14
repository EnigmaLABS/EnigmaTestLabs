using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;
using ZmLabsObjects;

namespace ZMLabsData.repos
{
    public class labs_repos
    {
        private string _str_cnx;

        private static MapperConfiguration config_map;
        private Mapper mapper;

        public labs_repos(string p_str_cnx)
        {
            _str_cnx = p_str_cnx;

            if (config_map == null)
            {
                config_map = new MapperConfiguration(cfg => { cfg.CreateMap<EFModels.Categories, Categories>().ReverseMap();
                                                              cfg.CreateMap<EFModels.Tests, test_object>().ReverseMap();
                                                              cfg.CreateMap<EFModels.TestCases, TestCases>().ReverseMap();
                                                            });

                
            }

            mapper = new Mapper(config_map);
        }

        public List<Categories> getCategories()
        {
            List<Categories> res = new List<Categories>();

            using (var db = new context.LabsContext(_str_cnx))
            {
                var res_model = db.Categories.ToList();

                res = mapper.Map(res_model, res);
            }

            return res;
        }

        public List<test_object> getTests()
        {
            List<test_object> res = new List<test_object>();

            using (var db = new context.LabsContext(_str_cnx))
            {
                var res_model = db.Test.Include(tc => tc.TestCases).Include(c => c.Categorie).ToList();
                res = mapper.Map(res_model, res);
            }

            return res;
        }

        public bool insertTest(test_object Test)
        {
            try
            {
                EFModels.Tests _testmodel = mapper.Map<EFModels.Tests>(Test);

                using (var db = new context.LabsContext(_str_cnx))
                {
                    db.Test.Add(_testmodel);
                    db.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public bool insertTestCase(ref TestCases TestCase)
        {
            try
            {
                EFModels.TestCases _testcasemodel = mapper.Map<EFModels.TestCases>(TestCase);

                using (var db = new context.LabsContext(_str_cnx))
                {
                    db.TestCases.Add(_testcasemodel);
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
