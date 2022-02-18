using devDept.Eyeshot.Entities;
using System.Drawing;

namespace Br3D
{
    public class EntityProperties
    {
        public Entity entity;

        public EntityProperties(Entity entity)
        {
            this.entity = entity;
        }

        public Color color { get => entity.Color; set => entity.Color = value; }
        public colorMethodType colorMethodtype
        {
            get => entity.ColorMethod; 
            set
            {
                entity.ColorMethod = value;
            }
        }
    }
}
