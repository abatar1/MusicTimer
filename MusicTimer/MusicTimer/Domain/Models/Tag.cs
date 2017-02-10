namespace MusicTimer.Domain
{
    public class Tag
    {
        public string Name { get; }

        public Tag(string name)
        {
            Name = name;
        }

        public override bool Equals(object obj)
        {           
            var other = obj as Tag;
            if (other == null) return false;
            return Name == other.Name;
        }

        public override int GetHashCode()
        {
            return Name?.GetHashCode() ?? 0;
        }
    }
}