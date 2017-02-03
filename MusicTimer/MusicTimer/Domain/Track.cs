using System;
using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace MusicTimer.Domain
{
    public class Track
    {
        public TimeSpan Duration { get; }
        public string Name { get; }
        public string FullName { get; }
        public IReadOnlyCollection<Tag> Tags { get; }
        public string Id { get; }

        public Track(TimeSpan duration, string name, string fullName, IEnumerable<Tag> tags, string id)
        {
            Duration = duration;
            Name = name;
            FullName = fullName;
            Tags = new List<Tag>(tags);
            Id = id;
        }
    }
}
