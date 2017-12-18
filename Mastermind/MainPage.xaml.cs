using System;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Shapes;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Mastermind
{
    /// <summary>
    /// Bernard Wong - G00341962 - Mastermind Game
    /// </summary>
    public sealed partial class MainPage : Page
    {
        #region Global Variables
        // Constants
        private const int _rows = 5, _columns = 4;
        // Variables
        private int i, j;
        private int currentRow = 0, currentColumn = 0;
        private Ellipse answerPeg1, answerPeg2, answerPeg3, answerPeg4;
        private Random randomNumber = new Random();
        private Boolean win = false;

        public Grid chooseGrid { get; private set; }
        public Ellipse choosePeg { get; private set; }
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
            int pegNum;

            // For loop 5 rows
            for (i = 0; i < _rows; i++)
            {
                pegNum = 1;
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
                            Name = "emptyFeedbackPeg " + (i + 1).ToString() + " " + (pegNum).ToString(),
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
                        pegNum++;
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

                // Create answer pegs with while loop prevent duplicate colors
                if (i == 0)
                {
                    answerPeg1 = CreateAnswerPegs(i);
                    questionPeg.Fill = answerPeg1.Fill;
                }
                else if (i == 1)
                {
                    answerPeg2 = CreateAnswerPegs(i);
                    while (((SolidColorBrush)answerPeg2.Fill).Color == ((SolidColorBrush)answerPeg1.Fill).Color)
                        answerPeg2 = CreateAnswerPegs(i);
                    questionPeg.Fill = answerPeg2.Fill;
                }
                else if (i == 2)
                {
                    answerPeg3 = CreateAnswerPegs(i);
                    while (((SolidColorBrush)answerPeg3.Fill).Color == ((SolidColorBrush)answerPeg2.Fill).Color
                            || ((SolidColorBrush)answerPeg3.Fill).Color == ((SolidColorBrush)answerPeg1.Fill).Color)
                        answerPeg3 = CreateAnswerPegs(i);
                    questionPeg.Fill = answerPeg3.Fill;
                }
                else
                {
                    answerPeg4 = CreateAnswerPegs(i);
                    while (((SolidColorBrush)answerPeg4.Fill).Color == ((SolidColorBrush)answerPeg3.Fill).Color
                        || ((SolidColorBrush)answerPeg4.Fill).Color == ((SolidColorBrush)answerPeg2.Fill).Color
                        || ((SolidColorBrush)answerPeg4.Fill).Color == ((SolidColorBrush)answerPeg1.Fill).Color)
                        answerPeg4 = CreateAnswerPegs(i);
                    questionPeg.Fill = answerPeg4.Fill;
                } // if..else..if

                // Add to parent
                stackPanel.Children.Add(questionPeg);
                stackPanel.SetValue(Grid.ColumnProperty, i);
                answerGrid.Children.Add(stackPanel);
            } // for i
        } // PlaceAnswerPegs()
        #endregion

        #region Event handlers
        // Method fire click button event
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Sender
            Button current = (Button)sender;

            // Generate Board
            CreateBoard();

            // Try remove useless grid
            try
            {
                parentGrid.Children.Remove(FindName("GameOverGrid") as Grid);
            }
            catch{}

            // Reset Variables
            currentRow = 0;
            currentColumn = 0;
            win = false;

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
            {
                GameOver();
                return;
            } // if

            // if 4th column, change to next row 1st column
            if (currentColumn == 4)
            {
                currentColumn = 0;
                currentRow++;
            } // if

            // Plant the tapped peg into the empty current turn peg
            selectPeg = FindName("emptyPeg " + (currentRow + 1).ToString() + " " + (currentColumn + 1).ToString()) as Ellipse;
            selectPeg.Fill = current.Fill;

            // if row done, give feedback
            if (currentColumn == 3)
                FeedbackPegs(currentRow);

            // increment current column
            currentColumn++;
        } // Choose.Tapped()
        #endregion

        #region Additional Methods
        // Method create answer pegs
        private Ellipse CreateAnswerPegs(int i)
        {
            // Instantiate answer pegs
            Ellipse answerPeg = new Ellipse
            {
                Name = "answerPeg " + (i + 1).ToString(),
                Height = 80,
                Width = 80,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
                Stroke = new SolidColorBrush(Colors.Black),
                StrokeThickness = 2,
            };

            // Generate random number
            int random = randomNumber.Next(8);

            // Switch statement generates random colors
            switch (random)
            {
                case 0:
                    answerPeg.Fill = new SolidColorBrush(Colors.Red);
                    break;
                case 1:
                    answerPeg.Fill = new SolidColorBrush(Colors.Orange);
                    break;
                case 2:
                    answerPeg.Fill = new SolidColorBrush(Colors.Yellow);
                    break;
                case 3:
                    answerPeg.Fill = new SolidColorBrush(Colors.Green);
                    break;
                case 4:
                    answerPeg.Fill = new SolidColorBrush(Colors.Blue);
                    break;
                case 5:
                    answerPeg.Fill = new SolidColorBrush(Colors.Indigo);
                    break;
                case 6:
                    answerPeg.Fill = new SolidColorBrush(Colors.Violet);
                    break;
                case 7:
                    answerPeg.Fill = new SolidColorBrush(Colors.Pink);
                    break;
            } // switch

            return answerPeg;
        } // CreateAnswerPegs()

        // Method for feedback function
        private void FeedbackPegs(int currentRow)
        {
            // Variables
            Ellipse currentPeg1, currentPeg2, currentPeg3, currentPeg4;
            Ellipse currentFeedbackPeg;
            int feedbackPegNum = 1;

            // Instantiate currentPegs
            currentPeg1 = FindName("emptyPeg " + (currentRow + 1).ToString() + " 1") as Ellipse;
            currentPeg2 = FindName("emptyPeg " + (currentRow + 1).ToString() + " 2") as Ellipse;
            currentPeg3 = FindName("emptyPeg " + (currentRow + 1).ToString() + " 3") as Ellipse;
            currentPeg4 = FindName("emptyPeg " + (currentRow + 1).ToString() + " 4") as Ellipse;

            #region black pegs
            // if position and color matches, black pegs
            if (((SolidColorBrush)currentPeg1.Fill).Color == ((SolidColorBrush)answerPeg1.Fill).Color)
            {
                currentFeedbackPeg = FindName("emptyFeedbackPeg " + (currentRow + 1).ToString() + " " + (feedbackPegNum).ToString()) as Ellipse;
                currentFeedbackPeg.Fill = new SolidColorBrush(Colors.Black);
                feedbackPegNum++;
            } // if

            if(((SolidColorBrush)currentPeg2.Fill).Color == ((SolidColorBrush)answerPeg2.Fill).Color)
            {
                currentFeedbackPeg = FindName("emptyFeedbackPeg " + (currentRow + 1).ToString() + " " + (feedbackPegNum).ToString()) as Ellipse;
                currentFeedbackPeg.Fill = new SolidColorBrush(Colors.Black);
                feedbackPegNum++;
            } // if

            if (((SolidColorBrush)currentPeg3.Fill).Color == ((SolidColorBrush)answerPeg3.Fill).Color)
            {
                currentFeedbackPeg = FindName("emptyFeedbackPeg " + (currentRow + 1).ToString() + " " + (feedbackPegNum).ToString()) as Ellipse;
                currentFeedbackPeg.Fill = new SolidColorBrush(Colors.Black);
                feedbackPegNum++;
            } // if

            if (((SolidColorBrush)currentPeg4.Fill).Color == ((SolidColorBrush)answerPeg4.Fill).Color)
            {
                currentFeedbackPeg = FindName("emptyFeedbackPeg " + (currentRow + 1).ToString() + " " + (feedbackPegNum).ToString()) as Ellipse;
                currentFeedbackPeg.Fill = new SolidColorBrush(Colors.Black);
                feedbackPegNum++;
            } // if
            #endregion

            #region white pegs
            // if color match but wrong position
            if (feedbackPegNum < 5 
                && ((SolidColorBrush)currentPeg1.Fill).Color == ((SolidColorBrush)answerPeg2.Fill).Color
                || ((SolidColorBrush)currentPeg1.Fill).Color == ((SolidColorBrush)answerPeg3.Fill).Color
                || ((SolidColorBrush)currentPeg1.Fill).Color == ((SolidColorBrush)answerPeg4.Fill).Color)
            {
                currentFeedbackPeg = FindName("emptyFeedbackPeg " + (currentRow + 1).ToString() + " " + (feedbackPegNum).ToString()) as Ellipse;
                currentFeedbackPeg.Fill = new SolidColorBrush(Colors.White);
                feedbackPegNum++;
            } // if

            if (feedbackPegNum < 5
                && ((SolidColorBrush)currentPeg2.Fill).Color == ((SolidColorBrush)answerPeg1.Fill).Color
                || ((SolidColorBrush)currentPeg2.Fill).Color == ((SolidColorBrush)answerPeg3.Fill).Color
                || ((SolidColorBrush)currentPeg2.Fill).Color == ((SolidColorBrush)answerPeg4.Fill).Color)
            {
                currentFeedbackPeg = FindName("emptyFeedbackPeg " + (currentRow + 1).ToString() + " " + (feedbackPegNum).ToString()) as Ellipse;
                currentFeedbackPeg.Fill = new SolidColorBrush(Colors.White);
                feedbackPegNum++;
            } // if
            if (feedbackPegNum < 5
                && ((SolidColorBrush)currentPeg3.Fill).Color == ((SolidColorBrush)answerPeg1.Fill).Color
                || ((SolidColorBrush)currentPeg3.Fill).Color == ((SolidColorBrush)answerPeg2.Fill).Color
                || ((SolidColorBrush)currentPeg3.Fill).Color == ((SolidColorBrush)answerPeg4.Fill).Color)
            {
                currentFeedbackPeg = FindName("emptyFeedbackPeg " + (currentRow + 1).ToString() + " " + (feedbackPegNum).ToString()) as Ellipse;
                currentFeedbackPeg.Fill = new SolidColorBrush(Colors.White);
                feedbackPegNum++;
            } // if
            if (feedbackPegNum < 5
                && ((SolidColorBrush)currentPeg4.Fill).Color == ((SolidColorBrush)answerPeg1.Fill).Color
                || ((SolidColorBrush)currentPeg4.Fill).Color == ((SolidColorBrush)answerPeg2.Fill).Color
                || ((SolidColorBrush)currentPeg4.Fill).Color == ((SolidColorBrush)answerPeg3.Fill).Color)
            {
                currentFeedbackPeg = FindName("emptyFeedbackPeg " + (currentRow + 1).ToString() + " " + (feedbackPegNum).ToString()) as Ellipse;
                currentFeedbackPeg.Fill = new SolidColorBrush(Colors.White);
                feedbackPegNum++;
            } // if
            #endregion

            // if user win the game
            Ellipse forthFeedbackPeg = FindName("emptyFeedbackPeg " + (currentRow + 1).ToString() + " " + (4).ToString()) as Ellipse;
            SolidColorBrush black = new SolidColorBrush(Colors.Black);
            if (((SolidColorBrush)forthFeedbackPeg.Fill).Color == black.Color)
            {
                win = true;
                GameOver();
            } // if
                
        } // FeedbackPegs()

        // Method Game Over
        private void GameOver()
        {
            // Variables
            Grid gameOverGrid;
            StackPanel stackPanel;
            TextBlock textBlock;
            Button restartBtn;

            // Try remove the game board
            gameOverGrid = new Grid()
            {
                Name = "GameOverGrid",
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top,
                Height = 1000,
                Width = 1000,
                Background = new SolidColorBrush(Colors.MintCream),
                Margin = new Thickness(5)
            };
            gameOverGrid.RowDefinitions.Add(new RowDefinition());
            gameOverGrid.SetValue(Grid.RowProperty, 2);
            gameOverGrid.SetValue(Grid.ColumnProperty, 0);

            stackPanel = new StackPanel()
            {
                Height = 1000,
                Width = 1000,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center
            };

            textBlock = new TextBlock()
            {
                Height = 500,
                Width = 500,
                Foreground = new SolidColorBrush(Colors.Red),
                FontSize = 20,
                TextAlignment = TextAlignment.Center,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Center,
            };

            if (win)
            {
                textBlock.Text = "Game Over! You Win!";
            }
            else
            {
                textBlock.Text = "Game Over! You Lose!";
            } // if

            restartBtn = new Button()
            {
                Content = "Play Again",
                Tag = "restart",
                Margin = new Thickness(10),
                Width = 150,
                HorizontalAlignment = HorizontalAlignment.Center,
                VerticalAlignment = VerticalAlignment.Top
            };

            restartBtn.Click += Button_Click;

            stackPanel.Children.Add(textBlock);
            stackPanel.Children.Add(restartBtn);
            stackPanel.SetValue(Grid.RowProperty, 0);
            gameOverGrid.Children.Add(stackPanel);
            parentGrid.Children.Add(gameOverGrid);
        } // GameOver()
        #endregion

    } // main
} // namespace
