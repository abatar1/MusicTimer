using System.Collections.Generic;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace MusicTimer.Domain.Repository
{
    public class TrackStorageController
    {
        private List<Track> _tracks;
        private const string TracksFilename = "tracks";

        private HashSet<Tag> _tags;
        private const string TagsFilename = "tags";

        private List<string> _repositoriesList;
        private List<string> _formatsList;

        public TrackStorageController(List<string> repositoriesList, List<string> formatsList)
        {
            _repositoriesList = repositoriesList;
            _formatsList = formatsList;
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
        }

        public void OnExit()
        {
            SerializeIn(TagsFilename, _tags);
            SerializeIn(TracksFilename, _tracks);
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
