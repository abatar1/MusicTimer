namespace MusicTimer.Domain.Models
{
    public class Repository
    {
        public Repository(string path, int fileCount)
        {
            Path = path;
            FileCount = fileCount;
        }

        public string Path { get; private set; }

        public int FileCount { get; private set; }

        // TODO Класс для того, чтобы проверять директории на добавление/удаление файлов
    }
}
