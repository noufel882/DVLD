using Business_Logic;
using Microsoft.Win32;
using System;
using System.IO;
using System.Windows.Forms;

namespace Application_Layer.Global_Classes
{
    internal class Global
    {
        public static User CurrentUser;

        public static bool SaveUsernameAndPassword(string Username, string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD\LOGIN";

            string valueName = "CURRENT_USER_LOGIN_INFO";
            try
            {
                string valueData;
                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password))
                    return false;

                else valueData = Username + "##//##" + Password;

                Registry.SetValue(keyPath, valueName, valueData, RegistryValueKind.String);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public static bool GetStoredCredential(ref string Username, ref string Password)
        {
            string keyPath = @"HKEY_CURRENT_USER\Software\DVLD\LOGIN";

            string valueName = "CURRENT_USER_LOGIN_INFO";
            try
            {
                var valueData = Registry.GetValue(keyPath, valueName, null) as string;

                if (valueData == null)
                {
                    return false;
                }

                string[] loginInfos = valueData.Split(new string[] { "##//##" }, StringSplitOptions.None);

                if (loginInfos.Length < 0)
                {
                    return false;
                }

                Username = loginInfos[0];
                Password = loginInfos[1];

            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
