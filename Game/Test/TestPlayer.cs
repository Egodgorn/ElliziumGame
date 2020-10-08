using Godot;
using System;
using Ellizium.Core.Entites;
using Ellizium.Core.Controls;


namespace Ellizium.Game.Test
{
    public class TestPlayer : Entity
    {
        public EntityController Controller;

        public TestPlayer(Mesh mesh, Shape shape)
        {
            Model.Mesh = mesh;
            Collision.Shape = shape;

            Controller = new TestController();

            Controller.Link(this);

            Data.MoveVelocity.Fading = 0.5f;
            Data.AdditionalVelocity.Fading = 0.5f;

            RenderProcess += ViewCurrentFSMState;
        }

        private void ViewCurrentFSMState(float delta)
        {
            Main.Label.Text = FSM.CurrentStateName;
        }
    }
}
