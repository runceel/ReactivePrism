using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Practices.Prism.Interactivity.InteractionRequest
{
    public interface IInteractionRequest
    {
        event EventHandler<InteractionRequestEventArgs> Raised;
    }

    public class InteractionRequest<T> : IInteractionRequest
        where T : INotification
    {
        public event EventHandler<InteractionRequestEventArgs> Raised;
        protected virtual void OnRaised(INotification context, Action callback)
        {
            var h = this.Raised;
            if (h != null)
            {
                h(this, new InteractionRequestEventArgs(context, callback));
            }
        }

        public void Raise(T context)
        {
            this.Raise(context, _ => { });
        }

        public void Raise(T context ,Action<T> callback)
        {
            this.OnRaised(context, () => callback(context));
        }
    }

}
