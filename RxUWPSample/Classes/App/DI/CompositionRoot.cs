using Pure;
using RxUWPSample.Classes.Presentation.Scence.Home.Reactor;
using RxUWPSample.Classes.Presentation.Scence.Home.Router;
using RxUWPSample.Classes.Presentation.Scence.Home.View;
using RxUWPSample.Classes.Presentation.Scence.Main.Reactor;
using RxUWPSample.Classes.Presentation.Scence.Main.Router;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace RxUWPSample.Classes.App.DI {
    public class CompositionRoot {

        public struct Depedency {

        }

        public struct Payload {

        }

        private Depedency depedency;
        private Payload payload;

        public CompositionRoot(Depedency depedency, Payload payload) {
            this.depedency = depedency;
            this.payload = payload;
        }

        // reslove method
        public static CompositionRoot Resolve() {
            return new CompositionRoot(
                    depedency: new Depedency(),
                    payload: new Payload()
                );
        }

        // MainPage factory
        public IFactory<MainPageParams> MainPageFactory() {
            Func<IDepedencyType> dependency = (() => {
                return new MainPage.Dependency {
                    _reactor = new MainReactor(
                            factory: MainRouterFactory()
                            )
                };
            });
            return new Factory<MainPageParams>(dependency: dependency);
        }

        // MainRouter factory
        public IFactory<MainRouter> MainRouterFactory() {
            Func<IDepedencyType> dependency = (() => {
                return new MainRouter.Dependency {
                    frame = Window.Current.Content as Frame,
                    HomePageType = typeof(HomePage),
                    HomePageFactory = HomePageFactory()
                };
            });
            return new Factory<MainRouter>(dependency: dependency);
        }

        // HomePage factory
        public IFactory<HomePageParams> HomePageFactory() {
            Func<IDepedencyType> dependency = (() => {
            return new HomePage.Dependency {
                _reactor = new HomeReactor(
                        factory: HomeRouterFactory()
                        )
                };
            });
            return new Factory<HomePageParams>(dependency: dependency);
        }

        // HomeRouter factory
        public IFactory<HomeRouter> HomeRouterFactory() {
            Func<IDepedencyType> dependency = (() => {
                return new HomeRouter.Dependency {

                };
            });
            return new Factory<HomeRouter>(dependency: dependency);
        }
    }
}
