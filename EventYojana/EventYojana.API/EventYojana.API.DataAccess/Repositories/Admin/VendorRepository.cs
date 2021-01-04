using EventYojana.API.DataAccess.DataEntities.Common;
using EventYojana.API.DataAccess.DataEntities.Vendor;
using EventYojana.API.DataAccess.Interfaces;
using EventYojana.API.DataAccess.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.DataAccess.Repositories.Admin
{
    public class VendorRepository : IVendorRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public VendorRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task<IEnumerable<VendorDetails>> GetRegisteredVendorList()
        {
            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("HaveLogIn", false)
            };

            return await Task.FromResult(_databaseContext.Repository<VendorDetails>().ExecuteSpToType<VendorDetails>(StoreProcedureSchemas.usp_GetVendorsDetails, sqlParameters.ToArray()));
        }

        public async Task<RegisterVendorResponse> ConfirmRegistration(int vendorId, string password, string passwordSalt)
        {
            RegisterVendorResponse registerVendorResponse = new RegisterVendorResponse();

            List<SqlParameter> sqlParameters = new List<SqlParameter>()
            {
                new SqlParameter("VendorId", vendorId),
                new SqlParameter("Password", password),
                new SqlParameter("PasswordSalt", passwordSalt)
            };

            SqlParameter[] outputParameter =
            {
                new SqlParameter("IsUserExists", SqlDbType.Bit),
                new SqlParameter("Success", SqlDbType.Bit)
            };

            var result = _databaseContext.Repository<Task>().ExecuteSp(StoreProcedureSchemas.usp_ConfirmVendorRegistration, sqlParameters.ToArray(), outputParameter);

            registerVendorResponse.IsUserExists = Convert.ToBoolean(result.OutParam[0].Value);
            registerVendorResponse.Success = Convert.ToBoolean(result.OutParam[1].Value);
            registerVendorResponse.Content = 
            result.Data.AsEnumerable().Select(dataRow => new VendorDetails
            {
                VendorId = dataRow.Field<int>("VendorId"),
                VendorName = dataRow.Field<string>("VendorName"),
                VendorEmail = dataRow.Field<string>("VendorEmail"),
                Mobile = dataRow.Field<string>("Mobile"),
                Landline = dataRow.Field<string>("Landline"),
            }).FirstOrDefault();

            return await Task.FromResult(registerVendorResponse);
        }
    }
}
