# README

ReactivePrism is Prism + ReactiveProperty + InteractionRequestExtensions class. InteractionRequestExtension class convert Raised event into IObservable.


## How to use

### Install

Install nuget.

```
Install-Package ReactivePrism -Pre
```

### InteractionRequestExtensions

```cs
public class MainWindowViewModel
{
    public ReactiveProperty<string> Input { get; private set; }

    public ReactiveCommand AlertCommand { get; private set; }

    public ReactiveProperty<string> Output { get; private set; }

    public InteractionRequest<IConfirmation> ConfirmRequest { get; private set; }

    public MainWindowViewModel()
    {
        this.ConfirmRequest = new InteractionRequest<IConfirmation>();
        this.Input = new ReactiveProperty<string>("");
        this.Output = new ReactiveProperty<string>();

        this.AlertCommand = new ReactiveCommand();
        this.AlertCommand
		    // RaiseAsObservable method integrate IObservable method chain.
            .SelectMany(_ => this.ConfirmRequest.RaiseAsObservable(new Confirmation
            {
                Title = "Confirm",
                Content = "Convert OK?"
            }))
            .Where(c => c.Confirmed)
            .Select(_ => this.Input.Value)
            .Select(s => s.ToUpper())
            .Subscribe(s => this.Output.Value = s);
    }
}
```

## ReactiveInteractionRequest

IO<INotification> -> ReactiveInteractionRequest<INotification>.
ReactiveInteractionRequest<INotification> is IObservable<INotification>.

```cs
this.AlertCommand2 = new ReactiveCommand();
// IO<Confirmation> -> ReactiveInteractionRequest<Confirmation>
this.ConfirmRequest2 = this.AlertCommand2
    .Select(_ => new Confirmation
    {
        Title = "Confirm",
        Content = "Convert OK?"
    })
    .ToInteractionRequest();

// ReactiveInteractionRequest<Confirmation> is IObservable<Confirmation>.
this.ConfirmRequest2
    .Where(c => c.Confirmed)
    .Select(_ => this.Input.Value)
    .Select(s => s.ToUpper())
    .Subscribe(s => this.Output.Value = s);
```
