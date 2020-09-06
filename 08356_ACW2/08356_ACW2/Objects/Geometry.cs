using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using OpenTK.Graphics.OpenGL;
using OpenTK;

namespace OpenGL_Game.Objects
{
    public class Geometry
    {
        // Graphics
        private int vao_Handle;
        public int vbo_verts;
        private readonly string tag;
        public float[] Vertices { get; private set; }
        public int VertSize { get; private set; }
        public int Vao_Handle { get;  set; }

        public int Vbo_verts { get;  set; }

        public Geometry(string tag)
        {
            this.tag = tag;
        }

        public void LoadObject(string filename)
        {
            try
            {
                GL.GenVertexArrays(1, out vao_Handle);
                GL.BindVertexArray(vao_Handle);
                GL.GenBuffers(1, out vbo_verts);

                List<Vector3> verts = new List<Vector3>();
                List<Vector3> normals = new List<Vector3>();
                List<Vector2> texCoords = new List<Vector2>();
                List<string> indices = new List<string>();
                //loads in an obj and populates the lists needed
                using (StreamReader sr = new StreamReader(filename))
                {

                    while (sr.Peek() > -1)
                    {
                        string line = sr.ReadLine();
                        string[] vertStrings = line.Split(' ');
                        string begin = vertStrings[0];
                        switch (begin)
                        {
                            case "v":

                                float x = float.Parse(vertStrings[1]);
                                float y = float.Parse(vertStrings[2]);
                                float z = float.Parse(vertStrings[3]);
                                verts.Add(new Vector3(x, y, z));
                                break;

                            case "vt":

                                float u = float.Parse(vertStrings[1]);
                                float v = float.Parse(vertStrings[2]);

                                texCoords.Add(new Vector2(u, v));
                                break;

                            case "vn":

                                float xN = float.Parse(vertStrings[1]);
                                float yN = float.Parse(vertStrings[2]);
                                float zN = float.Parse(vertStrings[3]);
                                normals.Add(new Vector3(xN, yN, zN));
                                break;

                            case "f":
                                for (int i = 0; i < vertStrings.Length; i++)
                                {
                                    if (vertStrings[i] != begin)
                                    {
                                        indices.Add(vertStrings[i]);
                                    }
                                }

                                break;
                            default:
                                //do nothing to account for extra strings that may be in the file
                                break;
                        }
                    }
                }

                List<float> vertexCount = new List<float>();

                for (int i = 0; i < indices.Count; i++)
                {
                    string[] splitString = indices[i].Split(' ');
                    string[] indSplit = splitString[0].Split('/');

                    Vector3 vertNumber = verts[int.Parse(indSplit[0]) - 1];
                    Vector2 texNumber = texCoords[int.Parse(indSplit[1]) - 1];
                    Vector3 normalNumber = normals[int.Parse(indSplit[2]) - 1];

                    vertexCount.Add(vertNumber.X);
                    vertexCount.Add(vertNumber.Y);
                    vertexCount.Add(vertNumber.Z);

                    vertexCount.Add(texNumber.X);
                    vertexCount.Add(texNumber.Y);

                    vertexCount.Add(normalNumber.X);
                    vertexCount.Add(normalNumber.Y);
                    vertexCount.Add(normalNumber.Z);
                }

                Vertices = vertexCount.ToArray();
                VertSize = indices.Count * 3;

                verts.Clear();
                normals.Clear();
                texCoords.Clear();
                indices.Clear();
                vertexCount.Clear();

                GL.BindBuffer(BufferTarget.ArrayBuffer, vbo_verts);
                GL.BufferData(BufferTarget.ArrayBuffer, (IntPtr)(Vertices.Length * sizeof(float)), Vertices, BufferUsageHint.StaticDraw);

                // Positions
                GL.EnableVertexAttribArray(0);
                GL.VertexAttribPointer(0, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 0);

                // Tex Coords
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(1, 2, VertexAttribPointerType.Float, false, 8 * sizeof(float), 3* sizeof(float));

                // Normals
                GL.EnableVertexAttribArray(1);
                GL.VertexAttribPointer(2, 3, VertexAttribPointerType.Float, false, 8 * sizeof(float), 5 * sizeof(float));

                GL.BindVertexArray(0);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
      
        public void Render()
        {
            GL.BindVertexArray(vao_Handle);
            GL.DrawArrays(PrimitiveType.Triangles, 0, VertSize);
        }
    }
}
