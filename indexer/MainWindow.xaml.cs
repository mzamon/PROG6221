using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace indexer
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            //create an insance of the class index_value
            index_value get_index = new index_value();

            //getting a custom indexer, to set a value
            get_index[0] = "Red";

            //getting the vale 
            string colour = get_index[0];

            //displying the index
            display_value.Text = colour;

        }        
    }
}