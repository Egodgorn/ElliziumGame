using Godot;
using System;
using Ellizium.Core.Entites;
using Ellizium.Core.FSMs;

namespace Ellizium.Core.Controls
{
    public abstract class EntityController
    {
        public object CustomData;
        protected Entity Papet;

        public void Link(Entity papet)
        {
            Papet = papet;

            _Link();
        }
        public void Unlink()
        {
            _Unlink();

            Papet = null;
        }

        protected abstract void _Link();
        protected abstract void _Unlink();
    }
}
