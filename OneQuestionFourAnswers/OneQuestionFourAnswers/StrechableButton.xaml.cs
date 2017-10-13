﻿using System.ComponentModel;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

// ReSharper disable once PossibleNullReferenceException

namespace OneQuestionFourAnswers
{
    public partial class StrechableButton : INotifyPropertyChanged
    {
        public enum StateType
        {
            Active,
            Inactive,
            Chosen,
            True,
            Wrong
        }

        public static DependencyProperty StateProperty =
            DependencyProperty.Register("State", typeof(StateType), typeof(StrechableButton), new UIPropertyMetadata(StateType.Active, new PropertyChangedCallback(Refresh)));

        public StateType State
        {
            get
            {
                if (GetValue(StateProperty) == null)
                {
                    return StateType.Active;
                }
                return (StateType)GetValue(StateProperty);
            }
            set{ /*SetValue(StateProperty, value);*/ }
        }

        static public void Refresh(DependencyObject property, DependencyPropertyChangedEventArgs args)
        {
            StrechableButton strechableButton = (StrechableButton)property;
            strechableButton.State = (StateType)args.NewValue;
            strechableButton.DoPropertyChanged("State");
            strechableButton.DoPropertyChanged("BackgroundBrush");
            strechableButton.DoPropertyChanged("Foregraund");
            strechableButton.DoPropertyChanged("ButtonIsEnable");
            strechableButton.DoPropertyChanged("IsEnabled");
        }

        public bool ButtonIsEnable => State == StateType.Active;

        public event RoutedEventHandler Click;

        public StrechableButton()
        {
            InitializeComponent();
            DataContext = this;
            Loaded += (sender, args) =>
            {
                Button.Click += (obj, e) => Click?.Invoke(this, e);
            };   
        }

        public static DependencyProperty ControlCommandProperty =
            DependencyProperty.Register("ControlCommand", typeof(ICommand), typeof(StrechableButton));

        public ICommand ControlCommand
        {
            get { return (ICommand)GetValue(ControlCommandProperty); }
            set
            {
                SetValue(ControlCommandProperty, value);
                DoPropertyChanged("ControlCommand");
            }
        }

        public string BackgroundBrush
        {
            get
            {
                string path = @"VisualResources\Images\ButtonsDisable.png";
                switch (State)
                {
                    case StateType.Inactive:
                        path = @"VisualResources\Images\ButtonsDisable.png";
                        break;
                    case StateType.Chosen:
                        path = @"VisualResources\Images\ButtonsChosen.png";
                        break;
                    case StateType.True:
                        path = @"VisualResources\Images\ButtonsTrue.png";
                        break;
                    case StateType.Wrong:
                        path = @"VisualResources\Images\ButtonsFalse.png";
                        break;
                }
                return path;
            }
        }

        public Brush Foregraund
        {
            get
            {
                Brush colour = Brushes.Gray;
                switch (State)
                {
                    case StateType.Inactive:
                        colour = Brushes.Gray;
                        break;
                    case StateType.Chosen:
                        colour = Brushes.OrangeRed;
                        break;
                    case StateType.True:
                        colour = Brushes.ForestGreen;
                        break;
                    case StateType.Wrong:
                        colour = Brushes.Red;
                        break;
                }
                return colour;
            }
        }

        public static DependencyProperty ButtonTextProperty =
            DependencyProperty.Register("ButtonText", typeof(string), typeof(StrechableButton));

        public string ButtonText
        {
            get
            {
                return (string)GetValue(ButtonTextProperty);
            }

            set
            {
                SetValue(ButtonTextProperty, value);
                DoPropertyChanged("ButtonText");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void DoPropertyChanged(string name)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
