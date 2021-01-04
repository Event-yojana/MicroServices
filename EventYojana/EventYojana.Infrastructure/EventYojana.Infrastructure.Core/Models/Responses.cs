using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Text;

namespace EventYojana.Infrastructure.Core.Models
{
    public class Responses
    {
        public enum ResponseCode
        {
            Success = 200,
            NoContent = 204,
            BadRequest = 400,
            UnAuthorized = 401,
            Forbidden = 403,
            NotFound = 404,
            InternalError = 500,
            PasswordNotMatch = 001
        }

        public static readonly ImmutableDictionary<int, string> ResponseMessage = new Dictionary<int, string>()
        {
            { 200, "Success"},
            { 204, "No content found"},
            { 400, "Bad request"},
            { 401, "You are not Authorized to access this resource" },
            { 403, "You are Forbidden to access this resource"},
            { 404, "This Resource you are looking for is not available"},
            { 500, "Some error occured. Please try again"},
            { 001, "Password Not Matched"}
        }.ToImmutableDictionary();
    }
}
