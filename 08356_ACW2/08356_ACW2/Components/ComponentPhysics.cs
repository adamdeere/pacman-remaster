using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentPhysics : IComponent
    {
        Vector3 vel;
        public ComponentPhysics()
        {
            vel = new Vector3(0.01f, 0, 0.5f);
        }

        public Vector3 Velocity
        {
            get { return vel; }
            set { vel = value; }
        }
        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_PHYSICS; }
        }
    }
}
