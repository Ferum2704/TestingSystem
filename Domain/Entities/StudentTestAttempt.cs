using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class StudentTestAttempt
    {
        public Guid Id { get; set; }

        public Guid StudentId { get; set; }

        public Student Student { get; set; }

        public Guid TestId { get; set; }

        public Test Test { get; set; }

        public int NumberOfAttemts { get; set; }
    }
}
