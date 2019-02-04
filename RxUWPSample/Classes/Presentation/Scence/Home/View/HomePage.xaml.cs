using Pure;
using RxUWP.Disposable;
using RxUWP.Observable.Extensions;
using RxUWP.UI.Extensions;
using RxUWPSample.Classes.Presentation.Scence.Home.Reactor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reactive.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MainAction = RxUWPSample.Classes.Presentation.Scence.Home.Reactor.ActionStruct;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace RxUWPSample.Classes.Presentation.Scence.Home.View {

    public struct HomePageParams : ITargetType {

        public HomePage.Dependency Dependency { get; set; }
        public HomePage.Payload Payload { get; set; }

        public void Inject(IDepedencyType dependency, IPayloadType payload) {
            this.Dependency = (HomePage.Dependency)dependency;
            this.Payload = (HomePage.Payload)payload;
        }
    }

    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page {

        private DisposeBag disposeBag = new DisposeBag();

        public struct Dependency : IDepedencyType {
            public HomeReactor _reactor;
        }

        public struct Payload : IPayloadType {

        }

        private HomePageParams _param;

        public HomePage() {
            this.InitializeComponent();
            Debug.WriteLine("HomePage");
        }

        void PageLoaded(object sender, RoutedEventArgs e) {
            
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            HomePageParams param = (HomePageParams)e.Parameter;
            this._param = param;

            Increment.rx_Tap()
                .Select(x => new MainAction(action: MainAction.Action.Increment))
                .Bind(to: this._param.Dependency._reactor.action)
                .DisposeBag(bag: this.disposeBag);

            Decrement.rx_Tap()
                .Select(x => new MainAction(action: MainAction.Action.Decrement))
                .Bind(to: this._param.Dependency._reactor.action)
                .DisposeBag(bag: this.disposeBag);

            InputText.rx_TextChanged()
                .Select(x => MainAction.Dispatcher(MainAction.Action.didMessage, x))
                .Bind(to: this._param.Dependency._reactor.action)
                .DisposeBag(bag: this.disposeBag);

            this._param.Dependency._reactor.state
                .Select(state => state.Counter.ToString())
                .Bind(to: Indicator.rx_Text())
                .DisposeBag(bag: this.disposeBag);

   
            this._param.Dependency._reactor.state
                .Select(state => state.Message)
                .Bind(to: MessageIndicator.rx_Text())
                .DisposeBag(bag: this.disposeBag);
               
        }
    }
}
