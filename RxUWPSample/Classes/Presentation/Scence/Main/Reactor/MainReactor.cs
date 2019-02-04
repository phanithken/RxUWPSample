using dotnetReactorkit;
using Pure;
using RxUWPSample.Classes.Presentation.Scence.Main.Router;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxUWPSample.Classes.Presentation.Scence.Main.Reactor {

    public class ActionStruct {
        public enum Action {
            didLoad
        }

        public Action ActionType;
        public dynamic Params;

        public ActionStruct(Action action, dynamic param = null) {
            this.ActionType = action;
            this.Params = param;
        }

        public static ActionStruct Dispatcher(Action action, dynamic param = null) {
            return new ActionStruct(action, param);
        }
    }

    internal enum MutationType {

    }

    public struct State {

    }

    public class Mutation: ReactorMutation {
        internal MutationType CurrentType;
        internal object Params;

        internal Mutation(MutationType type, object param = null) {
            this.CurrentType = type;
            this.Params = param;
        }
    }

    public class MainReactor: Reactor<ActionStruct, State>, IReactable<ActionStruct, State> {

        private MainRouter _router;

        private static State initialState = new State() {

        };

        public MainReactor(IFactory<MainRouter> factory) : base(initialState: initialState) {
            this._router = factory.Create(new MainRouter.Payload());
        }

        public struct Dependency {

        }

        public struct Payload {

        }

        protected override IObservable<ReactorMutation> Mutate(ActionStruct action) {
            switch(action.ActionType) {
                case ActionStruct.Action.didLoad:
                    Debug.WriteLine("MainPage Reactor DidLoad");
                    this._router.NavigateToHome();
                    return Observable.Empty<Mutation>();
                default: return Observable.Empty<Mutation>();
            }
        }

        protected override State Reduce(State state, ReactorMutation mutation) {
            return base.Reduce(state, mutation);
        }

    }
}
