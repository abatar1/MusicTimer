using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MusicTimer.Domain;

namespace MusicTimer.Infrastructure
{
    public interface IMatchRepository
    {
        List<Track> GetMatches();
        Match FindMatchById(string id);
        void SaveOrUpdate(Match match);
    }
}
