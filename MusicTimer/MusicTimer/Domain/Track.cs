using System.Collections.Generic;

// ReSharper disable once CheckNamespace
namespace MusicTimer.Domain
{
    public class Track
    {
        public int DurationInSeconds { get; }
        public string Name { get; }
        public string FullName { get; }
        public IReadOnlyCollection<Tag> Tags { get; }
        public string Id { get; }

        public Track(int duration, string name, string fullName, IEnumerable<Tag> tags, string id)
        {
            DurationInSeconds = duration;
            Name = name;
            FullName = fullName;
            Tags = new List<Tag>(tags);
            Id = id;
        }
    }
}
