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
    /// Interaktionslogik für Bishop.xaml
    /// </summary>
    public partial class Bishop : DraggableObject
    {
        public Bishop()
        {
            InitializeComponent();
            attackingPattern.DiagonalDirectonRightUpperCorner = true;
            attackingPattern.DiagonalDirectonLeftUpperCorner = true;
            attackingPattern.DiagonalDirectonRightBottomCorner = true;
            attackingPattern.DiagonalDirectonLeftBottomCorner = true;
            figure = Figure.Bishop;
        }
    }
}
