using Pure;
using RxUWPSample.Classes.Presentation.Scence.Main.Reactor;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace RxUWPSample
{

    public struct MainPageParams : ITargetType {

        public MainPage.Dependency Dependency { get; set; }
        public MainPage.Payload Payload { get; set; }

        public void Inject(IDepedencyType dependency, IPayloadType payload) {
            this.Dependency = (MainPage.Dependency)dependency;
            this.Payload = (MainPage.Payload)payload;
        }
    }


    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {

        public struct Dependency: IDepedencyType {
            public MainReactor _reactor;
        }

        public struct Payload: IPayloadType {

        }

        private MainPageParams _param;


        public MainPage()
        {
            this.InitializeComponent();
            //this.Loaded += this.PageLoaded;

            var button = new Button();
            button.Width = 100;
            button.Height = 44;
            button.Background = new SolidColorBrush(Colors.Black);
            button.Foreground = new SolidColorBrush(Colors.White);
            button.Click += this.OnClickListener;

            this.Root.Children.Add(button);

            this.Loaded += this.PageLoaded;
        }

        void PageLoaded(object sender, RoutedEventArgs e) {
            Debug.WriteLine("PageLoaded Main");
            this._param.Dependency._reactor.action.OnNext(ActionStruct.Dispatcher(ActionStruct.Action.didLoad));
        }

        void OnClickListener (object sender, RoutedEventArgs e) {
            this._param.Dependency._reactor.action.OnNext(ActionStruct.Dispatcher(ActionStruct.Action.didLoad));
        }

        protected override void OnNavigatedTo(NavigationEventArgs e) {
            base.OnNavigatedTo(e);
            Debug.WriteLine("OnNavigateTo Main");
            MainPageParams param = (MainPageParams)e.Parameter;
            this._param = param;
            
        }
    }
}
