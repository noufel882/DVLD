using System;
using System.IO;

namespace Utils
{
    public class FileUtils
    {
        public static bool IsFileExists(string FilePath)
        {
            if (string.IsNullOrEmpty(FilePath)) return false;
            return File.Exists(FilePath);
        }

        public static void SaveTextInFile(string text, string FullPath)
        {
            string directoryPath = Path.GetDirectoryName(FullPath);
            Directory.CreateDirectory(directoryPath);
            File.AppendAllText(FullPath, text);
        }

        public static bool MakeDirectory(string FullPath)
        {
            string dir = Path.GetDirectoryName(FullPath);
            if (string.IsNullOrEmpty(dir))
            {
                return false;
            }

            Directory.CreateDirectory(dir);
            return true;
        }

        public static bool CopyFile(string CurrentPath, string SavePath)
        {
            if (!IsFileExists(CurrentPath))
            {
                return false;
            }
                      
            if (!MakeDirectory(SavePath))
            {
                return false;
            }

            
            try
            {
                File.Copy(CurrentPath, SavePath,true);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool DeleteFile(string FilePath)
        {
            if (!IsFileExists(FilePath))
            {
                return true;
            }
            try
            {
                File.Delete(FilePath);               
                return true;
            }
            catch
            {
                return false;
            }
        }

        
    }
}
