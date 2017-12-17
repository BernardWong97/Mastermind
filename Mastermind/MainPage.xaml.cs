using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Mastermind
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Global Variables
        // Constants
        private const int _rows = 10;
        // Variables
        private int i, j;
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
        }

        // Method fire click button event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Sender
            Button current = (Button)sender;

            // Generate Board
            CreateBoard();

            // Disable and Invisible after button clicked
            current.IsEnabled = false;
            current.Visibility = Visibility.Collapsed;
            parentGrid.Children.Remove(FindName("startPanel") as StackPanel);
        } // Button_Click()

        // Method create board
        private void CreateBoard()
        {
            // Create grid for the gameboard
            Grid gridBoard = new Grid
            {
                Name = "GameBoard",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 100 * _rows,
                Width = 100 * _rows,
                Background = new SolidColorBrush(Colors.MintCream),
                Margin = new Thickness(5),
            };
            gridBoard.SetValue(Grid.RowProperty, 2);
            gridBoard.SetValue(Grid.ColumnProperty, 2);

            // Add rows number of row definitions and column definitions
            for (i = 0; i < _rows; i++)
            {
                gridBoard.RowDefinitions.Add(new RowDefinition());
            } // for

            for (i = 0; i < 4; i++)
            {
                gridBoard.ColumnDefinitions.Add(new ColumnDefinition());
            } // for

            parentGrid.Children.Add(gridBoard);
        } // createBoard()

    }
}
