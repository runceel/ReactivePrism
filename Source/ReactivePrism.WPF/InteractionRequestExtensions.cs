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
    /// <summary>
    /// InteractionRequest extensions.
    /// </summary>
    public static class InteractionRequestExtensions
    {
        /// <summary>
        /// Convert Raise event into IObservable.
        /// </summary>
        /// <typeparam name="T">INotification object</typeparam>
        /// <param name="self">InteractinRequest</param>
        /// <param name="context">INotification object</param>
        /// <returns>IO</returns>
        public static IObservable<T> RaiseAsObservable<T>(this InteractionRequest<T> self, T context)
            where T : INotification
        {
            return Observable.Create<T>(o =>
            {
                self.Raise(context, c => { o.OnNext(c); o.OnCompleted(); });
                return Disposable.Empty;
            });
        }

        /// <summary>
        /// Convert Raise event into Task.
        /// </summary>
        /// <typeparam name="T">INotification object</typeparam>
        /// <param name="self">InteractinRequest</param>
        /// <param name="context">INotification object</param>
        /// <returns>Task</returns>
        public static Task<T> RaiseAsync<T>(this InteractionRequest<T> self, T context)
            where T : INotification
        {
            return self.RaiseAsObservable(context).ToTask();
        }
    }
}
