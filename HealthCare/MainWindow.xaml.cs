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

namespace HealthCare
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            this.Left = 400;
            this.Top = 150;
            InitializeComponent();

        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            RegisterWindow window = new RegisterWindow();
            this.Close();
            window.Show();
        }

        private void LogIn_Click(object sender, RoutedEventArgs e)
        {
            
            LogInWindowxaml window = new LogInWindowxaml();
            this.Close();
            window.Show();
        }
    }
}
