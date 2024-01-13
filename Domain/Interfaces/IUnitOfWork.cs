﻿using Domain.Entities;

namespace Domain.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<Subject> SubjectRepository { get; }

        IGenericRepository<Topic> TopicRepository { get; }

        IGenericRepository<Test> TestRepository { get; }

        IGenericRepository<Question> QuestionRepository { get; }

        IGenericRepository<Student> StudentRepository { get; }

        IGenericRepository<StudentTestAttempt> StudentTestAttemptRepository { get; }

        Task SaveAsync();
    }
}
