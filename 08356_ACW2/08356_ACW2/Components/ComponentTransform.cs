using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using OpenTK;

namespace OpenGL_Game.Components
{
    class ComponentTransform : IComponent
    {
        private Vector3 position;
        private Vector3 oldPosition;
        private Vector3 rotation;
        private Vector3 scale;
        private Matrix4 world;
        public ComponentTransform(Vector3 pos, Vector3 rot, Vector3 scale)
        {
            position = pos;
            rotation = rot;
            this.scale = scale; 
        }

        public Matrix4 WorldMatrix
        {
            get { return world; }
            set { world = value; }
        }

        public Vector3 Position
        {
            get { return position; }
            set { position = value; }
        }

        public Vector3 OldPosition
        {
            get { return oldPosition; }
            set { oldPosition = value; }
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
