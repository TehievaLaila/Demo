using System;
using System.Collections.Generic;
using System.IO;
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
using Demo.DB;
using Microsoft.Win32;

namespace Demo.Ppage
{
    /// <summary>
    /// Логика взаимодействия для AddProdPage.xaml
    /// </summary>
    public partial class AddProdPage : Page
    {
        public AddProdPage()
        {
            InitializeComponent();
            ProdTypeCmb.ItemsSource = App.DemoDb.ProductType.ToList();
            ProdTypeCmb.DisplayMemberPath = "Title";
        }
        OpenFileDialog openFileDialog = new OpenFileDialog();
        private void AddProdBtn_Click(object sender, RoutedEventArgs e)
        {
            Product product = new Product();
            {
                product.Title = TitleTxt.Text;
                var idType = ProdTypeCmb.SelectedItem;
                product.ProductTypeID = ((ProductType)idType).ID;
                product.ArticleNumber = ArticleTxt.Text;
                product.ProductionPersonCount = Convert.ToInt32(CountHum.Text);
                product.ProductionWorkshopNumber = Convert.ToInt32(Ceh.Text);
                product.MinCostForAgent = Convert.ToDecimal(MinCount.Text);
                product.Image = openFileDialog.FileName;
            }
            App.DemoDb.Product.Add(product);
            App.DemoDb.SaveChanges();
            NavigationService.Navigate(new ListProdPage());
        }

        private void LogoBtn_Click(object sender, RoutedEventArgs e)
        {
            openFileDialog.Filter = "Image Files|*.bmp;*.jpg;*.png|All Files|*.*";
            openFileDialog.FilterIndex = 1;
            if (openFileDialog.ShowDialog() == true)
            {
                File.ReadAllBytes(openFileDialog.FileName);
                BitmapImage image = new BitmapImage();
                image.BeginInit();
                image.UriSource = new Uri(openFileDialog.FileName);
                image.EndInit();    
                LogoFrame.Source = image;   
            }
        }
    }
}
