using System.Drawing;
using System.IO;

namespace Utils
{
    public class ImageUtils
    {
        public static bool IsImageExists(string ImagePath)
        {

            return FileUtils.IsFileExists(ImagePath);
        }

        public static bool SaveImage(string CurrentPath , string SaveDirectory , string ImageName)
        {
            string ext = Path.GetExtension(CurrentPath);
            
            ImageName = ImageName + ext; 

            string SavePath = Path.Combine(SaveDirectory, ImageName);

            return FileUtils.CopyFile(CurrentPath, SavePath);
        }

        public static string SaveImageAndReturnPath(string CurrentPath, string SaveDirectory, string ImageName)
        {
            string ext = Path.GetExtension(CurrentPath);

            ImageName = ImageName +ext;

            string SavePath = Path.Combine(SaveDirectory, ImageName);

            if(FileUtils.CopyFile(CurrentPath, SavePath)) return SavePath;

            return string.Empty;
        }

        public static bool DeleteImage(string Path)
        {
            
            return FileUtils.DeleteFile(Path);
        }


    }
}
