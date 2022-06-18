using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;

namespace KnightTraining
{

    public enum PieceColor
    {
        Black,
        White
    }
 
    public class AttackingPattern
    {
        public bool DiagonalDirectonLeftUpperCorner { get; set; } = false;
        public bool DiagonalDirectonRightUpperCorner { get; set; } = false;
        public bool DiagonalDirectonLeftBottomCorner { get; set; } = false;
        public bool DiagonalDirectonRightBottomCorner { get; set; } = false;

        public bool HorizontalDirectonLeftRight { get; set; } = false;
        public bool HorizontalDirectonRightLeft { get; set; } = false;
        public bool VerticalDirectionBottomTop { get; set; } = false;
        public bool VerticalDirectionTopBottom { get; set; } = false;

        public bool Knight { get; set; } = false;
        public bool King { get; set; } = false;
        public bool WhitePawn { get; set; } = false;
        public bool BlackPawn { get; set; } = false;


    }

    public class FigureData
    {
        public Figure figure;
        public PieceColor pieceColor = PieceColor.White;
        public Coordinate coordinate = new Coordinate(0, 0);
    }


    public class DraggableObject : Image
    {

        public DraggableObject()
        {
            this.LayoutUpdated += DraggableObject_LayoutUpdated;
            this.Margin = new Thickness(4, 4, 4, 4);
        }

        public Figure figure;


        public AttackingPattern attackingPattern = new AttackingPattern() {};

        public PieceColor pieceColor = PieceColor.White;


        public List<Coordinate> AttackingFields()
        {
            List<Coordinate> afield = new List<Coordinate>();

            if(chessBoard != null)
            {
                if(attackingPattern != null)
                {
                    if(attackingPattern.WhitePawn)
                    {
                        int x = coordinate.x;
                        int y = coordinate.y;


                        
                        if (chessBoard.GetSquareByCoordinate(new Coordinate(x, y + 1))?.GetPiece == null)
                        {
                            Coordinate c1 = new Coordinate(x, y + 1);
                            afield.Add(c1);

                            if (chessBoard.GetSquareByCoordinate(new Coordinate(x, y + 2))?.GetPiece == null && coordinate.y == 2)
                            {
                                Coordinate c0 = new Coordinate(x, y + 2);
                                afield.Add(c0);
                            }
                        }

                        if (chessBoard.GetSquareByCoordinate(new Coordinate( x + 1, y + 1))?.GetPiece != null)
                        {
                            Coordinate c2 = new Coordinate(x + 1, y + 1);
                            afield.Add(c2);

                        }

                        if (chessBoard.GetSquareByCoordinate(new Coordinate(x - 1, y + 1))?.GetPiece != null)
                        {
                            Coordinate c3 = new Coordinate(x - 1, y + 1);
                            afield.Add(c3);

                        }


                    }


                    if (attackingPattern.King)
                    {

                        int x = coordinate.x;
                        int y = coordinate.y;

                        Coordinate c1 = new Coordinate(x, y + 1);
                        Coordinate c2 = new Coordinate(x+1, y + 1);
                        Coordinate c3 = new Coordinate(x+1, y);
                        Coordinate c4 = new Coordinate(x+1, y -1);
                        Coordinate c5 = new Coordinate(x, y -1);
                        Coordinate c6 = new Coordinate(x-1, y -1);
                        Coordinate c7 = new Coordinate(x-1, y);
                        Coordinate c8 = new Coordinate(x-1, y+1);


                        afield.Add(c1);
                        afield.Add(c2);
                        afield.Add(c3);
                        afield.Add(c4);
                        afield.Add(c5);
                        afield.Add(c6);
                        afield.Add(c7);
                        afield.Add(c8);

                    }

                    if (attackingPattern.DiagonalDirectonRightUpperCorner == true)
                    {
                        int x = coordinate.x;
                        int y = coordinate.y;
                        for (int i = 1; i < 9; i++)
                        {
                            x += 1;
                            y += 1;

                            afield.Add(new Coordinate(x, y));


                            if (chessBoard.GetSquareByCoordinate(new Coordinate(x,y))?.GetPiece != null)
                                break;
                            
                        }
                    }


                    if (attackingPattern.DiagonalDirectonLeftUpperCorner == true)
                    {
                        int x = coordinate.x;
                        int y = coordinate.y;
                        for (int i = 1; i < 9; i++)
                        {
                            x -= 1;
                            y += 1;

                            afield.Add(new Coordinate(x, y));


                            if (chessBoard.GetSquareByCoordinate(new Coordinate(x, y))?.GetPiece != null)
                                break;

                        }
                    }

                    if (attackingPattern.DiagonalDirectonLeftBottomCorner == true)
                    {
                        int x = coordinate.x;
                        int y = coordinate.y;
                        for (int i = 1; i < 9; i++)
                        {
                            x -= 1;
                            y -= 1;

                            afield.Add(new Coordinate(x, y));


                            if (chessBoard.GetSquareByCoordinate(new Coordinate(x, y))?.GetPiece != null)
                                break;

                        }
                    }

               

                    if (attackingPattern.DiagonalDirectonRightBottomCorner == true)
                    {
                        int x = coordinate.x;
                        int y = coordinate.y;
                        for (int i = 1; i < 9; i++)
                        {
                            x += 1;
                            y -= 1;

                            afield.Add(new Coordinate(x, y));


                            if (chessBoard.GetSquareByCoordinate(new Coordinate(x, y))?.GetPiece != null)
                                break;

                        }
                    }

                    if(attackingPattern.Knight)
                    {
                        int x = coordinate.x;
                        int y = coordinate.y;

                        Coordinate c1 = new Coordinate(x+1, y+ 2);
                        Coordinate c2 = new Coordinate(x + -1, y + 2);
                        Coordinate c3 = new Coordinate(x + -1, y + -2);
                        Coordinate c4 = new Coordinate(x + 1, y + -2);

                        Coordinate c5 = new Coordinate(x + 2, y + 1);
                        Coordinate c6 = new Coordinate(x + 2, y + -1);
                        Coordinate c7 = new Coordinate(x + -2, y + -1);
                        Coordinate c8 = new Coordinate(x + -2, y + 1);


                        afield.Add(c1);
                        afield.Add(c2);
                        afield.Add(c3);
                        afield.Add(c4);
                        afield.Add(c5);
                        afield.Add(c6);
                        afield.Add(c7);
                        afield.Add(c8);
                    }

                    if (attackingPattern.VerticalDirectionBottomTop == true)
                    {
                        int x = coordinate.x;
                        int y = coordinate.y;
                        for (int i = 1; i < 9; i++)
                        {
                            y += 1;

                            afield.Add(new Coordinate(x, y));


                            if (chessBoard.GetSquareByCoordinate(new Coordinate(x, y))?.GetPiece != null)
                                break;

                        }
                    }

                    if (attackingPattern.VerticalDirectionTopBottom == true)
                    {
                        int x = coordinate.x;
                        int y = coordinate.y;
                        for (int i = 1; i < 9; i++)
                        {
                            y -= 1;

                            afield.Add(new Coordinate(x, y));


                            if (chessBoard.GetSquareByCoordinate(new Coordinate(x, y))?.GetPiece != null)
                                break;

                        }
                    }

                    if (attackingPattern.HorizontalDirectonLeftRight == true)
                    {
                        int x = coordinate.x;
                        int y = coordinate.y;
                        for (int i = 1; i < 9; i++)
                        {
                            x += 1;

                            afield.Add(new Coordinate(x, y));


                            if (chessBoard.GetSquareByCoordinate(new Coordinate(x, y))?.GetPiece != null)
                                break;

                        }
                    }

                    if (attackingPattern.HorizontalDirectonRightLeft == true)
                    {
                        int x = coordinate.x;
                        int y = coordinate.y;
                        for (int i = 1; i < 9; i++)
                        {
                            x -= 1;

                            afield.Add(new Coordinate(x, y));


                            if (chessBoard.GetSquareByCoordinate(new Coordinate(x, y))?.GetPiece != null)
                                break;

                        }
                    }
                }
            }

            return afield.FindAll(t => t.x > 0 && t.y > 0);

        }


        public Point InitialSize { get; set; } = new Point(0, 0);

        public static DraggableObject SelectedPiece = null;



        private void DraggableObject_LayoutUpdated(object sender, EventArgs e)
        {
            if (canvas == null)
                canvas = GetCanvas();

            if (InitialSize == new Point(0, 0))
                InitialSize = new Point(this.ActualWidth, this.ActualHeight);
        }

        public bool IsDragging { get; set; } = false;
        public Coordinate coordinate {
            get
            {
                if (IsInSquare)
                    return square.coordinat;
                else
                    return new Coordinate(0, 0); // 0,0 Not in Square
            }
        }

        private List<Coordinate> LastAttackedFields = new List<Coordinate>();
        private Square LastSquare = null;
        protected override void OnMouseDown(MouseButtonEventArgs e)
        {
            if (copieable == true)
            {
                DraggableObject draggableObject = new DraggableObject();
                draggableObject.Source = this.Source;
                draggableObject.InitialSize = this.InitialSize;
                draggableObject.canvas = canvas;
                draggableObject.Width = InitialSize.X;
                draggableObject.Height = InitialSize.Y;
                draggableObject.figure = figure;
                draggableObject.attackingPattern = this.attackingPattern;
                draggableObject.pieceColor = this.pieceColor;

                canvas.Children.Add(draggableObject);

                Canvas.SetLeft(draggableObject, Mouse.GetPosition(canvas).X - this.ActualWidth / 2);
                Canvas.SetTop(draggableObject, Mouse.GetPosition(canvas).Y - this.ActualHeight / 2);

                SelectedPiece = draggableObject;
                draggableObject.IsDragging = true;

            }
            else
            {
                SelectedPiece = this;
                if (IsInSquare == true)
                {
                    LastAttackedFields = AttackingFields();
                    LastSquare = this.square;

                    ChessBoard Temp = chessBoard;


                    //square.hasPiece = false;
                    square.Children.Remove(this);
                    Temp?.InvokePieceDrag(this);
                    canvas.Children.Add(this);

                    //Restore Size
                    //this.Width = InitialSize.X;
                    //this.Height = InitialSize.Y;

                    this.Width = LastSquare.ActualWidth;
                    this.Height = LastSquare.ActualHeight;

                    InitialSize = new Point(LastSquare.ActualWidth, LastSquare.ActualHeight);

                }

                // Clip to Mouse
                Canvas.SetLeft(this, Mouse.GetPosition(canvas).X - InitialSize.X / 2);
                Canvas.SetTop(this, Mouse.GetPosition(canvas).Y - InitialSize.Y / 2);

                IsDragging = true;
            }



            base.OnMouseDown(e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            if (IsDragging == true)
            {
                if (e.LeftButton == MouseButtonState.Pressed)
                {
                    Canvas.SetLeft(this, Mouse.GetPosition(canvas).X - this.ActualWidth / 2);
                    Canvas.SetTop(this, Mouse.GetPosition(canvas).Y - this.ActualHeight / 2);
                }
            }

            base.OnMouseMove(e);
        }


        public bool OnlyLegalMove { get; set; } = true;


        public List<DraggableObject> IsAttackedBy
        {
            get
            {
                List<DraggableObject> attackers = new List<DraggableObject>();
                foreach (DraggableObject draggableObject in chessBoard.FindVisualChilds<DraggableObject>().Where(x => x.pieceColor != this.pieceColor))
                {
                    if (draggableObject.AttackingFields().Contains(square.coordinat))
                    {
                        attackers.Add(draggableObject);
                    }
                }

                return attackers;
            }
        }

        protected override void OnMouseUp(MouseButtonEventArgs e)
        {
            IsDragging = false;

            if (IsInSquare == false)
            {
                Rect rectThis = new Rect(Mouse.GetPosition(canvas).X - 5, Mouse.GetPosition(canvas).Y - 5, 5 / 2, 5 / 2);

                //foreach (Square tb in FindVisualChilds<Square>(canvas).Where(x => x.DisableDrop == false))
                foreach (Square tb in canvas.FindVisualChilds<Square>().Where(x => x.DisableDrop == false))
                {
                    Point relativePoint = tb.TransformToAncestor(canvas)
                          .Transform(new Point(0, 0));

                    Rect rectContainer = new Rect(relativePoint.X, relativePoint.Y, tb.ActualWidth, tb.ActualHeight);

                    if (rectThis.IntersectsWith(rectContainer))
                    {

                        if(LastAttackedFields.Contains(tb.coordinat) || LastSquare == null || LastSquare == tb) // Legal Move
                        {
                            canvas.Children.Remove(this);

                            if (tb.GetPiece != null)
                            {
                                System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.take);
                                player.Play();
                            }
                            else
                            {
                                if(LastSquare != tb)
                                {
                                    System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.drop);
                                    player.Play();
                                }

                            }


                            tb.Children.Clear();
                            tb.Children.Add(this);

                            this.Width = double.NaN;
                            this.Height = double.NaN;

                            if (chessBoard != null)
                            {
                                chessBoard.InvokePieceDrop(this);
                            }
                        }
                        else // Illegal Move
                        {
                            canvas.Children.Remove(this);
                            LastSquare.Children.Clear();
                            LastSquare.Children.Add(this);

                            //tb.hasPiece = true;
                            this.Width = double.NaN;
                            this.Height = double.NaN;


                        }


                        
                        break;
                    }
                }

                if(this.square == null)
                {
                    if(chessBoard != null)
                        chessBoard.InvokePieceRemoved(this);

                    canvas.Children.Remove(this);
                }


            }

            SelectedPiece = null;


            base.OnMouseUp(e);
        }


        public void TakePiece(DraggableObject victim)
        {
            System.Media.SoundPlayer player = new System.Media.SoundPlayer(Properties.Resources.take);
            player.Play();

            Square victimSquare = victim.square;
            victimSquare.Children.Clear();
            this.square.Children.Remove(this);
            victimSquare.Children.Add(this);
            LastSquare = victimSquare;
            
        }



        public bool copieable { get; set; } = false;

        public Canvas canvas { get; set; } = null;

        private ChessBoard _chessBoard = null;
        public ChessBoard chessBoard
        {
            get 
            {
                if (_chessBoard == null)
                {
                    FrameworkElement element = new FrameworkElement();

                    element = this.Parent as FrameworkElement;

                    if (element != null)
                    {
                        while (true)
                        {
                            if (element.GetType() == typeof(ChessBoard))
                            {
                                _chessBoard = element as ChessBoard;
                                return element as ChessBoard;
                            }
                            else
                            {
                                if (element.Parent != null)
                                    element = element.Parent as FrameworkElement;
                                else
                                    return null;
                            }
                        }
                    }
                    else
                        return null;
                }
                else return _chessBoard;


            }
        }



        public Square square
        {
            get
            {
                if (this.Parent?.GetType() == typeof(Square))
                    return this.Parent as Square;
                else
                    return null;
            }
        }


        public bool IsInSquare
        {
            get
            {
                if (this.Parent?.GetType() == typeof(Square))
                    return true;
                else
                    return false;
            }
        }


        public Canvas GetCanvas()
        {
            FrameworkElement element = new FrameworkElement();

            element = this.Parent as FrameworkElement;

            if (element != null)
            {
                while (true)
                {
                    if (element.GetType() == typeof(Canvas))
                        return element as Canvas;
                    else
                    {
                        if (element.Parent != null)
                            element = element.Parent as FrameworkElement;
                        else
                            return null;
                    }
                }
            }
            else
                return null;
        }


    }

}
