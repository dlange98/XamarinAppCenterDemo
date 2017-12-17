using System;
using System.IO;
using Xamarin.Forms;
using Refapp.Services.Droid;
//using SQLite.Net.Interop;
using Refapp.Managers;

[assembly: Dependency(typeof(FileManager))]
namespace Refapp.Services.Droid
{
    public class FileManager : IFileManager
    {
        public FileManager()
        {
        }


        public string GetFileNameFromPath(string path)
        {
            var fileName = path;
            string[] parts = path.Split(Path.DirectorySeparatorChar);
            if (parts.Length > 0)
            {
                fileName = parts[parts.Length - 1];
            }

            return fileName;
        }


        public Stream GetStream(string fullPath)
        {
            FileStream fs = new FileStream(fullPath, FileMode.Open, FileAccess.Read);
            return fs;
        }


        public string GetDBPath(string dbName)
        {
            string documentsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal); // Documents folder
            var path = Path.Combine(documentsPath, dbName);
            System.Diagnostics.Debug.WriteLine(path);
            return path;
        }

        //public ISQLitePlatform GetSQLPlatform()
        //{
        //    return new SQLitePlatformAndroid();
        //}
    }
}
