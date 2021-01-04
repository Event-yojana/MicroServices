using EventYojana.Infrastructure.Core.Enum;
using EventYojana.Infrastructure.Core.Interfaces;
using EventYojana.Infrastructure.Core.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;

namespace EventYojana.Infrastructure.Core.Helpers
{
    public sealed class RequestContextAdapter : IRequestContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly string _connectionString;

        public RequestContextAdapter(IHttpContextAccessor httpContextAccessor, IConfiguration configuration)
        {
            _httpContextAccessor = httpContextAccessor;
            _connectionString = configuration.GetConnectionString("EventYojanaDb");
        }
        
        public JwtSecurityToken JwtSecurityToken
        {
            get
            {
                var jwt = _httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                if(jwt.Count > 0)
                {
                    var handler = new JwtSecurityTokenHandler();
                    var token = handler.ReadJwtToken(jwt.ToString().Split(' ').Last());
                    return token;
                }
                return new JwtSecurityToken();
            }
        }

        public UserSettings LogOnUserDetails
        {
            get
            {
                UserSettings userSettings = new UserSettings();
                if (JwtSecurityToken.Claims.Count() > 0)
                {
                    userSettings.UserId = Convert.ToInt32(JwtSecurityToken.Claims.First(x => x.Type == "id").Value);
                    userSettings.UserRole = (ApplicationEnum.UserRoleEnum)Convert.ToInt32(JwtSecurityToken.Claims.First(x => x.Type == "id").Value);
                    userSettings.UserModule = GetUserModules((int)userSettings.UserRole);
                }
                return userSettings;
            }
        }

        private List<UserModule> GetUserModules(int RoleId)
        {
            List<UserModule> userModules = new List<UserModule>();
            SqlParameter sqlParameter = new SqlParameter("roleId", RoleId);
            using(SqlConnection sqlConnection = new SqlConnection(this._connectionString))
            {
                using(SqlCommand cmd = new SqlCommand("dbo.usp_GetModuleData", sqlConnection))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add(sqlParameter);
                    using(DataTable dt = new DataTable())
                    {
                        using(SqlDataAdapter dataAdapter = new SqlDataAdapter())
                        {
                            dataAdapter.SelectCommand = cmd;
                            dataAdapter.Fill(dt);
                        }
                        userModules = JsonConvert.DeserializeObject<List<UserModule>>(JsonConvert.SerializeObject(dt));
                    }
                }
            }

            return userModules;
        }
    }
}
