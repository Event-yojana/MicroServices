using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace EventYojana.Infrastructure.Repository
{
    public class SpOut
    {
        public DataTable Data { get; set; }
        public SqlParameter[] OutParam { get; set; }
    }

    public class SpOutDs: SpOut
    {
        public DataSet DS { get; set; }
    }

    public class BoolValue 
    {
        public bool value { get; set; }
    }
}
