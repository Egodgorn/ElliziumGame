using Godot;
using System;
using Ellizium.Core.FSMs;
using Ellizium.Core.Controls;


namespace Ellizium.Game.Test
{
    public class TestController : EntityController
    {
        private DirectionController _DirectionController;

        protected override void _Link()
        {
            Papet.InputProcess += FSMControl;

            Papet.FSM.AddState(new State("Idle", StateMode.BlackList, ""));
            Papet.FSM.AddState(new State("Move", StateMode.BlackList, "") { PhysicsProcessAction = Move });
            Papet.FSM.AddState(new State("Jump", StateMode.WhiteList, "Idle") { InAction = Jump, PhysicsProcessAction = OffJump });

            Papet.FSM.SwitchState("Idle");

            _DirectionController = new DirectionController(Papet.FSM, 2f);
        }
        protected override void _Unlink()
        {
            Papet.InputProcess -= FSMControl;

            Papet.FSM.RemoveState("Idle");
            Papet.FSM.RemoveState("Move");
            Papet.FSM.RemoveState("Jump");
        }

        private void FSMControl(InputEvent ev)
        {
            if (ev is InputEventKey keyEv)
            {
                KeyList key = (KeyList)keyEv.Scancode;

                if(key == KeyList.W)
                {
                    if (keyEv.Pressed)
                        _DirectionController.z = 1f;
                    else
                        _DirectionController.z = 0f;
                }
                if (key == KeyList.S)
                {
                    if (keyEv.Pressed)
                        _DirectionController.z = -1f;
                    else
                        _DirectionController.z = 0f;
                }
                if (key == KeyList.A)
                {
                    if (keyEv.Pressed)
                        _DirectionController.x = 1f;
                    else
                        _DirectionController.x = 0f;
                }
                if (key == KeyList.D)
                {
                    if (keyEv.Pressed)
                        _DirectionController.x = -1f;
                    else
                        _DirectionController.x = 0f;
                }
                if (key == KeyList.Space)
                {
                    if (keyEv.Pressed)
                        Papet.FSM.SwitchState("Jump");
                }
            }
        }


        private struct DirectionController
        {
            private FSM FSM;
            public float Speed;
            public Vector3 Direction;

            public float x
            {
                get
                {
                    return Direction.x;
                }
                set
                {
                    Direction.x = value * Speed;
                    if (value == 0 && Direction.z == 0)
                        FSM.SwitchState("Idle");
                    else
                        FSM.SwitchState("Move");
                }
            }
            public float z
            {
                get
                {
                    return Direction.z;
                }
                set
                {
                    Direction.z = value * Speed;
                    if (value == 0 && Direction.x == 0)
                        FSM.SwitchState("Idle");
                    else
                        FSM.SwitchState("Move");
                }
            }

            public DirectionController(FSM fsm, float speed)
            {
                FSM = fsm;
                Speed = speed;
                Direction = new Vector3();
            }
        }
        private void Move(float delta)
        {
            Papet.Data.MoveVelocity.Force = _DirectionController.Direction;
        }
        private void Jump()
        {
            Papet.Data.MoveVelocity.Force.y += 45;
        }
        private void OffJump(float delta)
        {
            if (Papet.Base.IsOnFloor())
            {
                Papet.FSM.SwitchState("Idle");
            }
        }
    }
}
