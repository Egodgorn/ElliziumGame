using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ellizium.Core.Entites;
using Ellizium.Core.Controls;
using Godot;

namespace Ellizium.Game.Character
{
    public class TestPlayer : Entity
    {
        private EntityController _Controler;

        public EntityController Control
        {
            get
            {
                return _Controler;
            }
            set
            {
                if (_Controler != null)
                    _Controler.Unlink();

                _Controler = value;
                _Controler.Link(Body, Data);
            }
        }

        public TestPlayer(Mesh mesh, Shape shape) : base(mesh, shape)
        {
            
        }

        protected override void RenderProcess(float delta)
        {
            Main.Label.Text = $"Position: {Body.Translation.x}, {Body.Translation.y}, {Body.Translation.z}";
        }
        protected override void PhysicsProcess(float delta)
        {
            
        }
        protected override void _Input(InputEvent ev)
        {
            
        }
    }
}
