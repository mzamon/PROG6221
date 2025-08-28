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

namespace Garbage_Collection
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            //this always comes first 
            InitializeComponent(); 
            Display_Memory_Collected();
        }
        //method to get garbage collection memory 

        private void Display_Memory_Collected()
        {
            //creating an instance for the object building class
            object current_gen = new object();

            //getting the generation automatically
            GC.GetGeneration(current_gen);

            display_memory.Text = "";
            //displaying the total memory of any gen object found
            display_memory.Text += $"Total Memory Collected: {GC.GetTotalMemory(false)} bytes";











            /**
            //Display the start of the demonstration
            display_memory.Text = "Garbage Collection Demo";
            
            //creating multiple objecs to multiple obects to allocate memory on the managed heap
            for (int k = 0; k < 1000; k++)
            {
                //Allowcate 1 KB per object
                var obj = new byte[1024];
            }
            
            //inform the user that the memory collection is done
            */
        }
    }
}