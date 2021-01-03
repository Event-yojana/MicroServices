using EventYojana.API.DataAccess.DataEntities.Common;
using EventYojana.API.DataAccess.Interfaces;
using EventYojana.API.DataAccess.Interfaces.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.DataAccess.Repositories.Common
{
    public class LoggingRepository : ILoggingRepository
    {
        private readonly IDatabaseContext _databaseContext;

        public LoggingRepository(IDatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
        }

        public async Task LogEmailTransaction(EmailLogs emailLogDetails)
        {
            await _databaseContext.Repository<EmailLogs>().AddAsync(emailLogDetails);
            _databaseContext.Commit();
        }
    }
}
