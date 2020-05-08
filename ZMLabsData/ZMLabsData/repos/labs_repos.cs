using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using AutoMapper;

using ZmLabsObjects;
using ZMLabsData.EFModels;

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
                config_map = new MapperConfiguration(cfg => { cfg.CreateMap<CategoriesModel, CategoriesDomain>().ReverseMap();
                                                              cfg.CreateMap<TestsModel, TestDomain>().ReverseMap();
                                                              cfg.CreateMap<TestCasesModel, TestCasesDomain>().ReverseMap();
                                                              cfg.CreateMap<ExecutionsModel, TestCaseExecutionsDomain>().ReverseMap();
                });               
            }

            mapper = new Mapper(config_map);
        }

        public List<CategoriesDomain> getCategories()
        {
            List<CategoriesDomain> res = new List<CategoriesDomain>();

            using (var db = new context.LabsContext(_str_cnx))
            {
                var res_model = db.Categories.ToList();

                res = mapper.Map(res_model, res);
            }

            return res;
        }

        public List<TestDomain> getTests()
        {
            List<TestDomain> res = new List<TestDomain>();

            using (var db = new context.LabsContext(_str_cnx))
            {
                var res_model = db.Test.Include(tc => tc.TestCases).Include(c => c.Categorie).ToList();
                res = mapper.Map(res_model, res);
            }

            return res;
        }

        public bool insertTest(TestDomain Test)
        {
            TestsModel _testmodel = mapper.Map<TestsModel>(Test);

            using (var db = new context.LabsContext(_str_cnx))
            {
                db.Test.Add(_testmodel);
                db.SaveChanges();
            }

            return true;
        }

        public bool insertTestCase(ref TestCasesDomain TestCase)
        {
            TestCasesModel _testcasemodel = mapper.Map<TestCasesModel>(TestCase);

            using (var db = new context.LabsContext(_str_cnx))
            {
                db.TestCases.Add(_testcasemodel);
                db.SaveChanges();
            }

            return true;
        }

        public bool InsertExecution(TestCaseExecutionsDomain _TestCaseExec)
        {
            ExecutionsModel _testexecmodel = mapper.Map<ExecutionsModel>(_TestCaseExec);

            using (var db = new context.LabsContext(_str_cnx))
            {
                db.Executions.Add(_testexecmodel);

                //error!!!!
                //object o2 = null; int i2 = (int)o2;

                db.SaveChanges();
            }

            return true;
        }
    }
}
