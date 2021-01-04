using EventYojana.API.DataAccess.DataEntities.Common;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace EventYojana.API.DataAccess.Interfaces.Common
{
    public interface ILoggingRepository
    {
        Task LogEmailTransaction(EmailLogs emailLogDetails);
    }
}
