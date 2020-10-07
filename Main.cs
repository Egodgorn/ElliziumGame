using Godot;
using System;
using Ellizium.Game.Character;
using Ellizium.Core.Generation.Chunks;
using Ellizium.Core.Generation.Worlds;

public class Main : Node
{
    public static Action<float> Render = null;
    public static Action<float> Physics = null;

    private ChunkGenerator Generator;
    private OpenSimplexNoise Noise;
    private Node Separator;

    public static Label Label;

    public override void _Ready()
    {
        Console.ForegroundColor = ConsoleColor.White;

        Label = (Label)GetNode("Label");

        Separator = new Node();

        Observer observer = new Observer();

        AddChild(observer);

        AddChild(Separator);

        Noise = new OpenSimplexNoise();
        Noise.Octaves = 4;
        Noise.Period = 50f;
        Noise.Persistence = 0.3f;
        Generator = new ChunkGenerator(0.5f, Noise);

        Generator.ChunkReady += ConnectChunk;
    }

    public override void _Process(float delta)
    {
        if(Render != null)
            Render(delta);
    }
    public override void _PhysicsProcess(float delta)
    {
        if (Physics != null)
            Physics(delta);
    }

    public override void _Input(InputEvent ev)
    {
        if(ev is InputEventKey key && key.Scancode == (int)KeyList.G && key.Pressed)
        {
            Separator.QueueFree();

            Separator = new Node();
            AddChild(Separator);

            Test();
        }
    }

    private void Test()
    {
        Noise.Seed = ++Noise.Seed;

        for (int x = 0; x < 6; x++)
        {
            for (int z = 0; z < 6; z++)
            {
                Generator.GenerateChunk(new Vector2(x, z));
            }
        }
    }

    private void ConnectChunk(int indexThread, long timeGeneration)
    {
        Chunk chunk = Generator.GetChunk();
        Separator.AddChild(chunk.Base);

        Console.WriteLine($"Generation complite in thread: {indexThread}, time: {timeGeneration} ms. ");
    }
}
