using Godot;
using System;
using Ellizium.Core.FSMs;

namespace Ellizium.Core.Entites
{
    public class Entity : EGL.Nodes3D.Physics.KinematicBody
    {
        protected MotionData Velocity = new MotionData(0.5f);

        protected MeshInstance Model = new MeshInstance();
        protected CollisionShape Collision = new CollisionShape();

        public object CustomData;

        public EntityData Data = new EntityData();
        public FSM FSM;

        public Entity()
        {
            Base.AddChild(Model);
            Base.AddChild(Collision);

            FSM = new FSM(this);
            PhysicsProcess += CalculatingMotion;
        }

        private void CalculatingMotion(float delta)
        {
            Velocity.Force = Velocity.Force.LinearInterpolate(Velocity.ForceInRest, Velocity.Fading);
            Data.AdditionalVelocity.Force = Data.AdditionalVelocity.Force.LinearInterpolate(Data.AdditionalVelocity.ForceInRest, Data.AdditionalVelocity.Fading);
            Data.MoveVelocity.Force = Data.MoveVelocity.Force.LinearInterpolate(Data.MoveVelocity.ForceInRest, Data.MoveVelocity.Fading);

            Velocity.Force += Data.AdditionalVelocity.Force;
            Velocity.Force += Data.MoveVelocity.Force;
            Velocity.Force.y -= Data.Gravity;

            Velocity.Force = Base.MoveAndSlide(Velocity.Force, Vector3.Up);
        }
    }
}
