using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZmLabsBusiness.registry
{
    public static class registry_functions
    {
        public static bool ExisteBBDD()
        {
            RegistryKey rk1 = Registry.LocalMachine;

            RegistryKey rkSoftware = rk1.OpenSubKey("SOFTWARE", true);

            RegistryKey rk_enigma = rkSoftware.OpenSubKey("EnigmaSoft", true);

            if (rk_enigma == null)
            {
                RegistryKey rk2 = rkSoftware.CreateSubKey("EnigmaSoft");

                rk2.SetValue("BBDDCreated", "N", RegistryValueKind.String);

                return false;
            }
            else
            {
                string valor = rk_enigma.GetValue("BBDDCreated").ToString();

                if (valor != "Y")
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        public static bool SetBBDDCreada(string Server)
        {
            try
            {
                RegistryKey rk1 = Registry.LocalMachine;

                RegistryKey rkSoftware = rk1.OpenSubKey("SOFTWARE", true);

                RegistryKey rk_enigma = rkSoftware.OpenSubKey("EnigmaSoft", true);

                rk_enigma.SetValue("BBDDCreated", "Y", RegistryValueKind.String);
                rk_enigma.SetValue("Server", Server, RegistryValueKind.String);

            }
            catch (Exception ex)
            {
                return false;
            }

            return true;
        }

        public static string GetRegisteredServer()
        {
            string res = "";

            try
            {
                RegistryKey rk1 = Registry.LocalMachine;

                RegistryKey rkSoftware = rk1.OpenSubKey("SOFTWARE", true);

                RegistryKey rk_enigma = rkSoftware.OpenSubKey("EnigmaSoft", true);

                if (rk_enigma == null)
                {
                    res = "";
                }
                else
                {
                    res = rk_enigma.GetValue("Server").ToString();
                }
            }
            catch (Exception ex)
            {

            }

            return res;
        }
    }
}
