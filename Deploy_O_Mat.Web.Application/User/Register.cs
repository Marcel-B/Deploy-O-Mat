using System;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using com.b_velop.Deploy_O_Mat.Web.Common.Exceptions;
using com.b_velop.Deploy_O_Mat.Web.Data.Context;
using com.b_velop.Deploy_O_Mat.Web.Identity.Models;
using Deploy_O_Mat.Web.Application.Interfaces;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace com.b_velop.Deploy_O_Mat.Web.Application.User
{
    public class Register
    {
        public class Command : IRequest<User>
        {
            public string DisplayName { get; set; }
            public string Username { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.DisplayName).NotEmpty();
                RuleFor(x => x.Username).NotEmpty();
                RuleFor(x => x.Email).NotEmpty().EmailAddress();
                RuleFor(x => x.Password).NotEmpty();
            }
        }
        public class Handler : IRequestHandler<Command, User>
        {
            private readonly WebContext _context;
            private readonly IJwtGenerator _jwtGenerator;
            private readonly UserManager<AppUser> _userManager;

            public Handler(WebContext context, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                _userManager = userManager;
                _jwtGenerator = jwtGenerator;
                _context = context;
            }

            public async Task<User> Handle(Command request, CancellationToken cancellationToken)
            {
                if (await _context.Users.Where(x => x.Email == request.Email).AnyAsync())
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Email = "Email already exists" });
                }
                if (await _context.Users.Where(x => x.UserName == request.Username).AnyAsync())
                {
                    throw new RestException(HttpStatusCode.BadRequest, new { Username = "Username already exists" });
                }

                var user = new AppUser
                {
                    DisplayName = request.DisplayName,
                    Email = request.Email,
                    UserName = request.Username
                };

                // Handler logic goes here
                var result = await _userManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                    return new User
                    {
                        DisplayName = user.DisplayName,
                        Token = await _jwtGenerator.CreateToken(user),
                        Username = user.UserName,
                    };

                throw new Exception("Problem creating user");
            }
        }
    }
}
