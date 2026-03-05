using System.IO;
using Business_Logic;
using System;
using System.Windows.Forms;

namespace Application_Layer.Global_Classes
{
    internal class Global
    {
        public static User CurrentUser;

        public static bool SaveUsernameAndPassword(string Username, string Password)
        {
            try
            {
                string ProjectDirectory = Directory.GetCurrentDirectory();
                string FullPath = Path.Combine(ProjectDirectory, "data.txt");

                if (Username.Trim() == "" && File.Exists(FullPath))
                {
                    File.Delete(FullPath);
                    return true;
                }

                string UserInfo = Username.Trim() +"#//#"+ Password.Trim();

                using(StreamWriter writer = new StreamWriter(FullPath))
                {
                    writer.Write(UserInfo);
                    return true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error : {ex.Message}");
                return false;
            }

           
        }

        public static bool GetStoredCredential(ref string Username , ref string Password)
        {
            try
            {
                string ProjectDirectory = Directory.GetCurrentDirectory();
                string FullPath = Path.Combine(ProjectDirectory, "data.txt");

                if (File.Exists(FullPath))
                {
                    using (StreamReader reader = new StreamReader(FullPath))
                    {
                        string Line ;

                        while((Line =reader.ReadLine()) != null)
                        {
                            Console.WriteLine(Line);
                            string[] LoginInfos = Line.Split(new string[]{ "#//#" } , StringSplitOptions.None);
                            Username = LoginInfos[0];
                            Password = LoginInfos[1];
                        }
                        return true;
                    }

                }
                else return false;

            }
            catch(Exception ex)
            {

                MessageBox.Show($"Error : {ex.Message}");
                return false;
            }

        }


    }
}
