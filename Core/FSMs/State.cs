using Godot;
using System;
using System.Collections.Generic;


namespace Ellizium.Core.FSMs
{
    public class State
    {
        public string Name;
        public StateMode Mode; 
        public List<string> PoolEnableStates = new List<string>();
        //---------Actions----------------//
        public Action InAction = null;
        public Action OutAction = null;
        public Action<float> RenderProcessAction = null;
        public Action<float> PhysicsProcessAction = null;
        public Action<InputEvent> InputProcessAction = null;

        public State(string name, StateMode mode, params string[] enableStates)
        {
            Name = name;
            Mode = mode;
            PoolEnableStates.AddRange(enableStates);
        }
    }
}
