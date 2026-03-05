using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Utils
{
    public class Log
    {
        private static void LogException(string text ,string SaveLocation)
        {
            FileUtils.SaveTextInFile(text,SaveLocation);
        }

        public static void LogException(Exception ex, string FunctionName,string FunctionLocation, string path)
        {
            string message =
$@"{Environment.NewLine}
-------------------------------------------
Date     : {DateTime.Now:dd/MM/yyyy hh:mm:ss} 
-------------------------------------------
sender   : {FunctionName}
location : {FunctionLocation}
Message  : {ex.Message}

{Environment.NewLine}";


            Utils.Log.LogException(message, path);
        }
    }
}
