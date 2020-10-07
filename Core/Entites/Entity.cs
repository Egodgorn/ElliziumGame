using Godot;
using System;

namespace Ellizium.Core.Entites
{
    public abstract class Entity
    {
        protected EntityData Data;
        protected EntityBody Body;

        public Vector3 Translation
        {
            get
            {
                return Body.Translation;
            }
            set
            {
                Body.Translation = value;
            }
        }

        public Entity(Mesh mesh, Shape shape)
        {
            Data = new EntityData();
            Body = new EntityBody(Data, mesh, shape);

            Body.RenderProcess += RenderProcess;
            Body.PhysicsProcess += PhysicsProcess;
            Body.Input += _Input;
        }
        public void ConnectToNode(Node parent)
        {
            parent.AddChild(Body);
        }
        public void DisconnectFromNode(Node parent)
        {
            parent.RemoveChild(Body);
        }
        public void AddChild(Node child)
        {
            Body.AddChild(child);
        }
        public void RemoveChild(Node child)
        {
            Body.RemoveChild(child);
        }

        protected abstract void RenderProcess(float delta);
        protected abstract void PhysicsProcess(float delta);
        protected abstract void _Input(InputEvent ev);
    }
}
