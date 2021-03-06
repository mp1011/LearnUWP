﻿using FinancialDucks.Models;
using LearnUWP.Services;
using LearnUWP.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at https://go.microsoft.com/fwlink/?LinkId=234236

namespace LearnUWP.Views
{
    public sealed partial class EnumPicker : UserControl
    {
        public EnumPickerViewModel ViewModel { get; }

        public EnumPicker()
        {
            ViewModel = new EnumPickerViewModel();
            InitializeComponent();
            DataContextChanged += EnumPicker_DataContextChanged;
            ViewModel.PropertyChanged += ViewModel_PropertyChanged;
            Picker.SelectionChanged += Picker_SelectionChanged;
        }

        private void Picker_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            this.DebugWrite($"set picker value to {Picker.SelectedValue}");
        }

        private void ViewModel_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            if (ViewModel.SelectedValue != null && e.PropertyName == nameof(ViewModel.SelectedValue))
                DataContext = ViewModel.SelectedValue;
        }

        private void EnumPicker_DataContextChanged(FrameworkElement sender, DataContextChangedEventArgs args)
        {
            if (DataContext != null && DataContext.GetType().IsEnum)
            {
                ViewModel.SelectedValue = DataContext;
                ViewModel.SetChoices(DataContext);
                this.DebugWrite($"set choices based on {DataContext.DescribeWithType()} : {ViewModel.Choices.Describe()} ");
            }
        }
    }
}
