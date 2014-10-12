using Codeplex.Reactive;
using Microsoft.Practices.Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;

namespace FlyoutSampleApp.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        public ReactiveProperty<bool> IsOpen { get; private set; }

        public ReactiveProperty<string> FlyoutState { get; private set; }

        public ReactiveCommand OpenedCommand { get; private set; }

        public ReactiveCommand ClosedCommand { get; private set; }

        public MainPageViewModel()
        {
            this.IsOpen = new ReactiveProperty<bool>();

            this.FlyoutState = new ReactiveProperty<string>();

            this.OpenedCommand = new ReactiveCommand();
            this.OpenedCommand.Subscribe(_ =>
                {
                    this.FlyoutState.Value = "Opened";
                });

            this.ClosedCommand = new ReactiveCommand();
            this.ClosedCommand.Subscribe(_ =>
                {
                    this.FlyoutState.Value = "Closed";
                });
        }
    }
}
