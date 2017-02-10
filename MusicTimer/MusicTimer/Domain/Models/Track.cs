using System;
using System.Collections.Generic;

namespace MusicTimer.Domain
{
    public class Track
    {
        public string Id { get; private set; }

        public string Name { get; private set; }

        public TimeSpan Duration { get; private set; }       

        public HashSet<Tag> Tags { get; private set; }

        public Track(string name, TimeSpan duration, HashSet<Tag> tags)
        {
            Id = Guid.NewGuid().ToString();
            Duration = duration;
            Name = name;
            Tags = tags;
        }
    }
}
