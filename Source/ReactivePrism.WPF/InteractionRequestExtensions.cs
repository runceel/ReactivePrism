using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Disposables;
using System.Reactive.Linq;
using System.Reactive.Threading.Tasks;
using System.Text;
using System.Threading.Tasks;


namespace ReactivePrism
{
    public static class InteractionRequestExtensions
    {
        public static IObservable<T> RaiseAsObservable<T>(this InteractionRequest<T> self, T context)
            where T : INotification
        {
            return Observable.Create<T>(o =>
            {
                self.Raise(context, c => { o.OnNext(c); o.OnCompleted(); });
                return Disposable.Empty;
            });
        }

        public static Task<T> RaiseAsync<T>(this InteractionRequest<T> self, T context)
            where T : INotification
        {
            return self.RaiseAsObservable(context).ToTask();
        }
    }
}
