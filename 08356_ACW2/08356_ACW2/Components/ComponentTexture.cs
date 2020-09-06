using OpenGL_Game.Managers;
using OpenGL_Game.Objects;

namespace OpenGL_Game.Components
{
    class ComponentTexture : IComponent
    {
        public Texture Texture { get; }
        public ComponentTexture(Texture tex)
        {
            Texture = tex;
        }
        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_TEXTURE; }
        }
    }
}
