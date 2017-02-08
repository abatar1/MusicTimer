using System;
using System.Collections.Generic;
using SQLite;

namespace MusicTimer.Domain
{
    public class Track
    {
        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }

        public int Duration { get; set; }

        public string Name { get; set; }

        public string FullName { get; set; }

        //TODO ��� ��� ������� ��������, � ������ Palletizer ���������
        [Ignore]
        public List<Tag> Tags { get; set; }
    }
}
