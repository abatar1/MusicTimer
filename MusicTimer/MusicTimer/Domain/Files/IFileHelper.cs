using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTimer.Domain.Files
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
        List<string> GetFilesByFormat(List<string> directories, List<string> formats);
        bool FileExists(string filename);
        void SaveFile(string path, string content = null);
        string LoadFile(string path);
        int CountFiles(string path, List<string> formats);
    }
}
