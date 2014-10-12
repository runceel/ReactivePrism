using Codeplex.Reactive.Interactivity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace ReactivePrism
{
    /// <summary>
    /// Flyout MVVM support behavior.
    /// </summary>
    public class FlyoutIsOpenBehavior : Behavior<FrameworkElement>
    {
        private bool isProcessing;

        /// <summary> 
        /// Target Flyout. 
        /// </summary> 
        public Flyout Flyout
        {
            get { return (Flyout)GetValue(FlyoutProperty); }
            set { SetValue(FlyoutProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Flyout.  This enables animation, styling, binding, etc... 
        public static readonly DependencyProperty FlyoutProperty =
            DependencyProperty.Register("Flyout", typeof(Flyout), typeof(FlyoutIsOpenBehavior), new PropertyMetadata(null));

        /// <summary> 
        /// Flyout's parent 
        /// </summary> 
        public FrameworkElement Parent
        {
            get { return (FrameworkElement)GetValue(ParentProperty); }
            set { SetValue(ParentProperty, value); }
        }

        // Using a DependencyProperty as the backing store for Parent.  This enables animation, styling, binding, etc... 
        public static readonly DependencyProperty ParentProperty =
            DependencyProperty.Register("Parent", typeof(FrameworkElement), typeof(FlyoutIsOpenBehavior), new PropertyMetadata(null));

        /// <summary> 
        /// Flyout open status. 
        /// </summary> 
        public bool IsOpen
        {
            get { return (bool)GetValue(IsOpenProperty); }
            set { SetValue(IsOpenProperty, value); }
        }

        // Using a DependencyProperty as the backing store for IsOpen.  This enables animation, styling, binding, etc... 
        public static readonly DependencyProperty IsOpenProperty =
            DependencyProperty.Register("IsOpen", typeof(bool), typeof(FlyoutIsOpenBehavior), new PropertyMetadata(false, IsOpenChanged));

        /// <summary> 
        /// Flyout opened command.
        /// </summary> 
        public ICommand OpenedCommand
        {
            get { return (ICommand)GetValue(OpenedCommandProperty); }
            set { SetValue(OpenedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for OpenedCommand.  This enables animation, styling, binding, etc... 
        public static readonly DependencyProperty OpenedCommandProperty =
            DependencyProperty.Register("OpenedCommand", typeof(ICommand), typeof(FlyoutIsOpenBehavior), new PropertyMetadata(null));

        /// <summary> 
        /// Flyout closed command. 
        /// </summary> 
        public ICommand ClosedCommand
        {
            get { return (ICommand)GetValue(ClosedCommandProperty); }
            set { SetValue(ClosedCommandProperty, value); }
        }

        // Using a DependencyProperty as the backing store for ClosedCommand.  This enables animation, styling, binding, etc... 
        public static readonly DependencyProperty ClosedCommandProperty =
            DependencyProperty.Register("ClosedCommand", typeof(ICommand), typeof(FlyoutIsOpenBehavior), new PropertyMetadata(null));

        private static void IsOpenChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            var self = (FlyoutIsOpenBehavior)d;
            if (self.isProcessing) { return; }
            if ((bool)e.NewValue)
            {
                self.Flyout.ShowAt(self.Parent);
            }
            else
            {
                self.Flyout.Hide();
            }
        }

        protected override void OnAttached()
        {
            this.Flyout.Opened += this.Flyout_Opened;
            this.Flyout.Closed += this.Flyout_Closed;
        }

        private void Flyout_Closed(object sender, object e)
        {
            this.isProcessing = true;
            this.IsOpen = false;
            if (this.ClosedCommand != null) { this.ClosedCommand.Execute(null); }
            this.isProcessing = false;
        }

        private void Flyout_Opened(object sender, object e)
        {
            this.isProcessing = true;
            this.IsOpen = true;
            if (this.OpenedCommand != null) { this.OpenedCommand.Execute(null); }
            this.isProcessing = false;
        }

        protected override void OnDetaching()
        {
            this.Flyout.Opened -= this.Flyout_Opened;
            this.Flyout.Closed -= this.Flyout_Closed;
        }
    }
}
