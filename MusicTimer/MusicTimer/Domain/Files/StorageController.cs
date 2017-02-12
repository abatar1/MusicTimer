using System.Collections.Generic;
using MusicTimer.Domain.Models;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MusicTimer.Domain.Files
{
    public class StorageController
    {
        private List<Track> _tracks;
        private const string TracksFilename = "tracks";

        private HashSet<Tag> _tags;
        private const string TagsFilename = "tags";

        private List<Repository> _repositories;
        private const string RepositoriesFilename = "tags";

        private List<string> _formatsList;

        public StorageController(List<string> formatsList)
        {
            _formatsList = formatsList;
        }

        private bool IsNewFileAdded(Repository rep)
        {
            return rep.FileCount == DependencyService.Get<IFileHelper>().CountFiles(rep.Path, _formatsList);
        }

        private void CheckOnChange()
        {
            foreach (var repository in _repositories)
            {
                if (!IsNewFileAdded(repository))
                {
                    
                }
            }
        }

        private static List<T> DeserializeFrom<T>(string name)
        {
            var path = DependencyService.Get<IFileHelper>().GetLocalFilePath(name);
            var content = DependencyService.Get<IFileHelper>().LoadFile(path);
            return JsonConvert.DeserializeObject<List<T>>(content);
        }

        private static void SerializeIn<T>(string name, IEnumerable<T> obj)
        {
            var path = DependencyService.Get<IFileHelper>().GetLocalFilePath(name);
            var content = JsonConvert.SerializeObject(obj);
            DependencyService.Get<IFileHelper>().SaveFile(path, content);
        }

        public void OnLoad()
        {
            _tracks = DeserializeFrom<Track>(TracksFilename);
            _tags = new HashSet<Tag>(DeserializeFrom<Tag>(TagsFilename));
            _repositories = DeserializeFrom<Repository>(RepositoriesFilename);
        }

        public void OnExit()
        {
            SerializeIn(TagsFilename, _tags);
            SerializeIn(TracksFilename, _tracks);
            SerializeIn(RepositoriesFilename, _repositories);
        }

        public void AddTag(Tag tag)
        {
            _tags.Add(tag);
        }

        public void BindTag(Tag tag, Track track)
        {
            _tags.Add(tag);
            _tracks.Find(t => t.Id == track.Id).Tags.Add(tag);
        }

        public List<Tag> Tags()
        {
            return new List<Tag>(_tags);
        }

        public void RemoveTag(Tag tag)
        {
            _tags.Remove(tag);
            foreach (var track in _tracks)
            {
                if (track.Tags.Contains(tag)) track.Tags.Remove(tag);
            }
        }

        public List<Track> Tracks()
        {
            return new List<Track>(_tracks);           
        }

        public void AddTrack(Track track)
        {
            _tracks.Add(track);
        }

        public void RemoveTrack(Track track)
        {
            _tracks.Remove(track);
        }
    }
}
