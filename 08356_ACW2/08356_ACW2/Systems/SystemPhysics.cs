using OpenGL_Game.Components;
using OpenGL_Game.Objects;
using OpenGL_Game.Utilites;
using OpenTK;
using System.Collections.Generic;

namespace OpenGL_Game.Systems
{
    class SystemPhysics : ISystem
    {
        const ComponentTypes MASK = ComponentTypes.COMPONENT_TRANSFORM | ComponentTypes.COMPONENT_PHYSICS;
     
        public SystemPhysics()
        {
            
        }
        public string Name
        {
            get { return "SystemPhysics"; }
        }

        public void OnAction(Entity entity, Entity player)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent TransformComp = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_TRANSFORM;
                });

                IComponent PhysicsComp = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_PHYSICS;
                });

                Move((ComponentTransform)TransformComp, (ComponentPhysics)PhysicsComp);
            }
        }

        private void Move(ComponentTransform transform, ComponentPhysics phys)
        {
            transform.OldPosition = transform.Position;
            transform.Position -= phys.Velocity * Timer.GetElapsedSeconds();
        }
    }
}
