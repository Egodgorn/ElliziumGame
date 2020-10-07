using Godot;
using System;

namespace Ellizium.Core.Entites
{
    public class EntityBody : KinematicBody
    {
        private EntityData Data;
        public Vector3 Velocity = new Vector3();

        public MeshInstance Model;
        public CollisionShape Physics;

        public Action<float> RenderProcess = null;
        public Action<float> PhysicsProcess = null;
        public Action<InputEvent> Input = null;

        public EntityBody(EntityData data, Mesh mesh, Shape shape)
        {
            Data = data;

            Model = new MeshInstance() { Mesh = mesh };
            Physics = new CollisionShape() { Shape = shape };

            AddChild(Model);
            AddChild(Physics);
        }

        public override void _Process(float delta)
        {
            RenderProcess?.Invoke(delta);
        }

        public override void _PhysicsProcess(float delta)
        {
            PhysicsProcess?.Invoke(delta);

            Velocity = Velocity.LinearInterpolate(new Vector3(0, Velocity.y, 0), Data.Acceleration);
            Data.MoveVelocity = Data.MoveVelocity.LinearInterpolate(Vector3.Zero, Data.MoveAcceleration);
            Data.AdditionalVelocity = Data.AdditionalVelocity.LinearInterpolate(Vector3.Zero, Data.AdditionalAcceleration);

            Velocity.y += Data.Gravity;
            Velocity += Data.MoveVelocity;
            Velocity += Data.AdditionalVelocity;

            Velocity = MoveAndSlide(Velocity, Vector3.Up, true);
        }
        public override void _Input(InputEvent ev)
        {
            Input?.Invoke(ev);
        }
    }
}
