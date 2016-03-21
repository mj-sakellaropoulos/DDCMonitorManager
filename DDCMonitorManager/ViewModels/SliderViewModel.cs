using System.ComponentModel;
using System.Runtime.CompilerServices;
using DDCMonitorManager.Annotations;
using DDCMonitorManager.Core;

namespace DDCMonitorManager.ViewModels
{
    public class SliderViewModel : INotifyPropertyChanged
    {
        private string name;
        private int minimum;
        private int maximum;
        private int current;

        public string Name
        {
            get { return name; }
            set
            {
                if (value == name) return;
                name = value;
                OnPropertyChanged();
            }
        }

        public int Minimum
        {
            get { return minimum; }
            set
            {
                if (value == minimum) return;
                minimum = value;
                OnPropertyChanged();
            }
        }

        public int Maximum
        {
            get { return maximum; }
            set
            {
                if (value == maximum) return;
                maximum = value;
                OnPropertyChanged();
            }
        }

        public int Current
        {
            get { return current; }
            set
            {
                if (value == current) return;
                current = value;
                OnPropertyChanged();
            }
        }

        public int Id { get; }

        public SliderViewModel(BrightnessInfo info, int id)
        {
            Name = "Monitor";
            Minimum = info.Minimum;
            Maximum = info.Maximum;
            Current = info.Current;
            Id = id;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}