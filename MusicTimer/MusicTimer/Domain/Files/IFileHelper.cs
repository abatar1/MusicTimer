﻿using System.Collections.Generic;

namespace MusicTimer.Domain.Files
{
    public interface IFileHelper
    {
        string GetLocalFilePath(string filename);
        List<string> GetFilesByFormat(List<string> directories, List<string> formats);
        bool FileExists(string filename);
        void SaveFile(string path, string content = null);
        string LoadFile(string path);
        int CountFiles(string path, IEnumerable<string> formats);
        string DefaultMusicFolder();
        string PersonalFolder();
        List<string> GetDirectories(string path);
    }
}
