using Codeplex.Reactive.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Markup;

namespace ReactivePrism
{
    /// <summary> 
    /// SettingsFlyout MVVM support behavior. 
    /// </summary> 
    [ContentProperty(Name = "SettingsFlyout")]
    public class SettingsFlyoutIsOpenBehavior : Behavior<FrameworkElement>
    {
        private bool isProcessing;

        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register(
                "IsOpen",
                typeof(bool),
                typeof(SettingsFlyoutIsOpenBehavior),
                new PropertyMetadata(false, IsOpenChanged));

        private static void IsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (SettingsFlyoutIsOpenBehavior)d;
            if (self.isProcessing) { return; }

            if ((bool)e.NewValue)
            {
                if (self.IsIndependent)
                {
                    self.SettingsFlyout.ShowIndependent();
                }
                else
                {
                    self.SettingsFlyout.Show();
                }
            }
            else
            {
                self.SettingsFlyout.Hide();
            }
        }

        /// <summary> 
        /// get and set is open SettingsFlyout.  
        /// </summary> 
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }


        public static readonly DependencyProperty SettingsFlyoutProperty =
            DependencyProperty.Register(
                "SettingsFlyout",
                typeof(SettingsFlyout),
                typeof(SettingsFlyoutIsOpenBehavior),
                new PropertyMetadata(null));

        /// <summary> 
        /// get and set target SettingsFlyout
        /// </summary> 
        public SettingsFlyout SettingsFlyout
        {
            get { return (SettingsFlyout)GetValue(SettingsFlyoutProperty); }
            set { SetValue(SettingsFlyoutProperty, value); }
        }

        public static readonly DependencyProperty IsIndependedProperty =
            DependencyProperty.Register(
                "IsIndependent",
                typeof(bool),
                typeof(SettingsFlyoutIsOpenBehavior),
                new PropertyMetadata(false));

        /// <summary> 
        /// get and set use ShowIndependent method 
        /// </summary> 
        public bool IsIndependent
        {
            get { return (bool)GetValue(IsIndependedProperty); }
            set { SetValue(IsIndependedProperty, value); }
        }


        public static readonly DependencyProperty OpenedCommandProperty =
            DependencyProperty.Register(
                "OpenedCommand",
                typeof(ICommand),
                typeof(SettingsFlyoutIsOpenBehavior),
                new PropertyMetadata(null));

        /// <summary> 
        /// get and set executed command when SettingsFlyout opened. 
        /// </summary> 
        public ICommand OpenedCommand
        {
            get { return (ICommand)GetValue(OpenedCommandProperty); }
            set { SetValue(OpenedCommandProperty, value); }
        }

        public static readonly DependencyProperty ClosedCommandProperty =
            DependencyProperty.Register(
                "ClosedCommand",
                typeof(ICommand),
                typeof(SettingsFlyoutIsOpenBehavior),
                new PropertyMetadata(null));

        /// <summary> 
        /// get and set executed command when SettingsFlyout closed. 
        /// </summary> 
        public ICommand ClosedCommand
        {
            get { return (ICommand)GetValue(ClosedCommandProperty); }
            set { SetValue(ClosedCommandProperty, value); }
        }


        protected override void OnAttached()
        {
            this.SettingsFlyout.Loaded += this.SettingsFlyoutLoaded;
            this.SettingsFlyout.Unloaded += this.SettingsFlyoutUnloaded;
        }


        protected override void OnDetaching()
        {
            this.SettingsFlyout.Loaded -= this.SettingsFlyoutLoaded;
            this.SettingsFlyout.Unloaded -= this.SettingsFlyoutUnloaded;
        }

        private void SettingsFlyoutLoaded(object sender, RoutedEventArgs e)
        {
            this.isProcessing = true;
            this.IsOpen = true;
            if (this.OpenedCommand != null) { this.OpenedCommand.Execute(null); }
            this.isProcessing = false;
        }

        private void SettingsFlyoutUnloaded(object sender, RoutedEventArgs e)
        {
            this.isProcessing = true;
            this.IsOpen = false;
            if (this.ClosedCommand != null) { this.ClosedCommand.Execute(null); }
            this.isProcessing = false;
        }
    }
}
