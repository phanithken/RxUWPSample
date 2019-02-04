using Pure;
using RxUWPSample.Classes.Presentation.Scence.Home.View;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RxUWPSample.Classes.Presentation.Scence.Main.Router {
    public class MainRouter: ITargetType {

        public struct Dependency: IDepedencyType {
            public Frame frame;
            public Type HomePageType;
            public IFactory<HomePageParams> HomePageFactory;
        }

        public struct Payload: IPayloadType {

        }

        private Dependency dependency;
        private Payload payload;

        public void Inject(IDepedencyType dependency, IPayloadType payload) {
            this.dependency = (Dependency)dependency;
            this.payload = (Payload)payload;
        }

        public void NavigateToHome() {
            Debug.WriteLine("MainRouter NavigateToHome");
            Debug.WriteLine(dependency.frame);
            dependency.frame.Navigate(dependency.HomePageType, dependency.HomePageFactory.Create(new HomePage.Payload()));
        }
    }
}
