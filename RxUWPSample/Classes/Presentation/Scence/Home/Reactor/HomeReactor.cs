using dotnetReactorkit;
using Pure;
using RxUWPSample.Classes.Presentation.Scence.Home.Router;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reactive.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RxUWPSample.Classes.Presentation.Scence.Home.Reactor {

    public class ActionStruct {
        public enum Action {
            Increment,
            Decrement,
            didMessage
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
        Increment,
        Decrement,
        setMessage
    }

    public struct State {
        public int Counter;
        public string Message;
    }

    public class Mutation: ReactorMutation {
        internal MutationType CurrentType;
        internal object Params;

        internal Mutation(MutationType type, object param = null) {
            this.CurrentType = type;
            this.Params = param;
        }
    }

    public class HomeReactor: Reactor<ActionStruct, State>, IReactable<ActionStruct, State> {

        private HomeRouter _router;

        private static State initialState = new State() {
            Counter = 0,
            Message = ""
        };

        public HomeReactor(IFactory<HomeRouter> factory): base(initialState: initialState) {
            this._router = factory.Create(new HomeRouter.Payload());
        }

        protected override IObservable<ReactorMutation> Mutate(ActionStruct action) {
            switch(action.ActionType) {
                case ActionStruct.Action.Increment:
                    return Observable.Return(new Mutation(type: MutationType.Increment));
                case ActionStruct.Action.Decrement:
                    return Observable.Return(new Mutation(type: MutationType.Decrement));
                case ActionStruct.Action.didMessage:
                    var d = action.Params.ToString() as string;
                    return Observable.Return(new Mutation(type: MutationType.setMessage, param: d));
                default: return Observable.Empty<Mutation>();
            }
        }

        protected override State Reduce(State state, ReactorMutation mutation) {
            var m = mutation as Mutation;
            var newState = state;
            switch(m.CurrentType) {
                case MutationType.Increment:
                    newState.Counter++;
                    break;
                case MutationType.Decrement:
                    newState.Counter--;
                    break;
                case MutationType.setMessage:
                    newState.Message = (string)m.Params;
                    break;
                default: break;
            }
            return newState;
        }
    }
}
