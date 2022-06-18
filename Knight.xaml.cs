﻿using System;
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
    /// Interaktionslogik für Knight.xaml
    /// </summary>
    public partial class Knight : DraggableObject
    {
        public Knight()
        {
            InitializeComponent();

            figure = Figure.Knight;

            this.attackingPattern.Knight = true;
        }
    }
}
