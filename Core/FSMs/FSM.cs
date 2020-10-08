using Godot;
using System;
using System.Collections.Generic;


namespace Ellizium.Core.FSMs
{
    public class FSM
    {
        private State CurrentState;
        private State LastState;
        private List<State> States = new List<State>();

        private EGL.INode Node;

        public string CurrentStateName
        {
            get
            {
                if (CurrentState != null)
                    return CurrentState.Name;
                else
                    return "None";
            }
        }
        public string LastStateName
        {
            get
            {
                if (LastState != null)
                    return LastState.Name;
                else
                    return "None";
            }
        }

        public event Action<string> ChangeState;

        public FSM(EGL.INode node)
        {
            Node = node;
        }

        public void AddState(State state)
        {
            States.Add(state);
        }
        public void RemoveState(string state)
        {
            State removeState = States.Find(x => x.Name == state);

            if(removeState != null)
                States.Remove(removeState);
        }
        public void SwitchState(string state)
        {
            State switchesState = States.Find(x => x.Name == state);

            if (switchesState != null)
            {
                if (CurrentState != null)
                {
                    string str = CurrentState.PoolEnableStates.Find(x => x == switchesState.Name);

                    if (CurrentState.Mode == StateMode.WhiteList)
                    {
                        if (str != null)
                            _SwitchState(switchesState);
                    }
                    else
                    {
                        if (str == null)
                            _SwitchState(switchesState);
                    }
                }
                else
                {
                    CurrentState = switchesState;

                    Node.RenderProcess += CurrentState.RenderProcessAction;
                    Node.PhysicsProcess += CurrentState.PhysicsProcessAction;
                    Node.InputProcess += CurrentState.InputProcessAction;

                    CurrentState.InAction?.Invoke();
                    ChangeState?.Invoke(CurrentState.Name);
                }
            }
        }
        public void SwitchFromLastState()
        {
            _SwitchState(LastState);
        }

        private void _SwitchState(State switchesState)
        {
            Node.RenderProcess -= CurrentState.RenderProcessAction;
            Node.PhysicsProcess -= CurrentState.PhysicsProcessAction;
            Node.InputProcess -= CurrentState.InputProcessAction;

            CurrentState.OutAction?.Invoke();

            LastState = CurrentState;
            CurrentState = switchesState;

            CurrentState.InAction?.Invoke();

            Node.RenderProcess += CurrentState.RenderProcessAction;
            Node.PhysicsProcess += CurrentState.PhysicsProcessAction;
            Node.InputProcess += CurrentState.InputProcessAction;

            ChangeState?.Invoke(CurrentState.Name);
        }
    }
}
