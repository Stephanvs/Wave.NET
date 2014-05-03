using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WaveNET.Windows
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            
            TextInput.TextWrapping = TextWrapping.Wrap;

            Observable
                .FromEventPattern<TextChangedEventArgs>(TextInput, "TextChanged")
                .Buffer(TimeSpan.FromSeconds(5))
                .Subscribe(pattern =>
                {
                    Console.WriteLine("Starting Buffered TextChanges");
                    foreach (var eventPattern in pattern)
                    {
                        LogChanges(eventPattern.EventArgs.Changes);
                    }
                    Console.WriteLine("Finished Buffered TextChanges");
                });
        }

        private void LogChanges(IEnumerable<TextChange> changes)
        {
            foreach (var change in changes)
            {
                Console.WriteLine("Added Length: {0}; Removed Length: {1}, Offset: {2}" , change.AddedLength, change.RemovedLength, change.Offset);
            }
        }
    }
}
