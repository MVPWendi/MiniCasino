using System.Data.SqlClient;

namespace WUBank.Utils.Database
{
    public static class DatabaseUtils : Object
    {
        private static string ConnectionString = "Server=(localdb)\\MSSQLLocalDB; " +
           "Database=WUBank; User Id=Amlex; " +
           "Password=123456q; Trusted_Connection=True; MultipleActiveResultSets=true";

        public static IEnumerable<Dictionary<string, object>> GetTable(string sqlExpression)
        {
            List<Dictionary<string, object>> Table = new List<Dictionary<string, object>>();

            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(sqlExpression, connection);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        var row = new Dictionary<string, object>();
                        for (int i = 0; i < reader.FieldCount; i++)
                        {
                            row.Add(reader.GetName(i), reader.GetValue(i));
                        }
                        Table.Add(row);
                    }
                }
            }
            return Table;
        }

        private static IEnumerable<Dictionary<string, object>> GetTable(SqlDataReader reader)
        {
            List<Dictionary<string, object>> Table = new List<Dictionary<string, object>>();
            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    var row = new Dictionary<string, object>();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        row.Add(reader.GetName(i), reader.GetValue(i));
                    }
                    Table.Add(row);
                }
            }
            return Table;
        }
        public static IEnumerable<Dictionary<string, object>> ExecuteCrud(string CrudName, Dictionary<string, object> param)
        {
            using (SqlConnection connection = new SqlConnection(ConnectionString))
            {
                connection.Open();
                SqlCommand command = new SqlCommand(CrudName, connection);
                command.CommandType = System.Data.CommandType.StoredProcedure;
                foreach (var par in param)
                {
                    command.Parameters.Add(new SqlParameter
                    {
                        ParameterName = par.Key,
                        Value = par.Value
                    });
                }
                return GetTable(command.ExecuteReader());
            }
        }

    }
}
