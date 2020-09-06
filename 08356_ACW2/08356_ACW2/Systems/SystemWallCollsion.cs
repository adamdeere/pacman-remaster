using OpenGL_Game.Components;
using OpenGL_Game.Objects;
using OpenGL_Game.Utilites;
using OpenTK;
using System.Collections.Generic;

namespace OpenGL_Game.Systems
{
    class SystemWallCollsion : ISystem
    {
        const ComponentTypes MASK = ComponentTypes.COMPONENT_TRANSFORM | ComponentTypes.COMPONENT_WALLCOLLSION;
        public string Name
        {
            get { return "SystemWallCollsion"; }
        }

        public void OnAction(Entity entity, Entity player)
        {
            if ((entity.Mask & MASK) == MASK)
            {
                List<IComponent> components = entity.Components;

                IComponent transfromComponent = components.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_TRANSFORM;
                });

                List<IComponent> playerComponents = player.Components;

                IComponent playerTransform = playerComponents.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_TRANSFORM;
                });

                IComponent playerPhysics = playerComponents.Find(delegate (IComponent component)
                {
                    return component.ComponentType == ComponentTypes.COMPONENT_PHYSICS;
                });

                Collide((ComponentPhysics)playerPhysics, (ComponentTransform)playerTransform);
            }
        }

        private void Collide(ComponentPhysics playerPhysics, ComponentTransform playerTransform)
        {
            Vector3 wallNormal = new Vector3(0, 0, -1);
            float product = Vector3.Dot(playerTransform.Position, wallNormal) * Vector3.Dot(playerTransform.OldPosition, wallNormal);

            Vector3 movementNormal = (playerTransform.Position- playerTransform.OldPosition).Normalized();
            Vector3 wallPointOne = new Vector3(-1, 0, 0);
            Vector3 wallPointTwo = new Vector3(1, 0, 0);
           
            float product2 = Vector3.Dot(wallPointOne, movementNormal) * Vector3.Dot(wallPointTwo, movementNormal);

            if (product < 0 && product2 < 0)
            {
                int i = 0;
                i++;
            }
        }
    }
}
