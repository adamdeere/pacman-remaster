namespace OpenGL_Game.Components
{
    enum ComponentTypes {
        COMPONENT_NONE = 0,
	    COMPONENT_TRANSFORM = 1 << 0,
        COMPONENT_GEOMETRY = 1 << 1,
        COMPONENT_TEXTURE  = 1 << 2,
        COMPONENT_PHYSICS = 1 << 3,
        COMPONENT_WALLCOLLSION = 1 << 4
    }

    interface IComponent
    {
        ComponentTypes ComponentType
        {
            get;
        }
    }
}
