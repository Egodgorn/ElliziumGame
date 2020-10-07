using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using Godot;


public class Observer : Camera
{
    private float MouseSens = 0.005f;

    public bool CanMove = true;
    public float Speed = 0.2f;

    public override void _PhysicsProcess(float delta)
    {
        if (!CanMove) return;

        Vector3 vel = new Vector3();

        if (Input.IsKeyPressed((int)KeyList.W))
        {
            vel -= new Vector3(0, 0, Speed);
        }
        if (Input.IsKeyPressed((int)KeyList.S))
        {
            vel += new Vector3(0, 0, Speed);
        }
        if (Input.IsKeyPressed((int)KeyList.A))
        {
            vel -= new Vector3(Speed, 0, 0);
        }
        if (Input.IsKeyPressed((int)KeyList.D))
        {
            vel += new Vector3(Speed, 0, 0);
        }
        if (Input.IsKeyPressed((int)KeyList.Shift))
        {
            vel -= new Vector3(0, Speed, 0);
        }
        if (Input.IsKeyPressed((int)KeyList.Space))
        {
            vel += new Vector3(0, Speed, 0);
        }

        vel = vel.Rotated(Vector3.Up, Rotation.y);

        Translation += vel;
    }
    public override void _Input(InputEvent ev)
    {
        if (!CanMove) return;

        if (ev is InputEventKey key && key.Scancode == (int)KeyList.Tab && key.Pressed)
        {
            if (Input.GetMouseMode() == Input.MouseMode.Visible)
                Input.SetMouseMode(Input.MouseMode.Captured);
            else
                Input.SetMouseMode(Input.MouseMode.Visible);
        }

        if (ev is InputEventMouseMotion && Input.GetMouseMode() == Input.MouseMode.Captured)
        {
            InputEventMouseMotion motion = ev as InputEventMouseMotion;

            Rotation = new Vector3(Rotation.x - motion.Relative.y * MouseSens, Rotation.y, Rotation.z);
            RotateY(-motion.Relative.x * MouseSens);
        }
    }
	
    private void OnMove()
    {
        CanMove = true;
    }
    private void OffMove()
    {
        CanMove = false;
    }
}

