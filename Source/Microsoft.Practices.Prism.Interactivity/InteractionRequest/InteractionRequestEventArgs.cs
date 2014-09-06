using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Practices.Prism.Interactivity.InteractionRequest
{
    public class InteractionRequestEventArgs : EventArgs
    {
        public INotification Context { get; private set; }
        public Action Callback { get; private set; }

        public InteractionRequestEventArgs(INotification context, Action callback)
        {
            this.Context = context;
            this.Callback = callback;
        }
    }
}
