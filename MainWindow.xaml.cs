using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;

namespace KnightTraining
{
    /// <summary>
    /// Interaktionslogik für MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer.Start();
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            TimeSpan timeSpan = s.Elapsed;
            Timer.Content = String.Format("Time: {0}h {1}m {2}s {3}ms", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
        }


        // This idea searches for 1 possible way, hope to find something in the future.
        public bool PossibleToReach(int initX, int initY)
        {
           
                int x = initX;
                int y = initY;

                Coordinate c1 = new Coordinate(x + 1, y + 2);
                Coordinate c2 = new Coordinate(x + -1, y + 2);
                Coordinate c3 = new Coordinate(x + -1, y + -2);
                Coordinate c4 = new Coordinate(x + 1, y + -2);

                Coordinate c5 = new Coordinate(x + 2, y + 1);
                Coordinate c6 = new Coordinate(x + 2, y + -1);
                Coordinate c7 = new Coordinate(x + -2, y + -1);
                Coordinate c8 = new Coordinate(x + -2, y + 1);

                List<Coordinate> moves = new List<Coordinate>();

                moves.Add(c1);
                moves.Add(c2);
                moves.Add(c3);
                moves.Add(c4);
                moves.Add(c5);
                moves.Add(c6);
                moves.Add(c7);
                moves.Add(c8);

        
 
            if (moves.FindAll(t => t.x > 0 && t.y > 0 && t.y <9 && t.x < 9 && chessboard.GetAttackedFieldsOfColor(PieceColor.White).Contains(t) == false).Any())
                return true;
            else
                return false;
        }



        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BoardColumn.Width = new GridLength(this.ActualHeight);
        }

   
        int StartX = 8;
        int StartY = 8;

        bool start = false;

        System.Diagnostics.Stopwatch s = new System.Diagnostics.Stopwatch();

        private Coordinate coordinateChanger()
        {
            while (true)
            {
                if (start == false)
                {
                    start = true;
                }
                else
                {
                    if(new Coordinate(8,8).Equals(new Coordinate(StartX,StartY)))
                    {

                        

                        s.Restart();
                        s.Start();
                    }

                    StartX = StartX - 1;

                    if (StartX == 0)
                    {
                        StartX = 8;
                        StartY = StartY - 1;
                    }

                    if (StartY == 0)
                    {
                        StartY = 8; // Round 

                        StopTime();
                        TimeSpan timeSpan = s.Elapsed;
                        TimerLast.Content = String.Format("Last: {0}h {1}m {2}s {3}ms", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
                    }

                }


                if (chessboard.GetAttackedFieldsOfColor(PieceColor.White).Contains(new Coordinate(StartX,StartY)) == false && chessboard.GetSquareByCoordinate(new Coordinate(StartX,StartY)).GetPiece == null &&
                    PossibleToReach(StartX,StartY) == true)
                {
                    chessboard.Mark(new Coordinate(StartX, StartY));
                    break;
                }
            }


            return new Coordinate(StartX, StartY);
        }


        public void StopTime(bool invalid = false, string invalidText = "")
        {
            s.Stop(); 
        }


        private void chessboard_OnPieceDrop(DraggableObject piece)
        {
            if (start == false)
            {
                coordinateChanger();
            }

            if (chessboard.GetAttackedFieldsOfColor(PieceColor.White).Contains(new Coordinate(StartX, StartY)))
            {
                chessboard.Mark(coordinateChanger());
            }

            if (piece.coordinate.Equals(new Coordinate(StartX, StartY)) )
            {
                // Reward Soundtrack
                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.reward);
                player.Play();

                coordinateChanger();
            }

            chessboard.HighlightFields();


            if (piece.pieceColor == PieceColor.Black && piece.IsAttackedBy.Any() )
            {
                var attacker = piece.IsAttackedBy.FirstOrDefault();
                attacker.TakePiece(piece);

                start = false;
                StartX = 8;
                StartY = 8;

                s.Restart();
                s.Start();

                chessboard.Mark(coordinateChanger());

                //if (chessboard.GetAttackedFieldsOfColor(PieceColor.White).Contains(new Coordinate(StartX, StartY)))
                //{
                //    chessboard.Mark(coordinateChanger());
                //}

            }
        }

        private void chessboard_OnPieceDrag(DraggableObject piece)
        {
            chessboard.HighlightFields();
        }

        private void chessboard_OnPieceRemoved(DraggableObject piece)
        {
            chessboard.HighlightFields();
        }


        List<FigureData> draggableObjects = new List<FigureData>();


   
    }
}
