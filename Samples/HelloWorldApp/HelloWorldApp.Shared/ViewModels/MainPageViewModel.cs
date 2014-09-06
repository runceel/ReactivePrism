using Codeplex.Reactive;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Practices.Prism.StoreApps;
using System;
using System.Collections.Generic;
using System.Text;
using System.Reactive.Linq;
using ReactivePrism;

namespace HelloWorldApp.ViewModels
{
    public class MainPageViewModel : ViewModel
    {
        public ReactiveProperty<string> Input { get; private set; }

        public ReactiveCommand AlertCommand { get; private set; }

        public ReactiveProperty<string> Output { get; private set; }

        public InteractionRequest<IConfirmation> ConfirmRequest { get; private set; }

        public MainPageViewModel()
        {
            this.ConfirmRequest = new InteractionRequest<IConfirmation>();
            this.Input = new ReactiveProperty<string>("");
            this.Output = new ReactiveProperty<string>();

            this.AlertCommand = new ReactiveCommand();
            this.AlertCommand
                .SelectMany(_ => this.ConfirmRequest.RaiseAsObservable(new Confirmation
                {
                    Title = "Confirm",
                    Content = "Convert OK?"
                }))
                .Where(c => c.Confirmed)
                .Select(_ => this.Input.Value)
                .Select(s => s.ToUpper())
                .Subscribe(s => this.Output.Value = s);
        }
    }
}
