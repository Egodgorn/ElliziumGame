using Godot;
using System;
using Ellizium.Core.Generation.Blocks;

namespace Ellizium.Core.Generation.Chunks
{
    public class Chunk
    {
        public static float BlockSize = 1f;
        public static Vector3 Size { get; } = new Vector3(64, 256, 64);

        public Vector2 Position;
        public Block[,,] Blocks;

        public StaticBody Base = new StaticBody();
        private MeshInstance Model = new MeshInstance();
        private CollisionShape Collision = new CollisionShape();

        public Chunk(Vector2 position)
        {
            Position = position;
            Blocks = new Block[(int)Size.x + 2, (int)Size.y + 2, (int)Size.z + 2];

            Base.Translation = new Vector3(Position.x * Size.x, 0, Position.y * Size.z);
            Base.AddChild(Model);
        }

        public void Render()
        {
            SurfaceTool tool = new SurfaceTool();
            tool.Begin(Mesh.PrimitiveType.Triangles);

            for (int x = 1; x < Size.x - 1; x++)
            {
                for (int z = 1; z < Size.z - 1; z++)
                {
                    for (int y = 1; y < Size.y - 1; y++)
                    {
                        BuildFaces(GetSideFree(x, y, z), tool, x, y, z);
                    }
                }
            }

            tool.GenerateNormals();

            Model.Mesh = tool.Commit();
        }

        private bool[] GetSideFree(int x, int y, int z)
        {
            bool[] freeSize = new bool[6];

            if (Blocks[x, y + 1, z].ID == 0) freeSize[0] = true;
            if (Blocks[x, y - 1, z].ID == 0) freeSize[1] = true;
            if (Blocks[x + 1, y, z].ID == 0) freeSize[2] = true;
            if (Blocks[x - 1, y, z].ID == 0) freeSize[3] = true;
            if (Blocks[x, y, z - 1].ID == 0) freeSize[4] = true;
            if (Blocks[x, y, z + 1].ID == 0) freeSize[5] = true;

            return freeSize;
        }
        private void BuildFaces(bool[] freeSides, SurfaceTool tool, int x, int y, int z)
        {
            Vector3 offset = new Vector3(x, y, z);

            if (freeSides[0])
            {
                tool.AddVertex(new Vector3(BlockSize, BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, BlockSize, BlockSize) + offset);

                tool.AddVertex(new Vector3(-BlockSize, BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, BlockSize, -BlockSize) + offset);
            }
            if (freeSides[1])
            {
                tool.AddVertex(new Vector3(BlockSize, -BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, -BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, -BlockSize, BlockSize) + offset);

                tool.AddVertex(new Vector3(-BlockSize, -BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, -BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, -BlockSize, -BlockSize) + offset);
            }
            if (freeSides[2])
            {
                tool.AddVertex(new Vector3(BlockSize, -BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, -BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, BlockSize, BlockSize) + offset);

                tool.AddVertex(new Vector3(BlockSize, BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, -BlockSize, -BlockSize) + offset);
            }
            if (freeSides[3])
            {
                tool.AddVertex(new Vector3(-BlockSize, -BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, -BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, BlockSize, BlockSize) + offset);

                tool.AddVertex(new Vector3(-BlockSize, BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, -BlockSize, -BlockSize) + offset);
            }
            if (freeSides[4])
            {
                tool.AddVertex(new Vector3(BlockSize, -BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, BlockSize, -BlockSize) + offset);

                tool.AddVertex(new Vector3(-BlockSize, BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, -BlockSize, -BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, -BlockSize, -BlockSize) + offset);
            }
            if (freeSides[5])
            {
                tool.AddVertex(new Vector3(BlockSize, -BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, -BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(-BlockSize, BlockSize, BlockSize) + offset);

                tool.AddVertex(new Vector3(-BlockSize, BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, BlockSize, BlockSize) + offset);
                tool.AddVertex(new Vector3(BlockSize, -BlockSize, BlockSize) + offset);
            }
        }
    }
}
