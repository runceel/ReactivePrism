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
    /// <summary>
    /// Notification trigger action.
    /// </summary>
    public class NotificationTriggerAction : TriggerAction<DependencyObject>
    {
        protected override async void Invoke(object parameter)
        {
            var args = parameter as InteractionRequestedEventArgs;
            if (args == null)
            {
                throw new ArgumentException("parameter: " + parameter);
            }

            var m = new MessageDialog(args.Context.Content.ToString(), args.Context.Title ?? "");
            await m.ShowAsync();
            args.Callback();
        }
    }
}
