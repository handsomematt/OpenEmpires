using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SFML.Graphics;

namespace OpenEmpires
{
    public class EntityManager : Drawable
    {
        public Dictionary<short, Entities.EntityBase> EntityContainer;

        public EntityManager()
        {
            EntityContainer = new Dictionary<short, Entities.EntityBase>();
        }

        public void Draw(RenderTarget rt, RenderStates states)
        {
            foreach (Entities.EntityBase entity in EntityContainer.Values)
                rt.Draw(entity);
        }

        public void Update()
        {

        }
    }
}
