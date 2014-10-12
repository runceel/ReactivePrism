using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Unity;
using PrismAdapter;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.ApplicationModel;
using Windows.ApplicationModel.Activation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

// 空のアプリケーション テンプレートについては、http://go.microsoft.com/fwlink/?LinkId=234227 を参照してください

namespace FlyoutSampleApp
{
    /// <summary>
    /// 既定の Application クラスに対してアプリケーション独自の動作を実装します。
    /// </summary>
    public sealed partial class App : Application
    {
        public App()
        {
            this.InitializeComponent();
        }

        protected override void OnLaunched(LaunchActivatedEventArgs e)
        {
#if DEBUG
            if (System.Diagnostics.Debugger.IsAttached)
            {
                this.DebugSettings.EnableFrameRateCounter = true;
            }
#endif
            PrismAdapterBootstrapper.Initialize(b =>
            {
                var c = new UnityContainer();
                c.RegisterInstance<IServiceLocator>(new UnityServiceLocator(c));
                c.RegisterInstance(b.NavigationService.Value);
                c.RegisterInstance(b.SessionStateService.Value);
                c.RegisterInstance(b.FrameFacade.Value);
                b.Resolve = t => c.Resolve(t);

                b.Run(n => n.Navigate("Main", e.Arguments));
                return Task.FromResult(0);
            }, e);
        }
    }
}