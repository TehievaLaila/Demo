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
using System.Windows.Shapes;

namespace Demo.Ppage
{
    /// <summary>
    /// Логика взаимодействия для DeleteItemWindow.xaml
    /// </summary>
    public partial class DeleteItemWindow : Window
    {
        public DeleteItemWindow()
        {
            InitializeComponent();
        }
        private void NoBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void YesBtn_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;
        }
    }
}
