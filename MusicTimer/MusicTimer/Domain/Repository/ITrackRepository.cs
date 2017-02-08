using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MusicTimer.Domain;

namespace MusicTimer.Domain
{
    public interface ITrackRepository
    {
        List<Track> GetTracks();
        Track FindTrackById(string id);
        void SaveOrUpdate(Track track);
    }
}
