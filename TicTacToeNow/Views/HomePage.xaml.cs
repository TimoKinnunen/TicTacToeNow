using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using TicTacToeNow.Models;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

namespace TicTacToeNow.Views
{
    public sealed partial class HomePage : Page
    {
        private MainPage mainPage;

        public HomePage()
        {
            InitializeComponent();

            SizeChanged += Page_SizeChanged;
            Loaded += Page_Loaded;

            mainPage = MainPage.CurrentMainPage;

            NextPlayerTextBlock.Text = GetMarker();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            SetPageContentStackPanelWidth();
        }

        private void Page_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetPageContentStackPanelWidth();
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            // code here
            for (int i = 0; i < 9; i++)
            {
                var marker = App.Markers.FirstOrDefault(m => m.Id == i);
                if (marker != null && !string.IsNullOrEmpty(marker.ButtonName))
                {
                    foreach (UIElement item in BoardGrid.Children)
                    {
                        Button button = item as Button;
                        if (item != null && button.Name == marker.ButtonName)
                        {
                            button.Content = marker.Value;
                        }
                    }
                }
            }
            SetUIStatus();
            GetWinner();
            // code here
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);

            // code here
            // code here
        }

        private void SetPageContentStackPanelWidth()
        {
            PageContentStackPanel.Width = ActualWidth -
                PageContentScrollViewer.Margin.Left -
                PageContentScrollViewer.Padding.Right;
        }

        #region MenuAppBarButton
        private void HomeAppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            mainPage.GoToHomePage();
            mainPage.MenuNavigationListView.SelectedIndex = 0;
        }

        private void NewGameAppBarButton_Tapped(object sender, TappedRoutedEventArgs e)
        {
            App.Markers = new ObservableCollection<Marker>();

            for (int i = 0; i < 9; i++)
            {
                App.Markers.Add(new Marker
                {
                    Id = i,
                    ButtonName = string.Empty,
                    Value = string.Empty
                });
            }

            App.NextPlayer = true;

            App.NumberOfTurns = 0;

            App.Winner = false;

            foreach (UIElement item in BoardGrid.Children)
            {
                Button button = item as Button;
                if (item != null)
                {
                    button.Content = string.Empty;
                    button.Foreground = new SolidColorBrush(Colors.White);
                }
            }

            NextPlayerStackPanel.Visibility = Visibility.Visible;
            WinnerStackPanel.Visibility = Visibility.Collapsed;
            DrawStackPanel.Visibility = Visibility.Collapsed;

            SetUIStatus();
            GetWinner();

            BoardGrid.IsHitTestVisible = true;
        }
        #endregion MenuAppBarButton

        private void TopLeft_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var id = 0;

            SetMarker(button, id);
            SetUIStatus();
            GetWinner();
        }

        private void TopMiddle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var id = 1;

            SetMarker(button, id);
            SetUIStatus();
            GetWinner();
        }

        private void TopRight_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var id = 2;

            SetMarker(button, id);
            SetUIStatus();
            GetWinner();
        }

        private void MiddleLeft_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var id = 3;

            SetMarker(button, id);
            SetUIStatus();
            GetWinner();
        }

        private void Middle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var id = 4;

            SetMarker(button, id);
            SetUIStatus();
            GetWinner();
        }

        private void MiddleRight_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var id = 5;

            SetMarker(button, id);
            SetUIStatus();
            GetWinner();
        }

        private void BottomLeft_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var id = 6;

            SetMarker(button, id);
            SetUIStatus();
            GetWinner();
        }

        private void BottomMiddle_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var id = 7;

            SetMarker(button, id);
            SetUIStatus();
            GetWinner();
        }

        private void BottomRight_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var button = sender as Button;
            var id = 8;

            SetMarker(button, id);
            SetUIStatus();
            GetWinner();
        }

        private static void SetMarker(Button button, int id)
        {
            if (App.Winner)
            {
                return;
            }

            var marker = App.Markers.FirstOrDefault(m => m.Id == id);
            if (button != null && marker != null && string.IsNullOrEmpty(button.Content.ToString()))
            {
                marker.ButtonName = button.Name;
                button.Content = marker.Value = GetMarker();
                App.NextPlayer = !App.NextPlayer;
                App.NumberOfTurns++;
            }
        }

        private void SetUIStatus()
        {
            NumberOfTurns.Text = App.NumberOfTurns.ToString();
            NextPlayerTextBlock.Text = GetMarker();
        }

        private void GetWinner()
        {
            App.Winner = CalculateWinner();
            if (App.Winner)
            {
                WinnerIsTextBlock.Text = App.NextPlayer ? "O" : "X";
                WinnerStackPanel.Visibility = Visibility.Visible;
                NextPlayerStackPanel.Visibility = Visibility.Collapsed;
                BoardGrid.IsHitTestVisible = false;

                foreach (UIElement item in BoardGrid.Children)
                {
                    Button button = item as Button;
                    if (item != null)
                    {
                        if (App.WinnerRow.Contains(button.Name))
                        {
                            button.Foreground = new SolidColorBrush(Colors.YellowGreen);
                        }
                    }
                }
            }

            if (!App.Winner && App.NumberOfTurns == 9)
            {
                foreach (UIElement item in BoardGrid.Children)
                {
                    Button button = item as Button;
                    if (item != null)
                    {
                        button.Foreground = new SolidColorBrush(Colors.Red);
                    }
                }
                WinnerStackPanel.Visibility = Visibility.Collapsed;
                NextPlayerStackPanel.Visibility = Visibility.Collapsed;
                DrawStackPanel.Visibility = Visibility.Visible;
                BoardGrid.IsHitTestVisible = false;
            }
        }

        private static bool CalculateWinner()
        {
            bool winner = false;

            uint[,] winnerLines = new uint[8, 3]
            {
                { 0, 1, 2 },
                { 3, 4, 5 },
                { 6, 7, 8 },
                { 0, 3, 6 },
                { 1, 4, 7 },
                { 2, 5, 8 },
                { 0, 4, 8 },
                { 2, 4, 6 }
            };

            for (uint i = 0; i < 8; i++)
            {
                Marker a = App.Markers.FirstOrDefault(m => m.Id == winnerLines[i, 0]);
                Marker b = App.Markers.FirstOrDefault(m => m.Id == winnerLines[i, 1]);
                Marker c = App.Markers.FirstOrDefault(m => m.Id == winnerLines[i, 2]);

                // This row can not win
                if (a.Value == string.Empty || b.Value == string.Empty || c.Value == string.Empty)
                {
                    continue;
                }

                // This row can win
                if (a.Value == b.Value && a.Value == c.Value)
                {
                    App.WinnerRow = new List<string>();
                    App.WinnerRow.Add(a.ButtonName);
                    App.WinnerRow.Add(b.ButtonName);
                    App.WinnerRow.Add(c.ButtonName);

                    winner = true;
                }
            }

            return winner;
        }

        private static string GetMarker()
        {
            return App.NextPlayer ? "X" : "O";
        }
    }
}
