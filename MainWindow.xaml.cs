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


    
        }

 
        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            BoardColumn.Width = new GridLength(this.ActualHeight);
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //chessboard.HighlightField(Figure.Knight, new Coordinate(2, 0));
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

                    }

                }


                if (chessboard.GetAttackedFieldsOfColor(PieceColor.White).Contains(new Coordinate(StartX,StartY)) == false && chessboard.GetSquareByCoordinate(new Coordinate(StartX,StartY)).GetPiece == null)
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
            TimeSpan timeSpan = s.Elapsed;
            Timer.Content = String.Format("Time: {0}h {1}m {2}s {3}ms", timeSpan.Hours, timeSpan.Minutes, timeSpan.Seconds, timeSpan.Milliseconds);
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


                if (chessboard.GetAttackedFieldsOfColor(PieceColor.White).Contains(new Coordinate(StartX, StartY)))
                {
    
                    chessboard.Mark(coordinateChanger());
                }
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
