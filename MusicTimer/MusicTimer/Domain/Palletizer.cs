using System;
using System.Collections.Generic;
using System.Linq;

// ReSharper disable once CheckNamespace
namespace MusicTimer.Domain
{
    public class Palletizer
    {
        private readonly IReadOnlyList<Track> _tracks;
        private readonly int _allowableErrorInSeconds;

        public int DurationInSeconds { get; private set; }

        public Palletizer(IEnumerable<Track> tracks, IEnumerable<Tag> tags = null, int allowableErrorInSeconds = 0)
        {
            if (tags == null)
                _tracks = new List<Track>(tracks);
            else
            {
                var tmpTracks = tracks
                    .Where(track => track.Tags.Intersect(tags).Count() != 0);
                _tracks = new List<Track>(tmpTracks);
            }
            _allowableErrorInSeconds = allowableErrorInSeconds;
            DurationInSeconds = 0;
        }

        public Stack<Track> LayIn(int duration)
        {
            var result = new Stack<Track>();
            var tmpTracks = new List<Track>(_tracks);
            var generator = new Random();
            var errorFlag = false;

            while (true)
            {
                var trackPos = generator.Next(tmpTracks.Count);
                var track = tmpTracks[trackPos];                
                if (DurationInSeconds + track.DurationInSeconds > duration)
                {
                    var currentDurationTracks = tmpTracks
                        .Where(t => t.DurationInSeconds <= duration - DurationInSeconds)
                        .ToList();
                    if (currentDurationTracks.Count == 0)
                    {
                        if (errorFlag) break;
                        duration += _allowableErrorInSeconds;
                        errorFlag = true;
                    }
                    tmpTracks = new List<Track>(currentDurationTracks);
                    continue;
                }
                DurationInSeconds += track.DurationInSeconds;
                result.Push(track);
                tmpTracks.RemoveAt(trackPos);
            }
            return result;
        }
    }
}