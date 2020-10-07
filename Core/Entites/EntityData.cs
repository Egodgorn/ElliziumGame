using Godot;
using System;

namespace Ellizium.Core.Entites
{
    public class EntityData
    {
        public float Gravity = -1f;
        public float Acceleration = 0.5f;
        public float MoveAcceleration = 0.5f;
        public float AdditionalAcceleration = 0.5f;
        public Vector3 MoveVelocity = new Vector3();
        public Vector3 AdditionalVelocity = new Vector3();
    }
}
