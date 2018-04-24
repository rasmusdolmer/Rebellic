using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Core;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Rebellic
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public MainPage()
        {
            this.InitializeComponent();
        }

        private void MenuOpen_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            //if (LeftMenu.Visibility == Visibility.Visible)
            //{
            //    LeftMenu.Visibility = Visibility.Collapsed;
            //    MenuOpen.Visibility = Visibility.Visible;
            //}
            //else
            //{
            //    LeftMenu.Visibility = Visibility.Visible;
            //    MenuOpen.Visibility = Visibility.Collapsed;
            //}
        }

        private void ChangeTab(object sender, TappedRoutedEventArgs e)
        {
            #region ResetFontWeights
            tb0.FontWeight = FontWeights.Normal;
            tb1.FontWeight = FontWeights.Normal;
            tb2.FontWeight = FontWeights.Normal;
            tb3.FontWeight = FontWeights.Normal;
            tb4.FontWeight = FontWeights.Normal;
            tb5.FontWeight = FontWeights.Normal;
            tb6.FontWeight = FontWeights.Normal;
            tb7.FontWeight = FontWeights.Normal;
            tb8.FontWeight = FontWeights.Normal;
            tb9.FontWeight = FontWeights.Normal;
            #endregion
            #region ResetProductViews
            ProductContent1.Visibility = Visibility.Collapsed;
            ProductContent2.Visibility = Visibility.Collapsed;
            ProductContent3.Visibility = Visibility.Collapsed;
            ProductContent4.Visibility = Visibility.Collapsed;
            ProductContent5.Visibility = Visibility.Collapsed;
            ProductContent6.Visibility = Visibility.Collapsed;
            ProductContent7.Visibility = Visibility.Collapsed;
            ProductContent8.Visibility = Visibility.Collapsed;
            ProductContent9.Visibility = Visibility.Collapsed;
            ProductContent10.Visibility = Visibility.Collapsed;
            #endregion

            int index = int.Parse(((TextBlock)e.OriginalSource).AccessKey);

            GridCursor.Margin = new Thickness(180, 00 + (50 * index), 0, 0);
            hoverEffect2.Margin = new Thickness(0, 00 + (50 * index), 0, 0);

            switch (index)
            {
                case 0:
                    ProductContent1.Visibility = Visibility.Visible;
                    tb0.FontWeight = FontWeights.SemiBold;
                    break;
                case 1:
                    ProductContent2.Visibility = Visibility.Visible;
                    tb1.FontWeight = FontWeights.SemiBold;
                    break;
                case 2:
                    ProductContent3.Visibility = Visibility.Visible;
                    tb2.FontWeight = FontWeights.SemiBold;
                    break;
                case 3:
                    ProductContent4.Visibility = Visibility.Visible;
                    tb3.FontWeight = FontWeights.SemiBold;
                    break;
                case 4:
                    ProductContent5.Visibility = Visibility.Visible;
                    tb4.FontWeight = FontWeights.SemiBold;
                    break;
                case 5:
                    ProductContent6.Visibility = Visibility.Visible;
                    tb5.FontWeight = FontWeights.SemiBold;
                    break;
                case 6:
                    ProductContent7.Visibility = Visibility.Visible;
                    tb6.FontWeight = FontWeights.SemiBold;
                    break;
                case 7:
                    ProductContent8.Visibility = Visibility.Visible;
                    tb7.FontWeight = FontWeights.SemiBold;
                    break;
                case 8:
                    ProductContent9.Visibility = Visibility.Visible;
                    tb8.FontWeight = FontWeights.SemiBold;
                    break;
                case 9:
                    ProductContent10.Visibility = Visibility.Visible;
                    tb9.FontWeight = FontWeights.SemiBold;
                    break;
            }
        }

        private void UIElement_OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            int index = int.Parse(((TextBlock)e.OriginalSource).AccessKey);

            hoverEffect.Margin = new Thickness(0, 00 + (50 * index), 0, 0);

            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            hoverEffect.Visibility = Visibility.Visible;
        }

        private void UIElement_OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
            hoverEffect.Visibility = Visibility.Collapsed;
        }

        private void MenuItem_OnPointerEntered2(object sender, PointerRoutedEventArgs e)
        {
            int index = int.Parse(((TextBlock)e.OriginalSource).AccessKey);

            hoverEffect1.Margin = new Thickness(0, 20 + (index * 50), 0, 0);

            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            hoverEffect1.Visibility = Visibility.Visible;
        }

        private void MenuItem_OnPointerExited2(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
            hoverEffect1.Visibility = Visibility.Collapsed;
        }

        private void ChangeCursorEnter(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void ChangeCursorExit(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }

        private void ChangeMenu(object sender, TappedRoutedEventArgs e)
        {
            string navTo = ((TextBlock)e.OriginalSource).Name;

            switch (navTo)
            {
                case "NavMainPage":
                    Frame.Navigate(typeof(MainPage));
                    break;
                case "NavLogInd":
                    Frame.Navigate(typeof(LogInd));
                    break;
            }
        }

        private void BtnShowPane_OnClick(object sender, TappedRoutedEventArgs e)
        {
            if (MenuPopup.IsOpen == false)
            {
                MenuPopup.IsOpen = true;
            }
            else
            {
                MenuPopup.IsOpen = false;
            }
        }

        private void BtnShowPane_OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            btnShowPaneBack.Visibility = Visibility.Visible;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void BtnShowPane_OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            btnShowPaneBack.Visibility = Visibility.Collapsed;
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            if (myPopup.IsOpen)
            {
                myPopup.IsOpen = false;
            }
            else
            {
                myPopup.IsOpen = true;
            }
        }

        private void ProfileOpen_OnTapped(object sender, TappedRoutedEventArgs e)
        {
            if (myPopup.IsOpen == false)
            {
                myPopup.IsOpen = true;
                InfoPopup.IsOpen = false;
                ProfileArrowUp.Visibility = Visibility.Visible;
                ProfileArrowDown.Visibility = Visibility.Collapsed;
            }
            else
            {
                myPopup.IsOpen = false;
                ProfileArrowDown.Visibility = Visibility.Visible;
                ProfileArrowUp.Visibility = Visibility.Collapsed;
            }
        }

        private void Profile_OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (myPopup.IsOpen)
            {
                Window.Current.CoreWindow.PointerCursor =
                    new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            }
        }

        private void Profile_OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (myPopup.IsOpen)
            {
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
            }
        }

        private void ProfileOpener_OnPointerEntered(object sender, PointerRoutedEventArgs e)
        {
            profileOpen.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }
        private void ProfileOpener_OnPointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (!myPopup.IsOpen)
            {
                profileOpen.Background = new SolidColorBrush(Color.FromArgb(237, 255, 255, 255));
            }
            else
            {
                profileOpen.Background = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
            }
        }

        #region Properties
        //List<int> cbList = new List<int>();
        string ValgteServices = "";
        Dictionary<int, string> servicelist = new Dictionary<int, string>();
        #endregion

        private void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            ValgteServices = "";
            servicelist.Add(int.Parse(((CheckBox)e.OriginalSource).AccessKey), ((CheckBox)e.OriginalSource).Content.ToString());
            foreach (var service in servicelist)
            {
                if (service.Key > 1)
                {
                    ValgteServices += ", " + service.Value;
                }
                else
                {
                    ValgteServices += service.Value;
                }
            }
            BookingProcessService.Text = ValgteServices;

            videreTilMedarIkon.Foreground = new SolidColorBrush(Colors.DodgerBlue);
            videreTilMedarT.Foreground = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            ValgteServices = "";
            servicelist.Remove(int.Parse(((CheckBox)e.OriginalSource).AccessKey));
            foreach (var service in servicelist)
            {
                ValgteServices += service.Value;
            }
            BookingProcessService.Text = ValgteServices;

            if (servicelist.Count == 0)
            {
                videreTilMedarIkon.Foreground = new SolidColorBrush(Colors.LightGray);
                videreTilMedarT.Foreground = new SolidColorBrush(Colors.LightGray);
            }
        }

        private void BookingVidere(object sender, TappedRoutedEventArgs e)
        {
            string getName = ((TextBlock)e.OriginalSource).Name;

            switch (getName)
            {
                case "videreTilMedarT":
                    if (servicelist.Count > 0)
                    {
                        VælgService.Visibility = Visibility.Collapsed;
                        VælgMedarbejder.Visibility = Visibility.Visible;
                    }
                    break;

                case "videreTilMedarIkon":
                    if (servicelist.Count > 0)
                    {
                        VælgService.Visibility = Visibility.Collapsed;
                        VælgMedarbejder.Visibility = Visibility.Visible;
                    }
                    break;
                case "tilbageTilServiceT":
                    VælgService.Visibility = Visibility.Visible;
                    VælgMedarbejder.Visibility = Visibility.Collapsed;
                    break;
                case "tilbageTilServiceIkon":
                    VælgService.Visibility = Visibility.Visible;
                    VælgMedarbejder.Visibility = Visibility.Collapsed;
                    break;
                case "VidereTilTidspunktBackground":
                    VælgTidspunkt.Visibility = Visibility.Visible;
                    VælgMedarbejder.Visibility = Visibility.Collapsed;
                    BookingProcessMedarbejder.Text = "Beneg Lysgaard";
                    BookingProcessArrow1.Visibility = Visibility.Visible;
                    break;
                case "VidereTilTidspunktTekst":
                    VælgTidspunkt.Visibility = Visibility.Visible;
                    VælgMedarbejder.Visibility = Visibility.Collapsed;
                    BookingProcessMedarbejder.Text = "Beneg Lysgaard";
                    BookingProcessArrow1.Visibility = Visibility.Visible;
                    break;
                case "tilbageTilMedarbejderT":
                    VælgMedarbejder.Visibility = Visibility.Visible;
                    VælgTidspunkt.Visibility = Visibility.Collapsed;
                    break;
                case "tilbageTilMedarbejderIkon":
                    VælgMedarbejder.Visibility = Visibility.Visible;
                    VælgTidspunkt.Visibility = Visibility.Collapsed;
                    break;
                case "videreTilKontaktT":
                    if (BookingProcessTidspunkt.Text != "")
                    {
                        VælgTidspunkt.Visibility = Visibility.Collapsed;
                        AngivKontaktOplysninger.Visibility = Visibility.Visible;
                    }
                    break;
                case "videreTilKontaktIkon":
                    if (BookingProcessTidspunkt.Text != "")
                    {
                        VælgTidspunkt.Visibility = Visibility.Collapsed;
                        AngivKontaktOplysninger.Visibility = Visibility.Visible;
                    }
                    break;
                case "tilbageTilTidT":
                    VælgTidspunkt.Visibility = Visibility.Visible;
                    AngivKontaktOplysninger.Visibility = Visibility.Collapsed;
                    break;
                case "tilbageTilTidIkon":
                    VælgTidspunkt.Visibility = Visibility.Visible;
                    AngivKontaktOplysninger.Visibility = Visibility.Collapsed;
                    break;
                case "videreTilBekræftT":

                    BekræftBooking.Visibility = Visibility.Visible;
                    AngivKontaktOplysninger.Visibility = Visibility.Collapsed;
                    break;
                case "videreTilBekræftIkon":
                    BekræftBooking.Visibility = Visibility.Visible;
                    AngivKontaktOplysninger.Visibility = Visibility.Collapsed;
                    break;
                case "tilbageTilKontaktT":
                    BekræftBooking.Visibility = Visibility.Collapsed;
                    AngivKontaktOplysninger.Visibility = Visibility.Visible;
                    break;
                case "tilbageTilKontaktIkon":
                    BekræftBooking.Visibility = Visibility.Collapsed;
                    AngivKontaktOplysninger.Visibility = Visibility.Visible;
                    break;
                case "VidereTilKontaktOplTekst":
                    if (kontaktTlfValidate == 0 && KontaktTlf.Text.Length > 0)
                    {
                        AngivTlfGrid.Visibility = Visibility.Collapsed;
                        AngivKontakOplGrid.Visibility = Visibility.Visible;
                        videreTilBekræftT.Visibility = Visibility.Visible;
                        videreTilBekræftIkon.Visibility = Visibility.Visible;
                    }
                    break;
            }
        }

        private void ChangeCursorTilMedarbejderEnter(object sender, PointerRoutedEventArgs e)
        {
            if (servicelist.Count != 0)
            {
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            }
        }

        private void ChangeCursorTilMedarbejderExit(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }

        private void Medarbejder_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            VidereTilTidspunktBackground.Fill = new SolidColorBrush(Color.FromArgb(255, 68, 138, 255));
        }

        private void Medarbejder_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
            VidereTilTidspunktBackground.Fill = new SolidColorBrush(Color.FromArgb(204, 68, 138, 255)); ;
        }

        private void InfoPointerEnter(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            InfoPopupBackground.Fill = new SolidColorBrush(Color.FromArgb(230, 255, 255, 255));
        }

        private void InfoPointerExit(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
            if (!InfoPopup.IsOpen)
            {
                InfoPopupBackground.Fill = new SolidColorBrush(Color.FromArgb(153, 255, 255, 255));
            }
        }

        private void InfoPopup_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (InfoPopup.IsOpen)
            {
                InfoPopup.IsOpen = false;
                InfoPopupBackground.Fill = new SolidColorBrush(Color.FromArgb(153, 255, 255, 255));
            }
            else
            {
                InfoPopup.IsOpen = true;
                myPopup.IsOpen = false;
                InfoPopupBackground.Fill = new SolidColorBrush(Color.FromArgb(230, 255, 255, 255));
            }
        }
        List<TextBlock> tblist = new List<TextBlock>();
        private void selectDate_DateChanged(object sender, DatePickerValueChangedEventArgs e)
        {
            int count = 1;
            #region Ledigetider
            List<DateTime> LedigeTiderMedDato = new List<DateTime>();
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/25/2018 19:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/25/2018 20:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 11:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 12:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 13:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 14:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 15:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 16:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 17:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 18:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 19:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 20:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 21:00:00.00"));
            LedigeTiderMedDato.Add(Convert.ToDateTime("04/24/2018 22:00:00.00"));

            List<string> LedigeTiderPåValgteDato = new List<string>();
            #endregion
            string getdate = selectDate.Date.Date.ToString();
            LedigeTiderPåValgteDato.Clear();
            foreach (var tid in LedigeTiderMedDato)
            {
                if (tid.Date.Date.ToString() == getdate)
                {
                    LedigeTiderPåValgteDato.Add(tid.TimeOfDay.ToString());
                }
            }
            if (LedigeTiderPåValgteDato.Count > 1)
            {
                selectTid.Children.Clear();

                foreach (var tid in LedigeTiderPåValgteDato)
                {
                    Grid TidGrid = new Grid();
                    TidGrid.Name = "TidGrid" + count;
                    TidGrid.AccessKey = count.ToString();
                    TidGrid.Width = 100;
                    TidGrid.Height = 32;
                    TidGrid.HorizontalAlignment = HorizontalAlignment.Left;
                    TidGrid.VerticalAlignment = VerticalAlignment.Top;
                    TidGrid.Background = new SolidColorBrush(Colors.White);
                    TidGrid.Margin = new Thickness(5, 0, 0, 0);
                    TidGrid.PointerEntered += VælgTidEnter;
                    TidGrid.PointerExited += VælgTidExit;
                    TidGrid.Tapped += VælgTidTapped;
                    if (count <= 4)
                    {
                        selectTid.Children.Add(TidGrid);
                    }
                    if (count >= 5 && count <= 8)
                    {
                        selectTid2.Children.Add(TidGrid);
                    }
                    if (count >= 9 && count <= 12)
                    {
                        selectTid3.Children.Add(TidGrid);
                    }

                    TextBlock TidTekst = new TextBlock();
                    TidTekst.Name = "tb-" + tid;
                    TidTekst.AccessKey = count.ToString();
                    TidTekst.Text = tid.ToString();
                    TidTekst.Width = 100;
                    TidTekst.Height = 32;
                    TidTekst.Padding = new Thickness(0, 6, 0, 0);
                    TidTekst.Foreground = new SolidColorBrush(Color.FromArgb(255, 51, 51, 51));
                    TidTekst.TextAlignment = TextAlignment.Center;
                    TidTekst.PointerEntered += VælgTidEnter;
                    TidTekst.PointerExited += VælgTidExit;
                    TidTekst.Tapped += VælgTidTapped;
                    TidGrid.Children.Add(TidTekst);
                    tblist.Add(TidTekst);

                    Border TidBorder = new Border();
                    TidBorder.Width = 100;
                    TidBorder.Height = 32;
                    TidBorder.BorderThickness = new Thickness(1);
                    TidBorder.BorderBrush = new SolidColorBrush(Colors.LightGray);
                    TidGrid.Children.Add(TidBorder);
                    count++;
                }
            }
            else
            {
                selectTid.Children.Clear();
            }

            videreTilKontaktT.Foreground = new SolidColorBrush(Colors.LightGray);
            videreTilKontaktIkon.Foreground = new SolidColorBrush(Colors.LightGray);
        }
        private void VælgTidEnter(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
        }

        private void VælgTidExit(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
        private void VælgTidTapped(object sender, TappedRoutedEventArgs e)
        {
            foreach (var textblock in tblist)
            {
                textblock.FontWeight = FontWeights.Normal;
            }

            int index = int.Parse(((TextBlock)e.OriginalSource).AccessKey);
            string name = ((TextBlock)e.OriginalSource).Name;
            TextBlock tb = (TextBlock)FindName(name);
            tb.FontWeight = FontWeights.SemiBold;

            BookingProcessTidspunkt.Text = selectDate.Date.Date.ToString("dd/MM yyyy") + " kl: " + ((TextBlock)e.OriginalSource).Text;
            BookingProcessArrow2.Visibility = Visibility.Visible;
            videreTilKontaktT.Foreground = new SolidColorBrush(Colors.DodgerBlue);
            videreTilKontaktIkon.Foreground = new SolidColorBrush(Colors.DodgerBlue);
        }

        private void ChangeCursorTilKontaktEnter(object sender, PointerRoutedEventArgs e)
        {
            if (BookingProcessTidspunkt.Text != "")
            {
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            }
        }

        private void ChangeCursorTilKontaktExit(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }

        private void LandeKodeOpen_Tapped(object sender, TappedRoutedEventArgs e)
        {
            if (LandeKodePopup.IsOpen)
            {
                LandeKodePopupBorder.IsOpen = false;
                LandeKodePopup.IsOpen = false;
                LandeKodeOpenBack.Fill = new SolidColorBrush(Color.FromArgb(179, 255, 255, 255));
                LandeKodeOpenIkonDown.Visibility = Visibility.Visible;
                LandeKodeOpenIkonUp.Visibility = Visibility.Collapsed;
            }
            else
            {
                LandeKodePopupBorder.IsOpen = true;
                LandeKodePopup.IsOpen = true;
                LandeKodeOpenBack.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
                LandeKodeOpenIkonDown.Visibility = Visibility.Collapsed;
                LandeKodeOpenIkonUp.Visibility = Visibility.Visible;
            }
        }

        private void LandeKodeOpen_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            LandeKodeOpenBack.Fill = new SolidColorBrush(Color.FromArgb(255, 255, 255, 255));
        }

        private void LandeKodeOpen_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            if (!LandeKodePopup.IsOpen)
            {
                LandeKodeOpenBack.Fill = new SolidColorBrush(Color.FromArgb(179, 255, 255, 255));
            }
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
        int kontaktTlfValidate = 0;
        private void KontaktTlf_TextChanged(object sender, TextChangedEventArgs e)
        {
            kontaktTlfValidate = Regex.Matches(KontaktTlf.Text, @"[a-åA-Å]").Count;
            if (KontaktTlf.Text == "" || kontaktTlfValidate > 0)
            {
                VidereTilKontaktOplBackground.Fill = new SolidColorBrush(Color.FromArgb(255, 189, 189, 189));
            }
            else if (kontaktTlfValidate == 0)
            {
                VidereTilKontaktOplBackground.Fill = new SolidColorBrush(Colors.DodgerBlue);
            }
        }

        private void HyperlinkButton_Click(object sender, RoutedEventArgs e)
        {
            AngivKontakOplGrid.Visibility = Visibility.Collapsed;
            AngivTlfGrid.Visibility = Visibility.Visible;
            videreTilBekræftT.Visibility = Visibility.Collapsed;
            videreTilBekræftIkon.Visibility = Visibility.Collapsed;
        }

        private void VidereTilKontaktOplTekst_PointerEntered(object sender, PointerRoutedEventArgs e)
        {
            if (KontaktTlf.Text != "" && kontaktTlfValidate == 0)
            {
                Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Hand, 1);
            }
        }

        private void VidereTilKontaktOplTekst_PointerExited(object sender, PointerRoutedEventArgs e)
        {
            Window.Current.CoreWindow.PointerCursor = new Windows.UI.Core.CoreCursor(Windows.UI.Core.CoreCursorType.Arrow, 2);
        }
    }
}
