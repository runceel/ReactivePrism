using Codeplex.Reactive.Interactivity;
using Microsoft.Practices.Prism.Interactivity.InteractionRequest;
using Windows.UI.Xaml;

namespace ReactivePrism
{
    public class InteractionRequestTrigger : TriggerBase<DependencyObject>
    {
        public static readonly DependencyProperty SourceObjectProperty =
            DependencyProperty.Register(
                "SourceObject",
                typeof(IInteractionRequest),
                typeof(InteractionRequestTrigger),
                new PropertyMetadata(null));

        public IInteractionRequest SourceObject
        {
            get { return (IInteractionRequest)GetValue(SourceObjectProperty); }
            set { SetValue(SourceObjectProperty, value); }
        }

        protected override void OnAttached()
        {
            base.OnAttached();
            if (this.SourceObject != null)
            {
                this.SourceObject.Raised += this.SourceObject_Raised;
            }
        }

        protected override void OnDetaching()
        {
            if (this.SourceObject != null)
            {
                this.SourceObject.Raised -= this.SourceObject_Raised;
            }
            base.OnDetaching();
        }

        private void SourceObject_Raised(object sender, InteractionRequestEventArgs e)
        {
            this.InvokeActions(e);
        }

    }
}
