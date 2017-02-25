using System.ComponentModel;

namespace MusicTimer.Infrastructure
{
    public class WrappedSelection<T> : INotifyPropertyChanged
    {
        public T Item { get; set; }
        private bool _isSelected;

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                if (_isSelected == value) return;
                _isSelected = value;
                PropertyChanged(this, new PropertyChangedEventArgs("IsSelected"));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged = delegate { };
    }
}
