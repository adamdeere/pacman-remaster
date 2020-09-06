using OpenGL_Game.Objects;

namespace OpenGL_Game.Systems
{
    interface ISystem
    {
        void OnAction(Entity entity, Entity player);

        // Property signatures: 
        string Name
        {
            get;
        }
    }
}
