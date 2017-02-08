using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTimer.Domain.Database
{
    public class TrackDatabase : Database<Track>
    {
        private class TagInTrackDatabase : Database<TagsInTrack>
        {
            public TagInTrackDatabase() : base("tagintrack.db")
            {
            }

            public Task<List<TagsInTrack>> GetTagsIdAsync(int id)
            {
                return Connection.Table<TagsInTrack>().Where(i => i.TrackId == id).ToListAsync();
            }
        }
       
        private class TagDatabase : Database<Tag>
        {
            public TagDatabase() : base("tag.db")
            {
            }

            public Task<Tag> GetTagAsync(string name)
            {
                return Connection.Table<Tag>().Where(i => i.Name == name).FirstOrDefaultAsync();
            }
        }

        private readonly TagInTrackDatabase _tagInTrackDatabase;
        private readonly TagDatabase _tagDatabase;

        public TrackDatabase() : base("tracks.db")
        {
            _tagInTrackDatabase = new TagInTrackDatabase();
            _tagDatabase = new TagDatabase();
        }

        public Task<Track> GetTrackAsync(int id)
        {
            return Connection.Table<Track>().Where(i => i.Id == id).FirstOrDefaultAsync();
        }

        public Task<List<Tag>> Tags => _tagDatabase.GetItemsAsync();

        public Task<List<Tag>> GetTagsAsync(int id)
        {
            var trackId = GetTrackAsync(id).Result.Id;
            return Task.FromResult(
                _tagInTrackDatabase.GetTagsIdAsync(trackId).Result
                .Select(tt => _tagDatabase.GetTagAsync(tt.TagName).Result)
                .ToList());
        }

        public void AddTrackTagsAsync(int trackId, params Tag[] addingTags)
        {
            foreach (var tag in addingTags)
            {
                _tagDatabase.InsertUpdateData(tag);
                _tagInTrackDatabase.InsertUpdateData(new TagsInTrack(trackId, tag.Name));
            }                                    
        }

        public void AddTagsAsync(params Tag[] addingTags)
        {
            foreach (var tag in addingTags)
            {
                _tagDatabase.InsertUpdateData(tag);
            }
        }

        public void DeleteTrackTagsAsync(int trackId, params Tag[] deletingTags)
        {
            foreach (var tag in deletingTags)
            {
                _tagDatabase.DeleteItemAsync(tag);
                _tagInTrackDatabase.DeleteItemAsync(new TagsInTrack(trackId, tag.Name));
            }
        }

        public void DeleteTagsAsync(params Tag[] deletingTags)
        {
            foreach (var tag in deletingTags)
            {
                _tagDatabase.DeleteItemAsync(tag);
            }
        }
    }
}
