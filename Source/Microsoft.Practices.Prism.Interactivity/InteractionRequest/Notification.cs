using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Microsoft.Practices.Prism.Interactivity.InteractionRequest
{
    public interface INotification
    {
        string Title { get; set; }
        object Content { get; set; }
    }

    public class Notification : INotification
    {

        public string Title { get; set; }

        public object Content { get; set; }
    }

    public interface IConfirmation : INotification
    {
        bool Confirmed { get; set; }
    }

    public class Confirmation : IConfirmation
    {

        public bool Confirmed { get; set; }

        public string Title { get; set; }

        public object Content { get; set; }
    }
}
