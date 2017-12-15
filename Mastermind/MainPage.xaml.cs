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

        // Method fire check button event
        private void RadioButton_Checked(object sender, RoutedEventArgs e)
        {
            // Sender
            RadioButton current = (RadioButton)sender;
  
            // Generate Board
            createBoard();
        } // RadioButton_Checked()

        // Method create board
        private void createBoard()
        {
            // Create grid for the gameboard
            Grid gridBoard = new Grid
            {
                Name = "GameBoard",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 75 * _rows,
                Width = 75 * _rows,
                Background = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(5)
            };
            gridBoard.SetValue(Grid.RowProperty, 2);

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
