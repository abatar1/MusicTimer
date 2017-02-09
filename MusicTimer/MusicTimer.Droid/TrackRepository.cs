using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MusicTimer.Domain;
using MusicTimer.Droid;
using Xamarin.Forms;

[assembly: Dependency(typeof(TrackRepository))]
namespace MusicTimer.Droid
{   
    public class TrackRepository : ITrackRepository
    {
        public List<Track> GetTracks()
        {
            throw new NotImplementedException();
        }

        public Track FindTrackById(string id)
        {
            throw new NotImplementedException();
        }

        public void SaveOrUpdate(Track track)
        {
            throw new NotImplementedException();
        }
    }
}
