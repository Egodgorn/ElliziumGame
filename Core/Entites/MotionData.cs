using Godot;
using System;


namespace Ellizium.Core.Entites
{
    public struct MotionData
    {
        public Vector3 Force;
        public Vector3 ForceInRest;
        public float Fading;

        public MotionData(float fading)
        {
            Force = new Vector3();
            ForceInRest = new Vector3();
            Fading = fading;
        }
    }
}
