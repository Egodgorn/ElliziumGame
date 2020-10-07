using Godot;
using System;
using Ellizium.Core.Controls;

namespace Ellizium.Game.Character
{
    public class ExampleWalk : EntityController
    {
        private Camera Camera = new Camera();
        private float Jump = 20f;
        private float Speed = 5f;
        private float Acceleration = 3f;
        private float Sensitivity = 0.005f;

        protected override void _Link()
        {
            Body.AddChild(Camera);
            Camera.Translation = new Vector3();
            Camera.RotationDegrees = new Vector3(-20, 180, 0);
            Data.Gravity = 0f;
        }
        protected override void _Unlink()
        {
            Body.RemoveChild(Camera);
        }

        protected override void RenderProcess(float delta)
        {
            
        }
        protected override void PhysicsProcess(float delta)
        {
            Control(delta);
        }
        protected override void _Input(InputEvent ev)
        {
            CameraControl(ev);
        }

        private void Control(float delta)
        {
            Vector3 velocity = new Vector3();

            if (Input.IsKeyPressed((int)KeyList.W))
            {
                velocity.z = Speed;
            }
            if (Input.IsKeyPressed((int)KeyList.S))
            {
                velocity.z = -Speed;
            }
            if(Input.IsKeyPressed((int)KeyList.A))
            {
                velocity.x = Speed;
            }
            if (Input.IsKeyPressed((int)KeyList.D))
            {
                velocity.x = -Speed;
            }
            if (Input.IsKeyPressed((int)KeyList.Space))
            {
                velocity.y += Speed;
            }
            if (Input.IsKeyPressed((int)KeyList.Control))
            {
                velocity.y -= Speed;
            }
            if (Input.IsKeyPressed((int)KeyList.Shift))
            {
                velocity.z *= Acceleration;
            }

            velocity = velocity.Rotated(Vector3.Up, Body.Rotation.y);

            Data.MoveVelocity += velocity;
        }
        private void CameraControl(InputEvent ev)
        {
            if(ev is InputEventMouseMotion motion && Input.GetMouseMode() == Input.MouseMode.Captured)
            {
                Body.Rotation -= new Vector3(0, motion.Relative.x * Sensitivity, 0);

                if (motion.Relative.y > 0 && Camera.RotationDegrees.x < 90 || motion.Relative.y < 0 && Camera.RotationDegrees.x > -90)
                    Camera.Rotation -= new Vector3(motion.Relative.y * Sensitivity, 0, 0);
            }
            if(ev is InputEventKey key)
            {
                if (key.Pressed)
                {
                    if (key.Scancode == (int)KeyList.Tab)
                    {
                        if (Input.GetMouseMode() == Input.MouseMode.Visible)
                            Input.SetMouseMode(Input.MouseMode.Captured);
                        else
                            Input.SetMouseMode(Input.MouseMode.Visible);
                    }
                }
            }
            if (ev is InputEventMouseButton button && false)
            {
                if (button.Pressed)
                {
                    if(button.ButtonIndex == (int)ButtonList.WheelUp && button.Alt)
                    {
                        if(Camera.Translation.z < 0)
                        {
                            Camera.Translation += new Vector3(0, 0.3f, 1f);
                        }
                    }
                    if (button.ButtonIndex == (int)ButtonList.WheelDown && button.Alt)
                    {
                        if (Camera.Translation.z > -4)
                        {
                            Camera.Translation -= new Vector3(0, 0.3f, 1f);
                        }
                    }
                }
            }
        }
    }
}
