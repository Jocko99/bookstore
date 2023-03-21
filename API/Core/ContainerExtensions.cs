using API.Jwt;
using Application;
using Application.Commands.Authors;
using Application.Commands.Books;
using Application.Commands.Genres;
using Application.Commands.Users;
using Application.Queries.Authors;
using Application.Queries.Books;
using Application.Queries.Genre;
using Application.Queries.Users;
using EfDataAccess;
using Implementation.Commands.Authors;
using Implementation.Commands.Books;
using Implementation.Commands.Genres;
using Implementation.Commands.Users;
using Implementation.Queries.Authors;
using Implementation.Queries.Books;
using Implementation.Queries.Genres;
using Implementation.Queries.Users;
using Implementation.Validators.Authors;
using Implementation.Validators.Books;
using Implementation.Validators.Genres;
using Implementation.Validators.Users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API.Core
{
    public static class ContainerExtensions
    {
        public static void AddUseCases(this IServiceCollection services)
        {
            services.AddTransient<UseCaseExecutor>();
            //Authors
            services.AddTransient<ICreateAuthorCommand, EfCreateAuthorCommand>();
            services.AddTransient<IUpdateAuthorCommand, EfUpdateAuthorCommand>();
            services.AddTransient<IDeleteAuthorCommand, EfDeleteAuthorCommand>();
            services.AddTransient<IGetAuthorQuery, EfGetAuthorQuery>();
            //Genres
            services.AddTransient<ICreateGenreCommand, EfCreateGenreCommand>();
            services.AddTransient<IUpdateGenreCommand, EfUpdateGenreCommand>();
            services.AddTransient<IDeleteGenreCommand, EfDeleteGenreCommand>();
            services.AddTransient<IGetGenreQuery, EfGetGenreQuery>();
            //Users
            services.AddTransient<ICreateUserCommand, EfCreateUserCommand>();
            services.AddTransient<IUpdateUserCommand, EfUpdateUserCommand>();
            services.AddTransient<IDeleteUserCommand, EfDeleteUserCommand>();
            services.AddTransient<IGetUserQuery, EfGetUserQuery>();
            //Books
            services.AddTransient<ICreateBookCommand, EfCreateBookCommand>();
            services.AddTransient<IUpdateBookCommand, EfUpdateBookCommand>();
            services.AddTransient<IChangeAuthorCommand, EfChangeAuthorCommand>();
            services.AddTransient<IDeleteBookCommand, EfDeleteBookCommand>();
            services.AddTransient<IGetBookQuery, EfGetBookQuery>();
        }

        public static void AddValidators(this IServiceCollection services)
        {
            //Authors
            services.AddTransient<CreateAuthorValidator>();
            services.AddTransient<UpdateAuthorValidator>();
            //Genres
            services.AddTransient<CreateGenreValidator>();
            services.AddTransient<UpdateGenreValidator>();
            //Users
            services.AddTransient<CreateUserValidator>();
            services.AddTransient<UpdateUserValidator>();
            //Books
            services.AddTransient<CreateBookValidator>();
            services.AddTransient<BookUpdateValidator>();
            services.AddTransient<BookAuthorValidator>();
        }

        public static void AddApplicationActor(this IServiceCollection services)
        {
            services.AddTransient<IApplicationActor>(x =>
            {
                var accessor = x.GetService<IHttpContextAccessor>();

                var user = accessor.HttpContext.User;

                if (user.FindFirst("ActorData") == null)
                {
                    return new AnonymousActor();
                }

                var actorString = user.FindFirst("ActorData").Value;

                var actor = JsonConvert.DeserializeObject<JwtActor>(actorString);

                return actor;
            });
        }

        public static void AddJwt(this IServiceCollection services, AppSettings appSettings)
        {
            services.AddTransient<JwtManager>(x =>
            {
                var context = x.GetService<BookContext>();

                return new JwtManager(context, appSettings.JwtIssuer, appSettings.JwtSecretKey);
            });

            services.AddAuthentication(options =>
            {
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(cfg =>
            {
                cfg.RequireHttpsMetadata = false;
                cfg.SaveToken = true;
                cfg.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidIssuer = appSettings.JwtIssuer,
                    ValidateIssuer = true,
                    ValidAudience = "Any",
                    ValidateAudience = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.JwtSecretKey)),
                    ValidateIssuerSigningKey = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero
                };
            });
        }
    }
}
