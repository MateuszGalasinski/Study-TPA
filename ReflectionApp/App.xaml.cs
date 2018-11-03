﻿using ReflectionApp.Views;
using System.Windows;

namespace ReflectionApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);

            Window window = new MainWindow();

            window.DataContext = Bootstraper.MainViewModel;

            window.Show();
        }
    }
}
