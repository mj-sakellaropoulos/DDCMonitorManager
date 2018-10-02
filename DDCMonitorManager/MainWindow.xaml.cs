using System.Windows;
using System.Windows.Controls;
using DDCMonitorManager.WPF.ViewModels;

namespace DDCMonitorManager.WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += OnLoaded;

        }

        private void OnLoaded(object sender, RoutedEventArgs routedEventArgs)
        {
            Window window = GetWindow(this);
            if (window != null)
            {
                DataContext = new MainViewModel(window);
            }
        }

        private void Slider_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            Slider slider = sender as Slider;
            if ((SliderViewModel) slider?.DataContext != null)
            {
                var viewModel = (MainViewModel) DataContext;
                viewModel?.ChangeSliderValue((SliderViewModel)slider.DataContext, (short)e.NewValue);
            }
        }

    }
}
