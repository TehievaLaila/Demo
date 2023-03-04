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
using Demo.DB;

namespace Demo.Ppage
{
    /// <summary>
    /// Логика взаимодействия для ListProdPage.xaml
    /// </summary>
    public partial class ListProdPage : Page
    {
        public ListProdPage()
        {
            InitializeComponent();
            Filtr.Text = "Фильтрация";
            Filtr.ItemsSource = App.DemoDb.ProductType.ToList();
            Filtr.DisplayMemberPath = "Title";
        }

        private void SortAdd()
        {
            Sort.Text = "Сортировка";
            Sort.Items.Add("Наименование а-я");
            Sort.Items.Add("Наименование я-а");
            Sort.Items.Add("Стоимость по возрастанию");
            Sort.Items.Add("Стоимость по убыванию");
        }
        
        private void ImageNull()
        {
            foreach(var item in App.DemoDb.Product)
            {
                if (item.Image == "")
                    item.Image = @"\products\picture.png";
            }
            App.DemoDb.SaveChanges();
        }
        int pageNumber;
        private void RefreshPagination()
        {
            DGWrites.ItemsSource = null;
            if (Sort.Text != null)
            {
                SortInfo();
            }
            else
            {
                DGWrites.ItemsSource = App.DemoDb.Product.OrderBy(x => x.Title).Skip(pageNumber * 10).Take(10).ToList();
            }
        }
        private void Bleft_Click(object sender, RoutedEventArgs e)
        {
            if (pageNumber == 0)
                return;
            pageNumber--;
            RefreshPagination();
        }

        private void BRight_Click(object sender, RoutedEventArgs e)
        {
            if(App.DemoDb.Product.ToList().Count % 10 == 0)
            {
                if (pageNumber == (App.DemoDb.Product.ToList().Count / 10) - 1)
                    return;
            }
            else
            {
                if (pageNumber == (App.DemoDb.Product.ToList().Count / 10))
                    return;
            }
            pageNumber++;
            RefreshPagination();
        }
        private void Button_Click(object sender, RoutedCommand e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPagination();
        }
        private void Search_SelectionChanged(object sender, RoutedEventArgs e)
        {

        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
        private void SortInfo()
        {
            switch (Sort.SelectedItem)
            {
                case "Наименование а-я":
                    DGWrites.ItemsSource = null;
                    DGWrites.ItemsSource = App.DemoDb.Product.OrderBy(X => X.Title).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                case "Наименование я-а":
                    DGWrites.ItemsSource = null;
                    DGWrites.ItemsSource = App.DemoDb.Product.OrderByDescending(X => X.Title).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                case "Стоимость по возрастанию":
                    DGWrites.ItemsSource = null;
                    DGWrites.ItemsSource = App.DemoDb.Product.OrderBy(x => x.MinCostForAgent).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                case "Стоимость по убыванию":
                    DGWrites.ItemsSource = null;
                    DGWrites.ItemsSource = App.DemoDb.Product.OrderByDescending(x => x.MinCostForAgent).Skip(pageNumber * 10).Take(10).ToList();
                    break;
                default:
                    DGWrites.ItemsSource = null;
                    DGWrites.ItemsSource = App.DemoDb.Product.ToList();
                    break;
            }
        }

        private void Filtr_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void AddProdBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DGWrites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }
    }
}
