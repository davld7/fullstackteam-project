using Ioon.Domain.Common.Enums;
using System.Net;

namespace Ioon.Application.Extensions
{
    public static class HandlerExtensions
    {
        public static ApplicationResponse BuildResponse<TStatus>(TStatus status, object? data = null) where TStatus : struct, Enum
        {
            HttpStatusCode statusCode = HttpStatusCode.InternalServerError;
            string message = string.Empty;

            if (status is EntityUserStatus entity)
            {
                (statusCode, message) = entity switch
                {
                    EntityUserStatus.InvalidPhoneNumber => (HttpStatusCode.BadRequest, "Invalid phone number."),
                    EntityUserStatus.InvalidEmailAddress => (HttpStatusCode.BadRequest, "Invalid email address."),
                    EntityUserStatus.InvalidUserName => (HttpStatusCode.BadRequest, "Invalid username."),
                    EntityUserStatus.InvalidIdentification => (HttpStatusCode.BadRequest, "Invalid identification."),
                    EntityUserStatus.Correct => (HttpStatusCode.OK, "Information correct."),
                    EntityUserStatus.UserCreated => (HttpStatusCode.Created, "User created successfully."),
                    _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred.")
                };
            }

            else if (status is AuthStatus auth)
            {
                (statusCode, message) = auth switch
                {
                    AuthStatus.UserAuthorized => (HttpStatusCode.OK, "User authorized successfully."),
                    AuthStatus.EmailNotVerified => (HttpStatusCode.BadRequest, "Email verification required."),
                    AuthStatus.UserNotFound => (HttpStatusCode.Unauthorized, "Invalid username or password."),
                    AuthStatus.InvalidPassword => (HttpStatusCode.BadRequest, "Invalid password."),
                    _ => (HttpStatusCode.InternalServerError, "An unexpected error occurred during authentication.")
                };
            }

            return new ApplicationResponse
            {
                StatusCode = statusCode,
                Message = message,
                IsSuccessful = statusCode == HttpStatusCode.OK,
                Data = data ?? default
            };

        }
    }
}
