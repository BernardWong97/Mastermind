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
        private const int _rows = 5;
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

            // Create grid for the answer pegs
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

            // Create grid for the display pegs
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
                for (j = 0; j < 4; j++)
                {
                    displayGrid.ColumnDefinitions.Add(new ColumnDefinition());
                    displayGrid.ColumnDefinitions[j].Width = new GridLength(180);
                } // for j
            } // for i
            PlaceEmptyPegs(displayGrid);
            gridBoard.Children.Add(displayGrid);

            // Create grid for the feedback pegs
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

            // Create grid for the choose pegs
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
            for(i = 0; i < 4; i++)
            {
                chooseGrid.RowDefinitions.Add(new RowDefinition());
                chooseGrid.RowDefinitions[i].Height = new GridLength(100);
                chooseGrid.ColumnDefinitions.Add(new ColumnDefinition());
            }
            PlaceChoosePegs(chooseGrid);
            gridBoard.Children.Add(chooseGrid);
        } // CreateBoard()

        private void PlaceChoosePegs(Grid chooseGrid)
        {
            Ellipse choosePeg;
            StackPanel stackPanel;
            for(i = 0; i < 2; i++)
            {
                for(j = 0; j < 4; j++)
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
                    stackPanel.Children.Add(choosePeg);
                    stackPanel.SetValue(Grid.RowProperty, i);
                    stackPanel.SetValue(Grid.ColumnProperty, j);
                    chooseGrid.Children.Add(stackPanel);
                } // for j
            } // for i
        } // PlaceChoosePegs()


        private void PlaceEmptyPegs(Grid displayGrid)
        {
            Ellipse emptyPeg;
            StackPanel stackPanel;
            for (i = 0; i < _rows; i++)
            {
                for (j = 0; j < 4; j++)
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
                        Name = "emptyPeg " + (i + 1).ToString(),
                        Height = 80,
                        Width = 80,
                        HorizontalAlignment = HorizontalAlignment.Center,
                        VerticalAlignment = VerticalAlignment.Center,
                        Fill = new SolidColorBrush(Colors.LightGray),
                        Stroke = new SolidColorBrush(Colors.Black),
                        StrokeThickness = 2
                    };

                    stackPanel.Children.Add(emptyPeg);
                    stackPanel.SetValue(Grid.RowProperty, i);
                    stackPanel.SetValue(Grid.ColumnProperty, j);
                    displayGrid.Children.Add(stackPanel);
                } // for j
            } // for i
        } // PlaceEmptyPegs()

        private void PlaceEmptyFeedbackPegs(Grid feedbackGrid)
        {
            Ellipse emptyPeg;
            Grid smallGrid;
            for (i = 0; i < _rows; i++)
            {
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

                smallGrid.SetValue(Grid.RowProperty, i);
                feedbackGrid.Children.Add(smallGrid);
            } // for i
        } // PlaceEmptyFeedbackPegs

        private void PlaceAnswerPegs(Grid answerGrid)
        {
            Ellipse questionPeg;
            StackPanel stackPanel;
            ImageBrush ellipseBackground = new ImageBrush();
            ellipseBackground.ImageSource = new BitmapImage(new Uri("ms-appx:/Assets/questionMark.png", UriKind.Absolute));
            for (i = 0; i < 4; i++)
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
                stackPanel.Children.Add(questionPeg);
                stackPanel.SetValue(Grid.ColumnProperty, i);
                answerGrid.Children.Add(stackPanel);
            } // for i
        } // PlaceAnswerPegs()
    } // main
} // namespace
