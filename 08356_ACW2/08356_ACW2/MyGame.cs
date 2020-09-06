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
using OpenGL_Game.Utilites;

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

            newEntity = new Entity("Sphere");
            newEntity.AddComponent(new ComponentTransform(new Vector3(3, 0.0f, 4), new Vector3(0,0,0), new Vector3(1,1,1)));
            newEntity.AddComponent(new ComponentGeometry(ResourceManager.FindGeometry(newEntity.Name)));
            newEntity.AddComponent(new ComponentTexture(ResourceManager.FindTexture("ship")));
            newEntity.AddComponent(new ComponentPhysics());
            entityManager.AddEntity(newEntity);

            newEntity = new Entity("Cube");
            newEntity.AddComponent(new ComponentTransform(new Vector3(0, 0.0f, 0), new Vector3(0, 0, 0), new Vector3(1, 1, 1)));
            newEntity.AddComponent(new ComponentGeometry(ResourceManager.FindGeometry(newEntity.Name)));
            newEntity.AddComponent(new ComponentTexture(ResourceManager.FindTexture("wall")));
            newEntity.AddComponent(new ComponentWallCollsion());
            entityManager.AddEntity(newEntity);
        }

        private void CreateSystems()
        {
            ISystem newSystem;

            newSystem = new SystemRender();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemPhysics();
            systemManager.AddSystem(newSystem);

            newSystem = new SystemWallCollsion();
            systemManager.AddSystem(newSystem);

        }
        private void CreateResorces()
        {
            using (StreamReader modelSR = new StreamReader(@"Geometry/modelFile.txt"))
            {
               
                while (modelSR.Peek() > -1)
                {
                    string line = modelSR.ReadLine();
                    string[] result = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    ResourceManager.LoadGeometry(result[0], $"Geometry/{result[1]}");
                }
            }

            using (StreamReader textureSR = new StreamReader(@"Geometry/TextureFile.txt"))
            {

                while (textureSR.Peek() > -1)
                {
                    string line = textureSR.ReadLine();
                    string[] result = line.Split(new string[] { " " }, StringSplitOptions.RemoveEmptyEntries);
                    ResourceManager.LoadTexture(result[0], $"Textures/{result[1]}");
                }
            }
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
            GL.Enable(EnableCap.DepthTest); 
            view = Matrix4.LookAt(new Vector3(0, 0, 3), new Vector3(0, 0, 0), new Vector3(0, 1, 0));
            view = Matrix4.CreateTranslation(0, 0, -8);
            projection = Matrix4.CreatePerspectiveFieldOfView(MathHelper.DegreesToRadians(45), 800f / 480f, 0.01f, 100f);

            CreateResorces();
            CreateEntities();
            CreateSystems();

            // TODO: Add your initialization logic here
            Timer.Start();
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
