using System;
using Microsoft.Win32;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Configuration;

namespace ZMLabsData.context
{
    public class MyContextFactory : IDbContextFactory<LabsContext>
    {
        public LabsContext Create()
        {
            return new LabsContext(MyCnxBuilder.GetCnx());
        }
    }

    public class LabsContext : DbContext
    {
        public LabsContext(string cnx_str) : base(cnx_str) {  }

        public DbSet<EFModels.CategoriesModel> Categories { get; set; }
        public DbSet<EFModels.TestsModel> Test { get; set; }
        public DbSet<EFModels.TestCasesModel> TestCases { get; set; }
        public DbSet<EFModels.ExecutionsModel> Executions { get; set; }

        //Modelos de test
        public DbSet<EFModels.testModels.ParteHorasModel> ParteHoras { get; set; }
    }

    public static class MyCnxBuilder
    {
        public static string GetCnx()
        {
            string cnx = "";

            try
            {
                RegistryKey rk1 = Registry.LocalMachine;

                RegistryKey rkSoftware = rk1.OpenSubKey("SOFTWARE", true);

                RegistryKey rk_enigma = rkSoftware.OpenSubKey("EnigmaSoft", true);

                string server = rk_enigma.GetValue("Server").ToString();
                cnx = ConfigurationManager.ConnectionStrings["cnxLABS_DB_STR_EF"].ConnectionString;

                cnx = cnx.Replace("##SERVER##", server);

            }
            catch (Exception ex)
            {
                return "";
            }

            return cnx;
        }
    }
}
