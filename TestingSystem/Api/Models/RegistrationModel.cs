﻿using Application.Identitity;
using Infrastructure.Authentication;

namespace Presentation.Api.Models
{
    public class RegistrationModel
    {
        public string Username { get; set; }

        public string Password { get; set; }

        public string? FirstName { get; set; }

        public string? LastName { get; set; }

        public ApplicationUserRole Role { get; set; }
    }
}