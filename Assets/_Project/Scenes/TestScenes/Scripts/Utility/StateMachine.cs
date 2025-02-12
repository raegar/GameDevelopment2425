using System;
using System.Collections.Generic;

public class StateMachine
{
    StateNode current;
    Dictionary<Type, StateNode> nodes = new();
    HashSet<ITransition> anyTransisiton = new();

    class StateNode
        {
        public IState State { get; }
        public HashSet<Transition> Transitions { get; }

        public StateNode(IState state)
        {
            State = state;
            Transitions = new HashSet<Transition>();
        }

        public void AddTransition(IState to, IPredicate condition)
        {
            Transitions.Add(new Transition(to,condition));
        }

    }
}
