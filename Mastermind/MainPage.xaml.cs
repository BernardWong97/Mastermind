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
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Windows.UI.Xaml.Shapes;

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
        private const int _rows = 5, _columns = 4;
        // Variables
        private int i, j;
        private int currentRow = 0, currentColumn = 0;

        public Grid chooseGrid { get; private set; }
        #endregion

        public MainPage()
        {
            this.InitializeComponent();
        }

        // Method create board
        private void CreateBoard()
        {
            // Create grid for the gameboard
            #region gridBoard
            Grid gridBoard = new Grid
            {
                Name = "GameBoard",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 1000,
                Width = 1000,
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
            gridBoard.RowDefinitions[2].Height = new GridLength(400);
            gridBoard.ColumnDefinitions[1].Width = new GridLength(250);
            parentGrid.Children.Add(gridBoard);
            #endregion

            // Create grid for the answer pegs
            #region answerGrid
            Grid answerGrid = new Grid
            {
                Name = "AnswerGrid",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 100,
                Width = 1000,
                Margin = new Thickness(5)
            };
            answerGrid.SetValue(Grid.RowProperty, 0);
            answerGrid.SetValue(Grid.ColumnSpanProperty, 2);
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            answerGrid.ColumnDefinitions.Add(new ColumnDefinition());
            PlaceAnswerPegs(answerGrid);
            gridBoard.Children.Add(answerGrid);
            #endregion

            // Create grid for the display pegs
            #region displayGrid
            Grid displayGrid = new Grid
            {
                Name = "DisplayGrid",
                HorizontalAlignment = HorizontalAlignment.Left,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 1000,
                Width = 1000,
                Margin = new Thickness(5)
            };
            displayGrid.SetValue(Grid.RowProperty, 1);
            displayGrid.SetValue(Grid.ColumnProperty, 0);
            for (i = 0; i < _rows; i++)
            {
                displayGrid.RowDefinitions.Add(new RowDefinition());
                displayGrid.RowDefinitions[i].Height = new GridLength(90);
                for (j = 0; j < _columns; j++)
                {
                    displayGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    displayGrid.ColumnDefinitions[j].Width = new GridLength(180);
                } // for j
            } // for i
            PlaceEmptyPegs(displayGrid);
            gridBoard.Children.Add(displayGrid);
            #endregion

            // Create grid for the feedback pegs
            #region feedbackGrid
            Grid feedbackGrid = new Grid
            {
                Name = "FeedbackGrid",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 1000,
                Width = 1000,
                Margin = new Thickness(5)
            };
            feedbackGrid.SetValue(Grid.RowProperty, 1);
            feedbackGrid.SetValue(Grid.ColumnProperty, 2);
            for (i = 0; i < _rows; i++)
            {
                feedbackGrid.RowDefinitions.Add(new RowDefinition());
                feedbackGrid.RowDefinitions[i].Height = new GridLength(90);
            } // for i
            PlaceEmptyFeedbackPegs(feedbackGrid);
            gridBoard.Children.Add(feedbackGrid);
            #endregion

            // Create grid for the choose pegs
            #region chooseGrid
            Grid chooseGrid = new Grid
            {
                Name = "ChooseGrid",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 1000,
                Width = 1000,
                Margin = new Thickness(5)
            };
            chooseGrid.SetValue(Grid.RowProperty, 2);
            chooseGrid.SetValue(Grid.ColumnSpanProperty, 2);
            for(i = 0; i < _columns; i++)
            {
                chooseGrid.RowDefinitions.Add(new RowDefinition());
                chooseGrid.RowDefinitions[i].Height = new GridLength(100);
                chooseGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            PlaceChoosePegs(chooseGrid);
            gridBoard.Children.Add(chooseGrid);
            #endregion
        } // CreateBoard()

        #region Place Pegs on Boards Methods
        // Method place pegs that user can choose
        private void PlaceChoosePegs(Grid chooseGrid)
        {
            // Variables
            Ellipse choosePeg;
            StackPanel stackPanel;
            // For loop 2 rows and 4 columns
            for(i = 0; i < 2; i++)
            {
                for(j = 0; j < _columns; j++)
                {
                    stackPanel = new StackPanel()
                    {
                        Height = 80,
                        Width = 80,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };

                    choosePeg = new Ellipse
                    {
                        Height = 80,
                        Width = 80,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeThickness = 2
                    };

                    // each pegs different colors
                    if (i == 0)
                    {
                        switch (j)
                        {
                            case 0:
                                choosePeg.Name = "red";
                                choosePeg.Fill = new SolidColorBrush(Colors.Red);
                                break;
                            case 1:
                                choosePeg.Name = "orange";
                                choosePeg.Fill = new SolidColorBrush(Colors.Orange);
                                break;
                            case 2:
                                choosePeg.Name = "yellow";
                                choosePeg.Fill = new SolidColorBrush(Colors.Yellow);
                                break;
                            case 3:
                                choosePeg.Name = "green";
                                choosePeg.Fill = new SolidColorBrush(Colors.Green);
                                break;
                        } // switch
                    } // if
                    else
                    {
                        switch (j)
                        {
                            case 0:
                                choosePeg.Name = "blue";
                                choosePeg.Fill = new SolidColorBrush(Colors.Blue);
                                break;
                            case 1:
                                choosePeg.Name = "indigo";
                                choosePeg.Fill = new SolidColorBrush(Colors.Indigo);
                                break;
                            case 2:
                                choosePeg.Name = "violet";
                                choosePeg.Fill = new SolidColorBrush(Colors.Violet);
                                break;
                            case 3:
                                choosePeg.Name = "pink";
                                choosePeg.Fill = new SolidColorBrush(Colors.Pink);
                                break;
                        } // switch
                    } // else

                    // Tapped event
                    choosePeg.Tapped += Choose_Tapped;

                    // Add to parent
                    stackPanel.Children.Add(choosePeg);
                    stackPanel.SetValue(Grid.RowProperty, i);
                    stackPanel.SetValue(Grid.ColumnProperty, j);
                    chooseGrid.Children.Add(stackPanel);
                } // for j
            } // for i
        } // PlaceChoosePegs()

        // Method place empty pegs into the board
        private void PlaceEmptyPegs(Grid displayGrid)
        {
            // Variables
            Ellipse emptyPeg;
            StackPanel stackPanel;

            // For loop 5 rows 4 column
            for (i = 0; i < _rows; i++)
            {
                for (j = 0; j < _columns; j++)
                {
                    stackPanel = new StackPanel()
                    {
                        Height = 80,
                        Width = 80,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                    };

                    emptyPeg = new Ellipse
                    {
                        Name = "emptyPeg " + (i + 1).ToString() + " " + (j + 1).ToString(),
                        Height = 80,
                        Width = 80,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Fill = new SolidColorBrush(Colors.LightGray),
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeThickness = 2
                    };

                    // Add to parent
                    stackPanel.Children.Add(emptyPeg);
                    stackPanel.SetValue(Grid.RowProperty, i);
                    stackPanel.SetValue(Grid.ColumnProperty, j);
                    displayGrid.Children.Add(stackPanel);
                } // for j
            } // for i
        } // PlaceEmptyPegs()

        // Method place empty feedback pegs into the board
        private void PlaceEmptyFeedbackPegs(Grid feedbackGrid)
        {
            // Variables
            Ellipse emptyPeg;
            Grid smallGrid;

            // For loop 5 rows
            for (i = 0; i < _rows; i++)
            {
                // Create a smaller grid 2x2 to put pegs
                smallGrid = new Grid()
                {
                    Name = "smallGrid" + (i+1).ToString(),
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Top,
                    Height = 80,
                    Width = 80,
                    Background = new SolidColorBrush(Colors.SandyBrown),
                    Margin = new Thickness(2)
                };
                smallGrid.RowDefinitions.Add(new RowDefinition());
                smallGrid.RowDefinitions.Add(new RowDefinition());
                smallGrid.ColumnDefinitions.Add(new ColumnDefinition());
                smallGrid.ColumnDefinitions.Add(new ColumnDefinition());

                // For loop 2 rows 2 columns
                for(j = 0; j < 2; j++)
                {
                    for(int k = 0; k < 2; k++)
                    {
                        emptyPeg = new Ellipse
                        {
                            Name = "emptyFeedbackPeg " + (i + 1).ToString() + " " + (j + 1).ToString(),
                            Height = 22.5,
                            Width = 22.5,
                            HorizontalAlignment = HorizontalAlignment.Center,
                            VerticalAlignment = VerticalAlignment.Center,
                            Fill = new SolidColorBrush(Colors.LightGray),
                            Stroke = new SolidColorBrush(Colors.Black),
                            StrokeThickness = 2
                        };
                        emptyPeg.SetValue(Grid.RowProperty, j);
                        emptyPeg.SetValue(Grid.ColumnProperty, k);
                        smallGrid.Children.Add(emptyPeg);
                    } // for k
                } // for j

                // Add to parent
                smallGrid.SetValue(Grid.RowProperty, i);
                feedbackGrid.Children.Add(smallGrid);
            } // for i
        } // PlaceEmptyFeedbackPegs

        // Method place answer pegs into the board
        private void PlaceAnswerPegs(Grid answerGrid)
        {
            // Variables
            Ellipse questionPeg;
            StackPanel stackPanel;
            // Image brush for the ellipse texture
            ImageBrush ellipseBackground = new ImageBrush();
            ellipseBackground.ImageSource = new BitmapImage(new Uri("ms-appx:/Assets/questionMark.png", UriKind.Absolute));

            // For loop 4 columns
            for (i = 0; i < _columns; i++)
            {
                stackPanel = new StackPanel()
                {
                    Height = 80,
                    Width = 80,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                };

                questionPeg = new Ellipse
                {
                    Name = "questionPeg " + (i + 1).ToString(),
                    Height = 80,
                    Width = 80,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    Stroke = new SolidColorBrush(Colors.Black),
                    StrokeThickness = 2,
                    Fill = ellipseBackground
                };

                // Add to parent
                stackPanel.Children.Add(questionPeg);
                stackPanel.SetValue(Grid.ColumnProperty, i);
                answerGrid.Children.Add(stackPanel);
            } // for i
        } // PlaceAnswerPegs()
        #endregion        
        
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

        // Method fire tapped ellipse event
        private void Choose_Tapped(object sender, TappedRoutedEventArgs e)
        {
            // Variables
            Ellipse current = (Ellipse)sender;
            Ellipse selectPeg;

            // If game finish
            if (currentRow == 4 && currentColumn == 4)
                return;

            // if 4th column, change to next row 1st column
            if (currentColumn == 4)
            {
                currentColumn = 0;
                currentRow++;
            } // if

            // Plant the tapped peg into the empty current turn peg
            selectPeg = FindName("emptyPeg " + (currentRow + 1).ToString() + " " + (currentColumn + 1).ToString()) as Ellipse;
            selectPeg.Fill = current.Fill;

            // increment current column
            currentColumn++;
        } // Choose.Tapped()
    } // main
} // namespace
