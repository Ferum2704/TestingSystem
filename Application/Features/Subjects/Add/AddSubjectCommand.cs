using Application.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Subjects.Add
{
    public class AddSubjectCommand : IRequest<SubjectDTO>
    {
        public Guid Id { get; set; }

        public string Name { get; set; }

        public Guid TeacherId { get; set; }
    }
}
