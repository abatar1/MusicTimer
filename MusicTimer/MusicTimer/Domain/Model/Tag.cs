using SQLite;

namespace MusicTimer.Domain
{
    public class Tag
    {
        [PrimaryKey]
        public string Name { get; set; }
    }
}