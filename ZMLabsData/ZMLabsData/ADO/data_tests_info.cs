﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

using ZmLabsObjects;

namespace ZMLabsData
{
    public class data_tests_info : contracts.ITestRepository
    {
        private string str_cnx;
        private ZmLabsObjects.contracts.ITestFunctionsDomain TestFunctions;

        public data_tests_info(string p_str_cnx, ZmLabsObjects.contracts.ITestFunctionsDomain p_TestFunctions)
        {
            str_cnx = p_str_cnx;
            TestFunctions = p_TestFunctions;
        }

        public List<CategoriesDomain> getCategories()
        {
            List<CategoriesDomain> res = new List<CategoriesDomain>();

            SqlConnection cnx = new SqlConnection(str_cnx);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cnx;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "GetCategories";

            cnx.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                CategoriesDomain _cat = new CategoriesDomain(TestFunctions)
                {
                    id = int.Parse(reader["idCategorie"].ToString()),
                    Categorie = reader["Categorie"].ToString(),
                };

                if (reader["idCategorieNode"] != DBNull.Value)
                {
                    _cat.Categorie_dad = new CategoriesDomain(TestFunctions)
                    {
                        id = int.Parse(reader["idCategorieNode"].ToString()) 
                    };
                }

                res.Add(_cat);
            }

            cnx.Close();

            return res;
        }

        public List<TestDomain> getTests()
        {
            List<TestDomain> res = new List<TestDomain>();

            SqlConnection cnx = new SqlConnection(str_cnx);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cnx;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "getTests";

            cnx.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TestDomain _test = new TestDomain(TestFunctions)
                {
                    id = int.Parse(reader["idTest"].ToString()),
                    Test = reader["Test"].ToString(),
                    Classname = reader["ClassName"].ToString(),
                    Description = reader["Description"].ToString(),
                    Url_blog = reader["Url_Blog"].ToString(),
                    Url_git = reader["Url_GIT"].ToString(),

                    Categorie = new CategoriesDomain(TestFunctions)
                    {
                        id = int.Parse(reader["idCategorie"].ToString()),
                        Categorie = reader["Categorie"].ToString()
                    }
                };

                res.Add(_test);
            }

            cnx.Close();

            foreach (TestDomain _test in res)
            {
                _test.TestCases = getTestCases(_test.id);
            }

            return res;
        }

        public bool insertTest(TestDomain _test)
        {
            SqlConnection cnx = new SqlConnection(str_cnx);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cnx;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertTest";

            cmd.Parameters.AddWithValue("@Test", _test.Test);
            cmd.Parameters.AddWithValue("@ClassName", _test.Classname);
            cmd.Parameters.AddWithValue("@Description", _test.Description);
            cmd.Parameters.AddWithValue("@idCategorie", _test.idCategorie);

            cnx.Open();
            cmd.ExecuteNonQuery();
            cnx.Close();

            return true;
        }

        public bool insertTestCase(ref TestCasesDomain testCase)
        {
            Int64 res = 0;

            SqlConnection cnx = new SqlConnection(str_cnx);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cnx;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertTestCase";

            cmd.Parameters.AddWithValue("@idTest", testCase.idTest);
            cmd.Parameters.AddWithValue("@FunctionName", testCase.Function);
            cmd.Parameters.AddWithValue("@Description", testCase.Description);

            cnx.Open();
            res = (Int64)cmd.ExecuteScalar();
            cnx.Close();

            testCase.id = res;

            return true;
        }

        public bool InsertExecution(TestCaseExecutionsDomain _TestCaseExec)
        {
            SqlConnection cnx = new SqlConnection(str_cnx);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cnx;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "insertExecution";

            cmd.Parameters.AddWithValue("@dtStart", _TestCaseExec.dtBegin);
            cmd.Parameters.AddWithValue("@dtEnd", _TestCaseExec.dtEnd);
            cmd.Parameters.AddWithValue("@idTestCase", _TestCaseExec.idTestCase);

            cnx.Open();
            cmd.ExecuteNonQuery();
            cnx.Close();

            return true;
        }

        //-->>
        #region Privados

        private List<TestCasesDomain>  getTestCases(Int64 id)
        {
            List<TestCasesDomain> res = new List<TestCasesDomain>();
            
            SqlConnection cnx = new SqlConnection(str_cnx);
            SqlCommand cmd = new SqlCommand();

            cmd.Connection = cnx;
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.CommandText = "getTestCases";

            cmd.Parameters.AddWithValue("idTest", id);

            cnx.Open();

            SqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                TestCasesDomain _testcase = new TestCasesDomain()
                {
                    id = Int64.Parse(reader["idTestCase"].ToString()),
                    Function = reader["FunctionName"].ToString(),
                    Description = reader["Description"].ToString(),
                    Orden = int.Parse(reader["Orden"].ToString())
                };

                res.Add(_testcase);
            }

            cnx.Close();

            return res;
        }

        #endregion
    }
}
