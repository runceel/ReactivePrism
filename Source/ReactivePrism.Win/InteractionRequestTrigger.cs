using Codeplex.Reactive.Interactivity;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Microsoft.Xaml.Interactivity;
using Windows.UI.Xaml;

namespace ReactivePrism
{
    /// <summary>
    /// InteractionRequest's Raised event trigger.
    /// </summary>
    public class InteractionRequestTrigger : TriggerBase<DependencyObject>
    {
        public static readonly DependencyProperty SourceObjectProperty =
            DependencyProperty.Register(
                "SourceObject",
                typeof(object),
                typeof(InteractionRequestTrigger),
                new PropertyMetadata(null));

        /// <summary>
        /// Binding InteractionRequest object.
        /// </summary>
        public object SourceObject
        {
            get { return (object)GetValue(SourceObjectProperty); }
            set { SetValue(SourceObjectProperty, value); }
        }

        public ActionCollection Actions
        {
            get { return this.ActionsImpl; }
        }

        protected override void OnAttached()
        {
            var r = this.SourceObject as IInteractionRequest;
            if (r != null)
            {
                r.Raised += this.SourceObject_Raised;
            }
        }

        protected override void OnDetaching()
        {
            var r = this.SourceObject as IInteractionRequest;
            if (r != null)
            {
                r.Raised -= this.SourceObject_Raised;
            }
        }

        private void SourceObject_Raised(object sender, InteractionRequestEventArgs e)
        {
            this.InvokeActions(e);
        }

    }
}
