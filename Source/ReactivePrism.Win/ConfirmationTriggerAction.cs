using Codeplex.Reactive.Interactivity;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace ReactivePrism
{
    public class ConfirmationTriggerAction : TriggerAction<DependencyObject>
    {
        protected override async void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestEventArgs;
            if (args == null)
            {
                throw new ArgumentException("parameter: " + parameter);
            }
            var confirmation = (IConfirmation)args.Context;
            var m = new MessageDialog(confirmation.Content.ToString(), confirmation.Title ?? "");
            m.Commands.Add(new UICommand { Label = "OK", Id = "OK" });
            m.Commands.Add(new UICommand { Label = "Cancel", Id = "Cancel" });
            var result = await m.ShowAsync();
            confirmation.Confirmed = result.Id.ToString() == "OK";
            args.Callback();
        }
    }
}
