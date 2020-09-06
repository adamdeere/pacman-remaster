using System;
using System.Collections.Generic;
using OpenTK;
using OpenTK.Graphics.OpenGL;
using OpenTK.Input;
using OpenGL_Game.Components;
using OpenGL_Game.Systems;
using OpenGL_Game.Managers;
using OpenGL_Game.Objects;
using OpenTK.Graphics;
using System.IO;

namespace OpenGL_Game
{
    /// <summary>
    /// This is the main type for your game
    /// </summary>
    public class MyGame : GameWindow
    {
        public Matrix4 view, projection;
        EntityManager entityManager;
        SystemManager systemManager;

        public static MyGame gameInstance;

        public MyGame()
           : base(
               1500, // Width
               1000, // Height
               GraphicsMode.Default,
               "ACW",
               GameWindowFlags.Default,
               DisplayDevice.Default,
               3, // major
               3, // minor
               GraphicsContextFlags.ForwardCompatible
               )
        {
            gameInstance = this;
            entityManager = new EntityManager();
            systemManager = new SystemManager();
        }
        private void CreateEntities()
        {
            Entity newEntity;

            newEntity = new Entity("sphere");
            newEntity.AddComponent(new ComponentTransform(new Vector3(-1.0f, 0.0f, -3.0f), new Vector3(0,0,0), new Vector3(.5f,.5f,.5f)));
            newEntity.AddComponent(new ComponentGeometry(ResourceManager.FindGeometry(newEntity.Name)));
            newEntity.AddComponent(new ComponentTexture(ResourceManager.FindTexture("ship")));
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Cube");
            newEntity.AddComponent(new ComponentTransform(new Vector3(0, 0.0f, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1)));
            newEntity.AddComponent(new ComponentGeometry(ResourceManager.FindGeometry(newEntity.Name)));
            newEntity.AddComponent(new ComponentTexture(ResourceManager.FindTexture("ship")));
            entityManager.AddEntity(newEntity);
        }

        private void CreateSystems()
        {
            ISystem newSystem;

            newSystem = new SystemRender();
            systemManager.AddSystem(newSystem);
        }
        private void CreateGeometry()
        {
            ResourceManager.LoadGeometry("sphere", "Geometry/TextureSphereTri.obj");
            ResourceManager.LoadGeometry("Cube", "Geometry/Cube.obj");
            //using (StreamReader modelSR = new StreamReader(@"Utility/Models/modelFile.txt"))
            //{
            //    List<string> fileList = new List<string>();
            //    while (modelSR.Peek() > -1)
            //    {
            //        string line = modelSR.ReadLine();
            //        string[] result = line.Split(new string[] { "\n", "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
            //        fileList.Add(result[0]);
            //    }

            //    for (int i = 0; i < fileList.Count; i++)
            //    {
            //        string[] splitStrings = fileList[i].Split(' ');

            //       // vertexBuffers.Add(mVAO_ID[i], splitStrings[0]);
            //    }
            //}
        }

        private void CreateTextures()
        {
            ResourceManager.LoadTexture("ship", "Textures/spaceship.png");
        }


        protected override void OnResize(EventArgs e)
        {
            base.OnResize(e);
            GL.Viewport(ClientRectangle);
                float windowHeight = ClientRectangle.Height;
                float windowWidth = ClientRectangle.Width;
                if (windowHeight > windowWidth)
                {
                    if (windowWidth < 1) { windowWidth = 1; }
                    float ratio = windowHeight / windowWidth;
                    projection = Matrix4.CreatePerspectiveFieldOfView(ratio, 1, 0.5f, 50);
                
                }
                else
                {
                    if (windowHeight < 1) { windowHeight = 1; }
                    float ratio = windowWidth / windowHeight;
                    projection = Matrix4.CreatePerspectiveFieldOfView(1, ratio, 0.5f, 50);
                  
                }
            
        }

        /// <summary>
        /// Allows the game to setup the environment and matrices.
        /// </summary>
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            GL.ClearColor(0.0f, 0.0f, 0.0f, 1.0f);
            GL.Enable(EnableCap.CullFace);
            view = Matrix4.LookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), 800f / 480f, 0.01f, 100f);

            CreateGeometry();
            CreateTextures();
            CreateEntities();
            CreateSystems();

            // TODO: Add your initialization logic here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        protected override void OnUpdateFrame(FrameEventArgs e)
        {
            base.OnUpdateFrame(e);
            if (GamePad.GetState(1).Buttons.Back == ButtonState.Pressed || Keyboard.GetState().IsKeyDown(Key.Escape))
                Exit();

            // TODO: Add your update logic here
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="e">Provides a snapshot of timing values.</param>
        protected override void OnRenderFrame(FrameEventArgs e)
        {
            base.OnRenderFrame(e);
            GL.Viewport(0, 0, Width, Height);
            GL.Clear(ClearBufferMask.ColorBufferBit | ClearBufferMask.DepthBufferBit);

            systemManager.ActionSystems(entityManager);

            GL.Flush();
            SwapBuffers();
        }

        protected override void OnUnload(EventArgs e)
        {
            base.OnUnload(e);
        }
    }
}
