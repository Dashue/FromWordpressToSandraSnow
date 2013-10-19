using BlogExporter.ViewModels;
using System.Windows;
using System.Windows.Forms;

namespace BlogExporter
{
    public partial class MainWindow : Window
    {
        private OpenFileDialog dialog;


        public MainWindow()
        {
            InitializeComponent();

            dialog = new OpenFileDialog();
            DataContext = new MainViewModel();
        }

        private void SelectFile(object sender, RoutedEventArgs e)
        {
            DialogResult result = dialog.ShowDialog();

            if (result == System.Windows.Forms.DialogResult.OK)
            {
                ((MainViewModel)DataContext).Path = dialog.FileName;
            }
        }
    }
}
