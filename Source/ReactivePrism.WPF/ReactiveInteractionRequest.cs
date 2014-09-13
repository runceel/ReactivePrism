using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Subjects;
using System.Text;
using System.Threading.Tasks;

namespace ReactivePrism
{
    public sealed class ReactiveInteractionRequest<T> : IInteractionRequest, IObservable<T>, IDisposable
        where T : INotification
    {
        private IDisposable disposable;
        private Subject<T> source = new Subject<T>();

        public event EventHandler<InteractionRequestedEventArgs> Raised;

        public ReactiveInteractionRequest(IObservable<T> source = null)
        {
            if (source == null)
            {
                throw new ArgumentNullException();
            }

            this.disposable = source.Subscribe(context => this.OnRaised(context));
        }

        private void OnRaised(T context)
        {
            var h = this.Raised;
            if (h != null)
            {
                h(this, new InteractionRequestedEventArgs(context, () => this.source.OnNext(context)));
            }
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            return source.Subscribe(observer);
        }

        public void Dispose()
        {
            if (this.disposable == null)
            {
                return;
            }

            this.disposable.Dispose();
            this.disposable = null;
        }
    }

    public static class ReactiveInteractionRequest
    {
        public static ReactiveInteractionRequest<T> ToInteractionRequest<T>(this IObservable<T> self)
            where T : INotification
        {
            return new ReactiveInteractionRequest<T>(self);
        }
    }
}
