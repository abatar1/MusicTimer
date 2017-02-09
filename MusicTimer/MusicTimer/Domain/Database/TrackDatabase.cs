using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MusicTimer.Domain.Database
{
    public class TrackDatabase : Database<Track>
    {
        private const string ttDatabaseName = "tagintrack.db";
        private const string tagDatabaseName = "tags.db";
        private const string trackDatabaseName = "tracks.db";

        private class TagInTrackDatabase : Database<TagsInTrack>
        {
            public TagInTrackDatabase() : base(ttDatabaseName)
            {
            }

            public Task<List<TagsInTrack>> GetTagsIdAsync(int id)
            {
                return Connection.Table<TagsInTrack>().Where(i => i.TrackId == id).ToListAsync();
            }

            public void RemoveTagsById(int id)
            {
                foreach (var item in Connection.Table<TagsInTrack>().Where(i => i.TrackId == id).ToListAsync().Result)
                {
                    Connection.DeleteAsync(item);
                }
            }

            public void RemoveTag(Tag tag)
            {
                foreach (var item in Connection.Table<TagsInTrack>().Where(i => i.TagName == tag.Name).ToListAsync().Result)
                {
                    Connection.DeleteAsync(item);
                }
            }
        }
       
        private class TagDatabase : Database<Tag>
        {
            public TagDatabase() : base(tagDatabaseName)
            {
            }

            public Task<Tag> GetTagAsync(string name)
            {
                return Connection.Table<Tag>().Where(i => i.Name == name).FirstOrDefaultAsync();
            }
        }

        private readonly TagInTrackDatabase _tagInTrackDatabase;
        private readonly TagDatabase _tagDatabase;

        public TrackDatabase() : base(trackDatabaseName)
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

        public void RemoveTagAsync(Tag tag, int? trackId = null)
        {
            if (trackId != null)
            {
                _tagDatabase.DeleteItemAsync(tag);
                _tagInTrackDatabase.DeleteItemAsync(new TagsInTrack(trackId, tag.Name));
            }
            else
            {
                _tagDatabase.DeleteItemAsync(tag);
                _tagInTrackDatabase.RemoveTag(tag);
            }               
        }

        public void AddTagAsync(Tag tag, int? trackId = null)
        {
            _tagDatabase.InsertUpdateData(tag);
            if (trackId != null) _tagInTrackDatabase.InsertUpdateData(new TagsInTrack(trackId, tag.Name));                                              
        }      
    }
}
