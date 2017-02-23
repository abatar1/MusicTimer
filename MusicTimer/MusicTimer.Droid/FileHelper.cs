using System.Collections.Generic;
using System.IO;
using System.Linq;
using Android.Support.V7.View.Menu;
using MusicTimer.Domain.Files;
using MusicTimer.Droid;
using Xamarin.Forms;
using Environment = System.Environment;

[assembly: Dependency(typeof(FileHelper))]
namespace MusicTimer.Droid
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            var docsPath = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            return Path.Combine(docsPath, filename);
        }

        public List<string> GetDirectories(string path)
        {
            var a = Directory.GetDirectories(path, "*", SearchOption.TopDirectoryOnly);
            return new List<string>(a);
        }

        public List<string> GetFilesByFormat(List<string> directories, List<string> formats)
        {
            var result = new List<string>();
            foreach (var path in directories)
            {
                var files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                result.AddRange(files.Where(file => formats.Any(file.Contains)));
            }
            return result;
        }

        public int CountFiles(string path, IEnumerable<string> formats)
        {
            return Directory
                .GetFiles(path, "*", SearchOption.AllDirectories)
                .Count(file => formats.Any(file.Contains));
        }

        public void SaveFile(string path, string content = null)
        {
            File.WriteAllText(path, content);
        }

        public string LoadFile(string path)
        {
            return File.ReadAllText(path);
        }

        public bool FileExists(string filename)
        {
            return File.Exists(GetLocalFilePath(filename));
        }

        public string DefaultMusicFolder()
        {
            return Environment.SpecialFolder.MyMusic.ToString();
        }

        public string PersonalFolder()
        {
            return Environment.SpecialFolder.Personal.ToString();
        }
    }
}