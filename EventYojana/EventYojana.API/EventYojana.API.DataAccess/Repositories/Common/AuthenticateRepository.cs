﻿using EventYojana.API.DataAccess.DataEntities;
using EventYojana.API.DataAccess.DataEntities.Common;
using EventYojana.API.DataAccess.DataEntities.Vendor;
using EventYojana.API.DataAccess.Interfaces;
using EventYojana.API.DataAccess.Interfaces.Common;
using EventYojana.Infrastructure.Core.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.DataAccess.Repositories.Common
{
    public class AuthenticateRepository : IAuthenticateRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public AuthenticateRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }
        public async Task<UserLogin> GetUserDetails(Expression<Func<UserLogin, bool>> filter)
        {
            return await _databaseContext.Repository<UserLogin>().FirstOrDefaultAsync(filter);
        }

        public async Task<bool> IsUserDetails(Expression<Func<UserLogin, bool>> filter)
        {
            return await _databaseContext.Repository<UserLogin>().ExistsAsync(filter);
        }

        public async Task<bool> RegisterVendor(RegisterVendor registerUserModel)
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("VendorName", registerUserModel.VendorName),
                new SqlParameter("VendorEmail", registerUserModel.VendorEmail),
                new SqlParameter("IsBranch", false),
                new SqlParameter("UserType", registerUserModel.UserType),
                new SqlParameter("Username", registerUserModel.Username),
                new SqlParameter("Password", registerUserModel.Password),
                new SqlParameter("Passwordsalt", registerUserModel.PasswordSalt),
            };

            return await _databaseContext.Repository<Task<bool>>().ExecuteNonQuerySp(StoreProcedureSchemas.usp_RegisterVendor, sqlParameters.ToArray());
        }
    }
}
