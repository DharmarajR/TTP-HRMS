using System.Data.Odbc;
using System.Data.SqlClient;

namespace TTP_HRMS.Models
{
    public class DBVariables
    {
        public SqlConnection Sqlcon { get; set; }
        public SqlCommand Sqlcmd { get; set; }
        public SqlDataAdapter Sqlda { get; set; }
        public SqlDataReader SqlReader { get; set; }
        public OdbcConnection Odbccon { get; set; }
        public OdbcCommand Odbccmd { get; set; }
        public OdbcDataAdapter Odbcda { get; set; }
        public OdbcDataReader Odbcreader { get; set; }
    }
}