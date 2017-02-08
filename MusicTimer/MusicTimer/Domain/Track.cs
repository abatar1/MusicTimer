using System;
using System.Collections.Generic;
using SQLite;

namespace MusicTimer.Domain
{
    public class Track : IData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        [Ignore]
        public TimeSpan Duration { get; }

        public string Name { get; }

        public string FullName { get; }

        [Ignore]
        public IReadOnlyCollection<Tag> Tags { get; }      

        public Track(TimeSpan duration, string name, string fullName, IEnumerable<Tag> tags, int id)
        {
            Duration = duration;
            Name = name;
            FullName = fullName;
            Tags = new List<Tag>(tags);
            Id = id;
        }

        public Track()
        {
            
        }
    }
}
