using System;
using System.Collections.Generic;
using System.Linq;

namespace MusicTimer.Domain
{
    public class Palletizer
    {
        public TimeSpan Duration { get; private set; }
        public List<Tag> Tags { get; }
        public List<Track> Tracks { get; }

        public Palletizer(IEnumerable<Track> tracks = null, IEnumerable<Tag> tags = null)
        {
            if (tracks == null && tags == null)
            {
                Tracks = new List<Track>();
                Tags = new List<Tag>();
            }
            else if (tags == null)
            {
                Tracks = new List<Track>(tracks);
                Tags = new List<Tag>();
            }              
            else
            {
                var tmpTracks = tracks
                    .Where(track => track.Tags.Intersect(tags).Count() != 0);
                Tracks = new List<Track>(tmpTracks);
                Tags = new List<Tag>(tags);
            }          
            Duration = new TimeSpan(0, 0, 0);
        }

        public Stack<Track> LayIn(TimeSpan fullDuration)
        {
            var result = new Stack<Track>();
            var tmpTracks = new List<Track>(Tracks);
            var generator = new Random();

            while (true)
            {
                var trackPos = generator.Next(tmpTracks.Count);
                var track = tmpTracks[trackPos];   
                // TODO ������� ���� �������� ������. �������� ����� �������� ����� ���������� �����, �� ��� ������ �������            
                if (Duration + track.Duration > fullDuration)
                {
                    var currentDurationTracks = tmpTracks
                        .Where(t => t.Duration <= fullDuration - Duration)
                        .ToList();
                    if (currentDurationTracks.Count == 0) break;
                    tmpTracks = new List<Track>(currentDurationTracks);
                    continue;
                }
                Duration += track.Duration;
                result.Push(track);
                tmpTracks.RemoveAt(trackPos);
            }
            return result;
        }
    }
}