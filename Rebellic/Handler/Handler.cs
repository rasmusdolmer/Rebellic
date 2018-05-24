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
using Windows.System.Threading;
using Windows.UI;
using Windows.UI.Input;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;
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

        public ObservableCollection<StackPanel> EmployeeCollection { get; set; }

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
        private Visibility _dateAndTimeViewVisibilty;

        public Visibility DateAndTimeViewVisibilty
        {
            get { return _dateAndTimeViewVisibilty; }
            set
            {
                _dateAndTimeViewVisibilty = value;
                OnPropertyChanged();
            }
        }

        public Visibility _contactViewVisibilty { get; set; }

        public Visibility ContactViewVisibilty
        {
            get { return _contactViewVisibilty; }
            set
            {
                _contactViewVisibilty = value;
                OnPropertyChanged();
            }
        }

        public Visibility _confirmViewVisibilty { get; set; }

        public Visibility ConfirmViewVisibilty
        {
            get { return _confirmViewVisibilty; }
            set
            {
                _confirmViewVisibilty = value;
                OnPropertyChanged();
            }
        }
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
            EmployeeCollection = new ObservableCollection<StackPanel>();
            GetProductCategories();
            GetEmployees();
            VidereTilMedarbejder = new SolidColorBrush(Colors.LightGray);
            EmployeeViewVisibilty = Visibility.Collapsed;
            DateAndTimeViewVisibilty = Visibility.Collapsed;
        }

        public Handler(ObservableCollection<TextBlock> productsCategories, ObservableCollection<Product> products, ObservableCollection<Grid> gridCollection, ObservableCollection<StackPanel> employeeCollection, string categoryName)
        {
            EmployeeCollection = employeeCollection;
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
                    OnPropertyChanged();
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

        public void GetEmployees()
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
                    var responce = client.GetAsync("api/Employees").Result;
                    if (responce.IsSuccessStatusCode)
                    {
                        var eys = responce.Content.ReadAsAsync<IEnumerable<Employee>>().Result;
                        foreach (var ey in eys)
                        {
                            #region Dynamic Employee Controls
                            StackPanel sp1 = new StackPanel();
                            sp1.Padding = new Thickness(30, 10,30, 10);
                            sp1.Background = new SolidColorBrush(Colors.White);
                            sp1.Orientation = Orientation.Horizontal;
                            sp1.Margin = new Thickness(50,15, 50, 0);
                            EmployeeCollection.Add(sp1);

                            Image img = new Image();
                           
                            img.Width = 50;
                            img.HorizontalAlignment = HorizontalAlignment.Left;
                            img.VerticalAlignment = VerticalAlignment.Top;

                            StackPanel sp2 = new StackPanel();
                            sp2.Width = 530;
                            sp2.Padding = new Thickness(10, 0, 0, 0);

                            sp1.Children.Add(img);
                            sp1.Children.Add(sp2);

                            TextBlock eName = new TextBlock();
                            eName.Text = ey.Employee_Name;
                            eName.FontWeight = FontWeights.SemiBold;
                            eName.Foreground = new SolidColorBrush(Colors.DodgerBlue);
                            eName.FontSize = 17;
                            eName.Margin = new Thickness(0, -7, 0, 0);

                            TextBlock eTitle = new TextBlock();
                            eTitle.Text = "Professionel Body Piercer";
                            eTitle.FontStyle = FontStyle.Oblique;
                            eTitle.Margin = new Thickness(0, 0, 0, 10);

                            TextBlock eDesc = new TextBlock();
                            eDesc.Text =
                                "Udd. Professionel Body Piercet siden 2010 startet i lærer som body piercer i 2008. - 2010.";
                            eDesc.TextWrapping = TextWrapping.Wrap;
                            eDesc.Width = 400;
                            eDesc.HorizontalAlignment = HorizontalAlignment.Left;

                            sp2.Children.Add(eName);
                            sp2.Children.Add(eTitle);
                            sp2.Children.Add(eDesc);

                            Rectangle ChooseE = new Rectangle();
                            ChooseE.Tapped += BookingVidere;
                            ChooseE.Width = 100;
                            ChooseE.Fill = new SolidColorBrush(Colors.DodgerBlue);
                            ChooseE.Height = 30;
                            ChooseE.VerticalAlignment = VerticalAlignment.Top;

                            TextBlock ChooseEText = new TextBlock();
                            ChooseEText.Text = "VÆLG";
                            ChooseEText.Margin = new Thickness(-100, 0, 0, 59);
                            ChooseEText.Padding = new Thickness(0, 4, 0, 0);
                            ChooseEText.Height = 30;
                            ChooseEText.Width = 100;
                            ChooseEText.TextAlignment = TextAlignment.Center;
                            ChooseEText.Foreground = new SolidColorBrush(Colors.White);
                            ChooseEText.Tapped += BookingVidere;

                            sp1.Children.Add(ChooseE);
                            sp1.Children.Add(ChooseEText);
                            #endregion
                        }
                    }                    
                }
                catch (Exception)
                {

                }
           
            }
        }

        private int count = 1;

        public void BookingVidere(object sender, RoutedEventArgs e)
        {
            ProductViewVisibility = Visibility.Collapsed;
            EmployeeViewVisibilty = Visibility.Collapsed;
            DateAndTimeViewVisibilty = Visibility.Collapsed;
            ContactViewVisibilty = Visibility.Collapsed;
            ConfirmViewVisibilty = Visibility.Collapsed;
            switch (count)
            {
                case 1:
                    if (Productlist.Count > 0)
                    {
                        EmployeeViewVisibilty = Visibility.Visible;
                        count++;
                    }
                    else
                    {
                        ProductViewVisibility = Visibility.Visible;
                    }
                    break;
                case 2:
                    DateAndTimeViewVisibilty = Visibility.Visible;
                    count++;
                    break;
            }
        }

        public void BookingTilbage()
        {
            ProductViewVisibility = Visibility.Collapsed;
            EmployeeViewVisibilty = Visibility.Collapsed;
            DateAndTimeViewVisibilty = Visibility.Collapsed;
            ContactViewVisibilty = Visibility.Collapsed;
            ConfirmViewVisibilty = Visibility.Collapsed;
            switch (count)
            {
                case 2:               
                    ProductViewVisibility = Visibility.Visible;
                    count--;
                    break;
                case 3:
                    EmployeeViewVisibilty = Visibility.Visible;
                    count--;
                    break;
            }

        }
    }
}

