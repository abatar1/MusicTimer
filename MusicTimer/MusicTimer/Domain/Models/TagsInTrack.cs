using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite;

namespace MusicTimer.Domain
{
    public class TagsInTrack
    {
        [PrimaryKey]
        public int? TrackId { get; set; }
        public string TagName { get; set; }    

        public TagsInTrack() { }

        public TagsInTrack(int? ti, string tn)
        {
            TagName = tn;
            TrackId = ti;
        }
    }
}
