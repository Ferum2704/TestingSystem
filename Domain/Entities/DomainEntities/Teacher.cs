using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities.DomainEntities
{
    public class Teacher : DomainUser
    {
        public ICollection<Subject> Subjects { get; set; }
    }
}
