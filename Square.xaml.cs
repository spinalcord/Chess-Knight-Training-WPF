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



public struct Coordinate
{
    public int x, y;

    public Coordinate(int _x, int _y)
    {
        x = _x;
        y = _y;
    }
}

namespace KnightTraining
{
    /// <summary>
    /// Interaktionslogik für Square.xaml
    /// </summary>
    public partial class Square : DockPanel
    {
        public Square()
        {
            InitializeComponent();
        }

        public bool DisableDrop { get; set; } = false;

        public Coordinate coordinat = new Coordinate(0,0);

        public SolidColorBrush InitialColor;

        //public bool hasPiece = false;

        public DraggableObject GetPiece
        {
            get
            {
                return this.FindVisualChilds<DraggableObject>().FirstOrDefault();
            }
        }

    }
}
