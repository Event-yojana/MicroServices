using EventYojana.API.DataAccess.DataEntities.Common;
using EventYojana.API.DataAccess.DataEntities.Vendor;
using EventYojana.API.DataAccess.Interfaces;
using EventYojana.API.DataAccess.Interfaces.Admin;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    }
}
