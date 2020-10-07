using Godot;
using System;
using Ellizium.Core.Entites;

namespace Ellizium.Core.Controls
{
    public abstract class EntityController
    {
        public object CustomData;
        protected EntityBody Body;
        protected EntityData Data;

        public void Link(EntityBody body, EntityData data)
        {
            Data = data;
            Body = body;
            Body.RenderProcess += RenderProcess;
            Body.PhysicsProcess += PhysicsProcess;
            Body.Input += _Input;

            _Link();
        }
        public void Unlink()
        {
            Body.RenderProcess -= RenderProcess;
            Body.PhysicsProcess -= PhysicsProcess;
            Body.Input -= _Input;

            _Unlink();
            Body = null;
            Data = null;
        }

        protected abstract void _Link();
        protected abstract void _Unlink();
        protected abstract void RenderProcess(float delta);
        protected abstract void PhysicsProcess(float delta);
        protected abstract void _Input(InputEvent ev);
    }
}
