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
            SortAdd();
            RefreshPagination();
            RefreshButtons();
            ImageNull();
            SortInfo();
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
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            pageNumber = Convert.ToInt32(button.Content) - 1;
            RefreshPagination();
        }
        private void RefreshButtons()
        {
            WPButtons.Children.Clear();
            if (App.DemoDb.Product.ToList().Count % 10 == 0)
            {
                for (int i = 0; i < App.DemoDb.Product.ToList().Count / 10; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 14;
                    WPButtons.Children.Add(button);
                }
            }
            else
            {
                for (int i = 0; i < App.DemoDb.Agent.ToList().Count / 10 + 1; i++)
                {
                    Button button = new Button();
                    button.Content = i + 1;
                    button.Click += Button_Click;
                    button.Margin = new Thickness(5);
                    button.Width = 20;
                    button.Height = 20;
                    button.FontSize = 14;
                    WPButtons.Children.Add(button);
                }
            }
        }
        private void Search_SelectionChanged(object sender, RoutedEventArgs e)
        {
            var res = App.DemoDb.Product.ToList();
            res = res.Where(X => X.Title.ToLower().Contains(Search.Text)).Skip(pageNumber * 10).Take(10).ToList();
            DGWrites.ItemsSource = res.ToList();
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            SortInfo();
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
            var selectedType = Filtr.SelectedItem;
            var type = ((ProductType)selectedType).ID;
            DGWrites.ItemsSource = App.DemoDb.Product.Where(x => x.ProductTypeID == type).ToList();
        }

        private void AddProdBtn_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new AddProdPage());
        }

        private void DGWrites_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DeleteItemWindow del = new DeleteItemWindow();
            if(del.ShowDialog()==true)
            {
                try
                {
                    var item = DGWrites.SelectedItem as Product;
                    App.DemoDb.Product.Remove(item);
                    App.DemoDb.SaveChanges();
                    RefreshButtons();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"{ex}");
                }
            }
        }
    }
}
