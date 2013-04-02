using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Craft.Net.Client.Events;
using Ionic.Zip;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using System.Drawing;
using Craft.Net.Client;
using System.Net;

namespace Crafty
{
    public class MainWindow : GameWindow
    {
        Matrix4 matrixProjection, matrixModelview;

        public MinecraftClient Client { get; set; }
        public DateTime StartTime { get; set; }

        public MainWindow(MinecraftClient client)
        {
            Client = client;
        }

        public void Run(IPEndPoint endPoint)
        {
            Client.ChunkRecieved += OnChunkRecieved;
            //Client.Connect(endPoint);
            StartTime = DateTime.Now;
            Run();
        }

        protected override void OnLoad(EventArgs e)
        {
            GL.ClearColor(Color.CornflowerBlue);
            GL.Enable(EnableCap.DepthTest);
            GL.Enable(EnableCap.CullFace);
            GL.Enable(EnableCap.Texture2D);
            GL.EnableClientState(ArrayCap.VertexArray);
            GL.EnableClientState(ArrayCap.TextureCoordArray);
            Textures.LoadTerrainTextures();
            Title = "Crafty";
        }

        protected override void OnResize(EventArgs e)
        {
            GL.Viewport(0, 0, Width, Height);
            matrixProjection = Matrix4.CreatePerspectiveFieldOfView((float)Math.PI / 4, Width / (float)Height, 1f, 100f);
            GL.MatrixMode(MatrixMode.Projection);
            GL.LoadMatrix(ref matrixProjection);
        }

        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            
        }

        float[] texCoords = {
			0.125f, 0, 0.0625f, 0, 0.0625f, 0.0625f, 0.125f, 0.0625f
		};

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            matrixModelview = Matrix4.CreateRotationX((float)(DateTime.Now - StartTime).TotalMilliseconds / 1000);
            matrixModelview *= Matrix4.CreateRotationY((float)(DateTime.Now - StartTime).TotalMilliseconds / 1000);
            matrixModelview *= Matrix4.LookAt(new Vector3(0, 0, -5), Vector3.Zero, new Vector3(0, 1, 0));
            GL.MatrixMode(MatrixMode.Modelview);
            GL.LoadMatrix(ref matrixModelview);

            GL.VertexPointer(3, VertexPointerType.Float, 0, Models.CubeVerticies);
            GL.BindTexture(TextureTarget.Texture2D, Textures.TerrainId);
            GL.TexCoordPointer(2, TexCoordPointerType.Float, 0, texCoords);
            GL.DrawElements(BeginMode.Triangles, Models.CubeIndicies.Length, DrawElementsType.UnsignedByte, Models.CubeIndicies);

            SwapBuffers();
        }

        private void OnChunkRecieved(object sender, ChunkRecievedEventArgs chunkRecievedEventArgs)
        {

        }
    }
}
