using System;
using System.Threading;
using Ellizium.Core.Utility;
using System.Diagnostics;
using System.Collections.Generic;
using Ellizium.Core.Generation.Chunks;

namespace Ellizium.Core.Generation.Worlds
{
    public class ChunkGenerator
    {
        private Godot.OpenSimplexNoise Noise;
        private ETimer Timer;

        private bool[] IsFree;
        private AutoResetEvent[] ResetEvents;
        private Thread[] GenerationThreads;

        private Queue<Godot.Vector2> QueueGeneration = new Queue<Godot.Vector2>();
        private Queue<Chunk> Chunks = new Queue<Chunk>();

        public event Action<int, long> ChunkReady;

        public int CountChunkReady
        {
            get
            {
                return Chunks.Count;
            }
        }

        public ChunkGenerator(float updateTime, Godot.OpenSimplexNoise noise)
        {
            Noise = noise;
            Timer = new ETimer(updateTime, true, true, UpdateQueue);

            ResetEvents = new AutoResetEvent[Environment.ProcessorCount / 2];
            GenerationThreads = new Thread[Environment.ProcessorCount / 2];
            IsFree = new bool[Environment.ProcessorCount / 2];

            for (int i = 0; i < ResetEvents.Length; i++)
            {
                ResetEvents[i] = new AutoResetEvent(false);
            }
            for (int i = 0; i < IsFree.Length; i++)
            {
                IsFree[i] = true;
            }
            for (int i = 0; i < GenerationThreads.Length; i++)
            {
                GenerationThreads[i] = new Thread(GenerationChunk);
                GenerationThreads[i].Name = $"{i}";
                GenerationThreads[i].Start();
            }

            Timer.Start();
        }

        public void GenerateChunk(Godot.Vector2 position)
        {
            QueueGeneration.Enqueue(position);
        }
        
        public Chunk GetChunk()
        {
            return Chunks.Dequeue();
        }

        private void UpdateQueue()
        {
            for (int i = 0; i < QueueGeneration.Count; i++)
            {
                int index = IsFree.GetFree();

                if (index == -1) return;

                if (QueueGeneration.Count > 0)
                    ResetEvents[index].Set();
            }
        }

        private void GenerationChunk(object obj)
        {
            int threadIndex = int.Parse(Thread.CurrentThread.Name);
            ResetEvents[threadIndex].WaitOne();

            while (true)
            {
                if (QueueGeneration.Count == 0)
                {
                    Console.WriteLine($"Miss in {threadIndex} thread.");
                    IsFree[threadIndex] = true;
                    ResetEvents[threadIndex].WaitOne();
                    continue;
                }
                Godot.Vector2 position = QueueGeneration.Dequeue();

                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                Chunk chunk = new Chunk(position);
                Generation(chunk);
                chunk.Render();
                Chunks.Enqueue(chunk);

                stopwatch.Stop();

                ChunkReady(threadIndex, stopwatch.ElapsedMilliseconds);

                IsFree[threadIndex] = true;
                ResetEvents[threadIndex].WaitOne();
                IsFree[threadIndex] = false;
            }
        }

        private void Generation(Chunk chunk)
        {

            for (int x = 0; x < Chunk.Size.x + 2; x++)
            {
                for (int z = 0; z < Chunk.Size.z + 2; z++)
                {
                    float height = Math.Abs(Noise.GetNoise2d(x + x * Chunk.Size.x, z + z * Chunk.Size.z) * 10 + 5);

                    for (int y = (int)height; y < Chunk.Size.y + 2; y++)
                    {
                        chunk.Blocks[x, y, z] = new Blocks.Block(1, 1);
                    }
                }
            }
        }
    }
}
