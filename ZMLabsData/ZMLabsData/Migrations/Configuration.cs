namespace ZMLabsData.Migrations
{
    using System;
    using System.Data.Entity;
    using SqlProviderServices = System.Data.Entity.SqlServer.SqlProviderServices;
    using System.Data.Entity.Migrations;
    using System.Linq;


    public sealed class Configuration : DbMigrationsConfiguration<ZMLabsData.context.LabsContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        public void CreateOrUpdateDataBase(bool Update, string cnx_str)
        {
            context.LabsContext _myContext = new context.LabsContext(cnx_str);

            if (Update)
            {
                if (!_myContext.Database.CompatibleWithModel(false))
                {
                    Database.SetInitializer(new MigrateDatabaseToLatestVersion<context.LabsContext, Configuration>());

                    using (var ctx = new context.LabsContext(cnx_str))
                    {
                        ctx.Database.Initialize(true);
                    }
                }
            }
            else
            {
                _myContext.Database.Create();
                Seed(_myContext);
            }
        }

        protected override void Seed(context.LabsContext context)
        {
            //Categorías

            //Ramas de nivel 1
            EFModels.Categories _cat_sqlserver = new EFModels.Categories()
            {
                id = 1,
                Categorie = "SQL Server Tips"
            };

            EFModels.Categories _cat_csharpr = new EFModels.Categories()
            {
                id = 2,
                Categorie = "C# Tips"
            };

            context.Categories.AddOrUpdate(x => x.id, _cat_sqlserver);
            context.Categories.AddOrUpdate(x => x.id, _cat_csharpr);

            //Ramas de nivel 2
            EFModels.Categories _cat_Multithreading = new EFModels.Categories()
            {
                id = 3,
                Categorie = "Multithreading Tests",
                Categorie_dad = _cat_csharpr
            };

            EFModels.Categories _cat_Basics = new EFModels.Categories()
            {
                id = 4,
                Categorie = "Basics Tips",
                Categorie_dad = _cat_csharpr
            };

            EFModels.Categories _cat_DataLayer = new EFModels.Categories()
            {
                id = 5,
                Categorie = "Data Layer",
                Categorie_dad = _cat_sqlserver
            };

            context.Categories.AddOrUpdate(x => x.id, _cat_Multithreading);
            context.Categories.AddOrUpdate(x => x.id, _cat_Basics);
            context.Categories.AddOrUpdate(x => x.id, _cat_DataLayer);

            context.SaveChanges();

            //Tests
            EFModels.Tests _test1 = new EFModels.Tests()
            {
                id = 1,
                Test = "Multithreading vs Singlethreading",
                Classname = "test1_multithreading_vs_singlethreading",
                Description = "Calcula 500 veces 200 elementos de la serie fibonacci",
                Url_Blog = @"https://enigmasoftwarelabs.blogspot.com/2020/04/test-1-multithreading-vs-singlethreading.html",
                Url_Git = "",
                Url_Stackoverflow = @"https://stackoverflow.com/questions/12390468/multithreading-slower-than-singlethreading",

                Categorie = _cat_Multithreading,
                idCategorie = _cat_Multithreading.id
            };

            EFModels.Tests _test2 = new EFModels.Tests()
            {
                id = 2,
                Test = "Concatenate Strings",
                Classname = "test2_basicos_concatstrings",
                Description = "Plus Operator Vs StringBuilder",
                Url_Blog = @"",
                Url_Git = "",

                Categorie = _cat_Basics,
                idCategorie = _cat_Basics.id
            };

            EFModels.Tests _test3 = new EFModels.Tests()
            {
                id = 3,
                Test = "Bulk Data - Store Procedure vs Entity Framework",
                Classname = "test3_sql_loaddata",
                Description = "Generación de 26.200 registros de información para posteriormente, consolidarla en base de datos mediante distintas técnicas ",
                Url_Blog = @"https://enigmasoftwarelabs.blogspot.com/2020/04/test-2-concatenacion-de-strings.html",
                Url_Git = "",

                Categorie = _cat_DataLayer,
                idCategorie = _cat_DataLayer.id
            };

            context.Test.AddOrUpdate(t => t.id, _test1);
            context.Test.AddOrUpdate(t => t.id, _test2);
            context.Test.AddOrUpdate(t => t.id, _test3);

            context.SaveChanges();

            //TestCases - Test1
            EFModels.TestCases _test1_case1 = new EFModels.TestCases()
            {
                id = 1,
                Function = "MultithreadingCase",
                Description = "Cálculo simultáneo de la serie fibo (500 hilos, 200 elementos por hilo)",

                Test = _test1,
                idTest = _test1.id
            };
            EFModels.TestCases _test1_case2 = new EFModels.TestCases()
            {
                id = 2,
                Function = "SinglethreadingCase",
                Description = "Cálculo secuencial de la serie fibo (500 iteraciones, 200 elementos por iteración)",

                Test = _test1,
                idTest = _test1.id
            };
            EFModels.TestCases _test1_case3 = new EFModels.TestCases()
            {
                id = 3,
                Function = "HybridCase",
                Description = "20 hilos calculan 25 veces cada uno la serie fibo",

                Test = _test1,
                idTest = _test1.id
            };

            //TestCases - Test2
            EFModels.TestCases _test2_case1 = new EFModels.TestCases()
            {
                id = 4,
                Function = "Concat_PlusOperator",
                Description = "Concatenación con operador + Concatena 100 veces 26 variables string con el operador",
                
                Test = _test2,
                idTest = _test2.id
            };
            EFModels.TestCases _test2_case2 = new EFModels.TestCases()
            {
                id = 5,
                Function = "Concat_StringBuilder",
                Description = "Concatenación con StringBuilder: Concatena 100 veces 26 variables string con un StringBuilder",

                Test = _test2,
                idTest = _test2.id
            };

            //TestCases - Test3
            EFModels.TestCases _test3_case1 = new EFModels.TestCases()
            {
                id = 6,
                Function = "EFBulkData",
                Description = "Grabación de parte de horas anual para 100 trabajadores con Entity Framework",

                Test = _test3,
                idTest = _test3.id
            };
            EFModels.TestCases _test3_case2 = new EFModels.TestCases()
            {
                id = 7,
                Function = "ADOBulkData_Datatable",
                Description = @"Grabación de parte de horas anual para 100 trabajadores con ADO.NET convirtiendo con reflection la clase Parte_Horas en un DataTable que recibe un procedimiento almacenado como parámetro",

                Test = _test3,
                idTest = _test3.id
            };


            context.TestCases.AddOrUpdate(tc => tc.id, _test1_case1);
            context.TestCases.AddOrUpdate(tc => tc.id, _test1_case2);
            context.TestCases.AddOrUpdate(tc => tc.id, _test1_case3);

            context.TestCases.AddOrUpdate(tc => tc.id, _test2_case1);
            context.TestCases.AddOrUpdate(tc => tc.id, _test2_case2);

            context.TestCases.AddOrUpdate(tc => tc.id, _test3_case1);
            context.TestCases.AddOrUpdate(tc => tc.id, _test3_case2);

            context.SaveChanges();
        }
    }
}
