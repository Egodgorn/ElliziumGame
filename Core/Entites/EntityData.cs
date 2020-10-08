using System;
using System.Collections.Generic;


namespace Ellizium.Core.Entites
{
    public class EntityData
    {
        public float Gravity = 1f;
        public MotionData MoveVelocity = new MotionData();
        public MotionData AdditionalVelocity = new MotionData();
    }
}
