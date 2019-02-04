using Pure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace RxUWPSample.Classes.Presentation.Scence.Home.Router {
    public class HomeRouter: ITargetType {

        public struct Dependency: IDepedencyType {
            
        }

        public struct Payload: IPayloadType {

        }

        private Dependency dependency;
        private Payload payload;

        public void Inject(IDepedencyType dependency, IPayloadType payload) {
            this.dependency = (Dependency)dependency;
            this.payload = (Payload)payload;
        }
    }
}
