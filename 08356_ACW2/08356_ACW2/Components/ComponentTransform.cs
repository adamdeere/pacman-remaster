using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentTransform : IComponent
    {
        Vector3 position;
        Vector3 rotation;
        Vector3 scale;

        public ComponentTransform(Vector3 pos, Vector3 rot, Vector3 scale)
        {
            position = pos;
            rotation = rot;
            this.scale = scale; 
        }

        public ComponentTransform(Vector3 pos)
        {
            position = pos;
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }
        public Vector3 Rotation
        {
            get { return rotation; }
            set { rotation = value; }
        }
        public Vector3 Scale
        {
            get { return scale; }
            set { scale = value; }
        }

        public ComponentTypes ComponentType
        {
            get { return ComponentTypes.COMPONENT_TRANSFORM; }
        }
    }
}
