using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Rebellic;
using WebService;

namespace Rebellic
{
    class Handler : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public Frame frame = (Frame)Window.Current.Content;

        public ObservableCollection<TextBlock> ProductCategories { get; set; }
        public ObservableCollection<Product> Products { get; set; }
        public ObservableCollection<Grid> GridCollection { get; set; }
        public string CategoryName { get; set; }
        public int SelectedProductCategory { get; set; }
        private Dictionary<int, string> Productlist = new Dictionary<int, string>();

        private SolidColorBrush _videreTilMedarbejder;

        public SolidColorBrush VidereTilMedarbejder
        {
            get
            {
                return _videreTilMedarbejder;
            }
            set
            {
                _videreTilMedarbejder = value;
                OnPropertyChanged();
            }
        }


        #region BookingViewsVisibilty

        private Visibility _productViewVisibilty;

        public Visibility ProductViewVisibility
        {
            get { return _productViewVisibilty; }
            set
            {
                _productViewVisibilty = value;
                OnPropertyChanged();
            }
        }
        private Visibility _employeeViewVisibilty;

        public Visibility EmployeeViewVisibilty
        {
            get { return _employeeViewVisibilty; }
            set
            {
                _employeeViewVisibilty = value;
                OnPropertyChanged();
            }
        }

        public Visibility DateAndTimeViewVisibilty { get; set; }
        public Visibility ContactViewVisibilty { get; set; }
        public Visibility ConfirmViewVisibilty { get; set; }
        #endregion


        private string _chosenProducts;

        public string ChosenProducts
        {
            get
            {
                return _chosenProducts;
            }
            set
            {
                _chosenProducts = value;
                OnPropertyChanged();
            }
        }

        public Handler()
        {
            ProductCategories = new ObservableCollection<TextBlock>();
            Products = new ObservableCollection<Product>();
            GridCollection = new ObservableCollection<Grid>();
            GetProductCategories();
            VidereTilMedarbejder = new SolidColorBrush(Colors.LightGray);
            EmployeeViewVisibilty = Visibility.Collapsed;

        }

        public Handler(ObservableCollection<TextBlock> productsCategories, ObservableCollection<Product> products, ObservableCollection<Grid> gridCollection, string categoryName)
        {
            ProductCategories = productsCategories;
            Products = products;
            GridCollection = gridCollection;
            CategoryName = categoryName;
        }
        public void GetProductCategories()
        {
            const string serverUrl = "http://localhost:51992/";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                try
                {
                    var responce = client.GetAsync("api/ProductCategories").Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var pcs = responce.Content.ReadAsAsync<IEnumerable<ProductCategory>>().Result;
                        foreach (var pc in pcs)
                        {
                            TextBlock lvi = new TextBlock();
                            lvi.Text = pc.Category_Name;
                            lvi.AccessKey = pc.Category_Id.ToString();
                            lvi.Padding = new Thickness(15, 13, 15, 12);
                            lvi.Height = 50;
                            lvi.Width = 180;
                            lvi.Tapped += LviOnTapped;
                            ProductCategories.Add(lvi);
                        }
                    }
                }
                catch (Exception)
                {

                }
            }
        }

        public void GetProducts()
        {
            GridCollection.Clear();
            const string serverUrl = "http://localhost:51992/";
            HttpClientHandler handler = new HttpClientHandler();
            handler.UseDefaultCredentials = true;
            using (var client = new HttpClient(handler))
            {
                client.BaseAddress = new Uri(serverUrl);
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var responce = client.GetAsync("api/Products").Result;
                if (responce.IsSuccessStatusCode)
                {
                    var pds = responce.Content.ReadAsAsync<IEnumerable<Product>>().Result;
                    foreach (var pd in pds)
                    {
                        if (pd.Product_Category == SelectedProductCategory)
                        {
                            #region Dynamic XamlControls
                            Grid g = new Grid();
                            //sp.Orientation = Orientation.Horizontal;
                            g.VerticalAlignment = VerticalAlignment.Top;
                            g.MaxWidth = 665;
                            GridCollection.Add(g);

                            CheckBox cb = new CheckBox();
                            cb.Content = pd.Product_Name;
                            cb.FontWeight = FontWeights.SemiBold;
                            cb.Width = 380;
                            cb.AccessKey = pd.Product_Id.ToString();
                            cb.VerticalAlignment = VerticalAlignment.Center;
                            cb.Checked += ProductChosen;
                            cb.Unchecked += ProductUnchosen;
                            if (Productlist.ContainsKey(int.Parse(cb.AccessKey)))
                            {
                                Productlist.Remove(int.Parse(cb.AccessKey));
                                cb.IsChecked = true;
                            }

                            TextBlock tb = new TextBlock();
                            tb.Text = pd.Product_time.Minutes + " minutter";
                            tb.VerticalAlignment = VerticalAlignment.Center;
                            tb.Foreground = new SolidColorBrush(Color.FromArgb(255, 107, 110, 111));
                            tb.FontWeight = FontWeights.SemiBold;
                            tb.TextAlignment = TextAlignment.Right;
                            tb.Width = 150;

                            TextBlock tb2 = new TextBlock();
                            tb2.Text = pd.Product_Price + " kr.";
                            tb2.HorizontalAlignment = HorizontalAlignment.Right;
                            tb2.Foreground = new SolidColorBrush(Colors.DodgerBlue);
                            tb2.FontWeight = FontWeights.SemiBold;
                            tb2.TextAlignment = TextAlignment.Right;
                            tb2.Width = 135;
                            tb2.Margin = new Thickness(0, 5, 0, 0);

                            Grid g2 = new Grid();
                            //sp.Orientation = Orientation.Horizontal;
                            g2.VerticalAlignment = VerticalAlignment.Top;
                            g2.MaxWidth = 665;
                            g2.Margin = new Thickness(0, 0, 0, 15);
                            GridCollection.Add(g2);

                            TextBlock tb3 = new TextBlock();
                            tb3.Text = pd.Product_Desc;
                            tb3.Width = 665;
                            tb3.HorizontalAlignment = HorizontalAlignment.Left;
                            tb3.TextWrapping = TextWrapping.Wrap;
                            tb3.Margin = new Thickness(0, 0, 0, 0);

                            g.Children.Add(cb);
                            g.Children.Add(tb);
                            g.Children.Add(tb2);
                            g2.Children.Add(tb3);
                            #endregion
                        }
                    }
                }


            }
        }

        private void LviOnTapped(object sender, TappedRoutedEventArgs e)
        {
            SelectedProductCategory = int.Parse(((TextBlock)e.OriginalSource).AccessKey);
            GetProducts();
        }

        private void ProductChosen(object sender, RoutedEventArgs e)
        {
            ChosenProducts = "";

            int ProductId = int.Parse(((CheckBox)e.OriginalSource).AccessKey);
            string ProductName = ((CheckBox)e.OriginalSource).Content.ToString();

            Productlist.Add(ProductId, " " + ProductName);

            foreach (var product in Productlist)
            {
                ChosenProducts += product.Value;
            }

            VidereTilMedarbejder = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void ProductUnchosen(object sender, RoutedEventArgs e)
        {
            ChosenProducts = "";
            int ProductId = int.Parse(((CheckBox)e.OriginalSource).AccessKey);

            if (Productlist.ContainsKey(ProductId))
            {
                Productlist.Remove(ProductId);
            }

            if (Productlist.Count > 0)
            {
                foreach (var product in Productlist)
                {
                    ChosenProducts += product.Value;
                }
            }
            else
            {
                VidereTilMedarbejder = new SolidColorBrush(Colors.LightGray);
            }

        }

        private int count = 1;
        public void BookingVidere()
        {
            ProductViewVisibility = Visibility.Collapsed;
            DateAndTimeViewVisibilty = Visibility.Collapsed;
            ContactViewVisibilty = Visibility.Collapsed;
            ConfirmViewVisibilty = Visibility.Collapsed;
            switch (count)
            {
                case 1:
                    if (Productlist.Count > 0)
                    {
                        EmployeeViewVisibilty = Visibility.Visible;
                    }
                    else
                    {
                        ProductViewVisibility = Visibility.Visible;
                    }
                    break;
                case 2:
                    break;
            }


            count++;
        }
    }
}

