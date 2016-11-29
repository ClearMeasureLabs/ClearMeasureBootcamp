using System;

namespace ClearMeasure.Bootcamp.Core.Model
{
    public class Role
    {
        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }

        public Role()
        {
            
        }

        public Role(string name)
        {
            Name = name;
        }
    }
}