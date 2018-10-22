using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Encrypt_Decrypt_AES256
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow ()
        {
            InitializeComponent ();
            textBox.MaxLength=16;
            textBox1.MaxLength=32;
           
        }

        private void button_Click (object sender, RoutedEventArgs e)
        {
            richTextBox.Document.Blocks.Clear ();
            byte[] IV = Encoding.UTF8.GetBytes (textBox.Text);
            byte[] KEY = Encoding.UTF8.GetBytes (textBox1.Text);
            if (textBox.Text.Length==16 && textBox1.Text.Length==32)
            {
                richTextBox.AppendText (Methods.EncryptString (textBox2.Text, KEY, IV));
            }
            else
            {
                MessageBox.Show ("KARAKTER SINIRI");
            }

           
        }
    }
}
