using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Xml.Linq;

namespace KnightTraining
{
    public enum Figure
    {
        Pawn,
        Knight,
        Rook,
        King,
        Queen,
        Bishop
    }


    public delegate void PieceDrag(DraggableObject piece);
    public delegate void PieceDrop(DraggableObject piece);
    public delegate void PieceRemoved(DraggableObject piece);

    /// <summary>
    /// Interaktionslogik für ChessBoard.xaml
    /// </summary>
    public partial class ChessBoard : UniformGrid
    {
        public List<Square> squares = new List<Square>();

        public event PieceDrag OnPieceDrag;
        public event PieceDrag OnPieceDrop;
        public event PieceRemoved OnPieceRemoved;

        public void InvokePieceDrag(DraggableObject piece)
        {
            OnPieceDrag?.Invoke(piece);
        }

        public void InvokePieceDrop(DraggableObject piece)
        {
            
            OnPieceDrop?.Invoke(piece);
        }

        public void InvokePieceRemoved(DraggableObject piece)
        {

            OnPieceRemoved?.Invoke(piece);
        }

        public ChessBoard()
        {
            InitializeComponent();

            bool ColorSwitch = false;
            for (int i = 1; i < 10; i++)
            {
                for (int j = 1; j < 10; j++)
                {
                    Square square = new Square();

                    if (ColorSwitch == true)
                    {
                        square.InitialColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#b58863");
                        square.Background = square.InitialColor;
                    }
                    else
                    {
                        square.InitialColor = (SolidColorBrush)new BrushConverter().ConvertFrom("#f0d9b5");
                        square.Background = square.InitialColor;
                    }

                    if (j < 8)
                        ColorSwitch = !ColorSwitch;

                    square.coordinat.x = j;
                    square.coordinat.y = 9 - i;


                    if (j > 8 || i > 8)
                    {
                        square.InitialColor = new SolidColorBrush(Colors.Transparent);
                        square.Background = square.InitialColor;


                        if (i > 8)
                        {
                            if (j < 9)
                            {
                                square.Children.Add(new TextBlock() { Text = GetColumnName(j - 1), HorizontalAlignment = HorizontalAlignment.Center });
                                square.DisableDrop = true;
                            }
                        }

                        if (j > 8)
                        {
                            if (i < 9)
                            {
                                square.Children.Add(new TextBlock() { Text = " " + (9 - i).ToString(), VerticalAlignment = VerticalAlignment.Center });
                                square.DisableDrop = true;
                            }
                        }

                    }

                    ////
                    



                    this.Children.Add(square);
                    squares.Add(square);
                }


                

            }
        }


        string GetColumnName(int index)
        {
            const string letters = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";

            var value = "";

            if (index >= letters.Length)
                value += letters[index / letters.Length - 1];

            value += letters[index % letters.Length];

            return value;
        }

        public Square GetSquareByCoordinate(Coordinate coordinate)
        {
            if (coordinate.x > 8 || coordinate.y > 8)
                return null;
            else
                return squares.Find(x => x.coordinat.Equals(coordinate));

        }

        public void ClearHighligtedFields()
        {
            foreach(Square square in squares)
            {
                square.Background = square.InitialColor;
            }
        }



        public Coordinate MarkedCoordinate = new Coordinate(0, 0);
        public void Mark(Coordinate coordinate)
        {
            if (coordinate.x == 0 && coordinate.y == 0  )
            {
                if (MarkedCoordinate.x != 0 && MarkedCoordinate.y != 0)

                    GetSquareByCoordinate(MarkedCoordinate).Background = GetSquareByCoordinate(MarkedCoordinate).InitialColor;
            }
            else if (MarkedCoordinate.x != coordinate.x || MarkedCoordinate.y != coordinate.y)
            {
                MarkedCoordinate = coordinate;
            }
        }


        public List<Coordinate> GetAttackedFieldsOfColor(PieceColor pieceColor)
        {
            List<Coordinate> result = new List<Coordinate>();

            foreach (DraggableObject piece in this.FindVisualChilds<DraggableObject>().Where(t => t.pieceColor == pieceColor))
            {
                foreach (Coordinate attackingField in piece.AttackingFields())
                {

                    int x = attackingField.x;
                    int y = attackingField.y;

                    if (x > 0 && y > 0)
                    {
                        result.Add(new Coordinate(x, y));
                    }
                }
            }

            return result;

        }



        public bool HighlightOnlySelected { get; set; } = true;
        public void HighlightFields()
        {
            ClearHighligtedFields();


            if (HighlightOnlySelected == false)
            {
                foreach (DraggableObject piece in this.FindVisualChilds<DraggableObject>())
                {
                    foreach (Coordinate attackingField in piece.AttackingFields())
                    {

                        int x = attackingField.x;
                        int y = attackingField.y;

                        if (x > 0 && y > 0)
                        {
                            var a = GetSquareByCoordinate(new Coordinate(x, y));

                            if (a != null)
                            {
                                a.Background = new SolidColorBrush(Colors.IndianRed);
                            }
                        }
                    }
                }
            }
            else
            {
                var Selected = this.FindVisualChilds<DraggableObject>().Where(x => x == DraggableObject.SelectedPiece).FirstOrDefault();
                if(Selected != null)
                {
                    foreach (Coordinate attackingField in Selected.AttackingFields())
                    {

                        int x =  attackingField.x;
                        int y =  attackingField.y;

                        if (x > 0 && y > 0)
                        {
                            var a = GetSquareByCoordinate(new Coordinate(x, y));

                            if (a != null)
                            {
                                a.Background = new SolidColorBrush(Colors.IndianRed);
                            }
                        }
                    }
                }

            }

            if (MarkedCoordinate.x != 0 && MarkedCoordinate.y != 0)
                GetSquareByCoordinate(MarkedCoordinate).Background = new SolidColorBrush(Colors.LightSteelBlue);


        }

    }
}
