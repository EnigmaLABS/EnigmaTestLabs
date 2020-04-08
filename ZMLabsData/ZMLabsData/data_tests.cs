﻿using System;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;

using ZmLabsObjects;

namespace ZMLabsData
{
    public class data_tests
    {
        //private static string str_cnx = ConfigurationManager.ConnectionStrings["cnxLABS_DB_STR"].ConnectionString;

        private string str_cnx;

        public data_tests(string p_str_cnx)
        {
            str_cnx = p_str_cnx;
        }

        public List<Categories> getCategories()
        {
            List<Categories> res = new List<Categories>();

            try
            {
                SqlConnection cnx = new SqlConnection(str_cnx);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "GetCategories";

                cnx.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    Categories _cat = new Categories()
                    {
                        id = int.Parse(reader["idCategorie"].ToString()),
                        categorie = reader["Categorie"].ToString()
                    };

                    if (reader["idCategorieNode"] != DBNull.Value)
                    {
                        _cat.categorie_dad = new Categories()
                        {
                            id = int.Parse(reader["idCategorieNode"].ToString()) 
                        };
                    }

                    res.Add(_cat);
                }

                cnx.Close();
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        public List<test_object> getTests()
        {
            List<test_object> res = new List<test_object>();

            try
            {
                SqlConnection cnx = new SqlConnection(str_cnx);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "getTests";

                cnx.Open();

                SqlDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    test_object _test = new test_object()
                    {
                        id = int.Parse(reader["idTest"].ToString()),
                        test = reader["Test"].ToString(),
                        classname = reader["ClassName"].ToString(),
                        description = reader["Description"].ToString(),
                        url_blog = reader["UrlBlog"].ToString(),
                        url_git = reader["UrlGIT"].ToString(),

                        categorie = new Categories()
                        {
                            id = int.Parse(reader["idCategorie"].ToString()),
                            categorie = reader["Categorie"].ToString()
                        }
                   };

                    res.Add(_test);
                }

                cnx.Close();

                foreach (test_object _test in res)
                {
                    _test.execution.testcases = getTestCases(_test.id);
                }
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        public bool insertTest(string test, string classname, string desc, int idCategorie)
        {
            try
            {
                SqlConnection cnx = new SqlConnection(str_cnx);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insertTest";

                cmd.Parameters.AddWithValue("@Test", test);
                cmd.Parameters.AddWithValue("@ClassName", classname);
                cmd.Parameters.AddWithValue("@Description", desc);
                cmd.Parameters.AddWithValue("@idCategorie", idCategorie);

                cnx.Open();
                cmd.ExecuteNonQuery();
                cnx.Close();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public Int64 insertTestCase(Int64 idTest, string functionName, string Description)
        {
            Int64 res = 0;

            try
            {
                SqlConnection cnx = new SqlConnection(str_cnx);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insertTestCase";

                cmd.Parameters.AddWithValue("@idTest", idTest);
                cmd.Parameters.AddWithValue("@FunctionName", functionName);
                cmd.Parameters.AddWithValue("@Description", Description);

                cnx.Open();
                res = (Int64)cmd.ExecuteScalar();
                cnx.Close();
            }
            catch (Exception ex)
            {
                return 0;
            }

            return res;
        }

        public bool InsertExecution(TestCases _TestCase)
        {
            try
            {
                SqlConnection cnx = new SqlConnection(str_cnx);
                SqlCommand cmd = new SqlCommand();

                cmd.Connection = cnx;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "insertExecution";

                cmd.Parameters.AddWithValue("@dtStart", _TestCase.dtBegin);
                cmd.Parameters.AddWithValue("@dtEnd", _TestCase.dtEnd);
                cmd.Parameters.AddWithValue("@idTestCase", _TestCase.id);

                cnx.Open();
                cmd.ExecuteNonQuery();
                cnx.Close();
            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        //-->>
        #region Privados

        private List<TestCases>  getTestCases(Int64 id)
        {
            List<TestCases> res = new List<TestCases>();
            
            try
            {
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
                    TestCases _testcase = new TestCases()
                    {
                        id = Int64.Parse(reader["idTestCase"].ToString()),
                        Function = reader["FunctionName"].ToString(),
                        Description = reader["Description"].ToString()
                    };

                    res.Add(_testcase);
                }

                cnx.Close();
            }
            catch (Exception ex)
            {

            }

            return res;
        }

        #endregion
    }
}