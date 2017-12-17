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
            gridBoard.RowDefinitions.Add(new RowDefinition());
            gridBoard.RowDefinitions.Add(new RowDefinition());
            gridBoard.RowDefinitions.Add(new RowDefinition());
            gridBoard.ColumnDefinitions.Add(new ColumnDefinition());
            gridBoard.ColumnDefinitions.Add(new ColumnDefinition());
            gridBoard.RowDefinitions[0].Height = new GridLength(100);
            gridBoard.ColumnDefinitions[1].Width = new GridLength(250);
            parentGrid.Children.Add(gridBoard);

            // Create grid for the answer pegs
            Grid answerGrid = new Grid
            {
                Name = "AnswerGrid",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 10 * _rows,
                Width = 100 * _rows,
                Background = new SolidColorBrush(Colors.Gray),
                Margin = new Thickness(5)
            };
            answerGrid.SetValue(Grid.RowProperty, 0);
            answerGrid.SetValue(Grid.ColumnSpanProperty, 2);
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gridBoard.Children.Add(answerGrid);

            // Create grid for the display pegs
            Grid displayGrid = new Grid
            {
                Name = "DisplayGrid",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 100 * _rows,
                Width = 100 * _rows,
                Background = new SolidColorBrush(Colors.Green),
                Margin = new Thickness(5)
            };
            displayGrid.SetValue(Grid.RowProperty, 1);
            displayGrid.SetValue(Grid.ColumnProperty, 0);
            displayGrid.ColumnDefinitions.Add(new ColumnDefinition());
            displayGrid.ColumnDefinitions.Add(new ColumnDefinition());
            displayGrid.ColumnDefinitions.Add(new ColumnDefinition());
            displayGrid.ColumnDefinitions.Add(new ColumnDefinition());
            for(i = 0; i < _rows; i++)
            {
                displayGrid.RowDefinitions.Add(new RowDefinition());
            } // for
            gridBoard.Children.Add(displayGrid);

            // Create grid for the feedback pegs
            Grid feedbackGrid = new Grid
            {
                Name = "FeedbackGrid",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 100 * _rows,
                Width = 100 * _rows,
                Background = new SolidColorBrush(Colors.Blue),
                Margin = new Thickness(5)
            };
            feedbackGrid.SetValue(Grid.RowProperty, 1);
            feedbackGrid.SetValue(Grid.ColumnProperty, 2);
            feedbackGrid.RowDefinitions.Add(new RowDefinition());
            feedbackGrid.RowDefinitions.Add(new RowDefinition());
            feedbackGrid.ColumnDefinitions.Add(new ColumnDefinition());
            feedbackGrid.ColumnDefinitions.Add(new ColumnDefinition());
            gridBoard.Children.Add(feedbackGrid);

            // Create grid for the choose pegs
            Grid chooseGrid = new Grid
            {
                Name = "ChooseGrid",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 100 * _rows,
                Width = 100 * _rows,
                Background = new SolidColorBrush(Colors.Red),
                Margin = new Thickness(5)
            };
            chooseGrid.SetValue(Grid.RowProperty, 2);
            chooseGrid.SetValue(Grid.ColumnSpanProperty, 2);
            for(i = 0; i < 4; i++)
            {
                chooseGrid.RowDefinitions.Add(new RowDefinition());
                chooseGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            gridBoard.Children.Add(chooseGrid);
        } // createBoard()

    }
}
