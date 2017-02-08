using SQLite;

namespace MusicTimer.Domain
{
    public class Tag : IData
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public string Name { get; }

        public Tag(string name)
        {
            Name = name;
        }

        public Tag()
        {
            
        }
    }
}