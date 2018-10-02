using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Interop;
using DDCMonitorManager.Core;
using DDCMonitorManager.WPF.Properties;

namespace DDCMonitorManager.WPF.ViewModels
{
    public class MainViewModel : INotifyPropertyChanged
    {
        private readonly BrightnessControl brightnessControl;

        public MainViewModel(Window window)
        {
            Sliders = new ObservableCollection<SliderViewModel>();

            var wih = new WindowInteropHelper(window);
            brightnessControl = new BrightnessControl(wih.Handle);
            InitializeSliders();
        }

        public ObservableCollection<SliderViewModel> Sliders { get; private set; }

        public void ChangeSliderValue(SliderViewModel slider, short value)
        {
            if (slider == null)
            {
                throw new ArgumentNullException(nameof(slider));
            }
            
            brightnessControl.SetBrightness(value, slider.Id);
            
        }

        private void InitializeSliders()
        {
            var monitorsCount = brightnessControl.GetMonitors();
            for (var i = 0; i < monitorsCount; ++i)
            {
                var mInfo = brightnessControl.GetBrightnessCapabilities(i);
                Sliders.Add(new SliderViewModel(mInfo, i));
            }
        }


        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
