using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DevIO.Business.Models
{
    public class Entity
    {
        public Guid Id { get; set; }
        public Entity()
        {
            Id = Guid.NewGuid();
        }
    }
}
