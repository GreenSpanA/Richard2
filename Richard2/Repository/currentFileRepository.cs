using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using Richard2.Models;


namespace Richard2.Repository
{
    public class currentFileRepository : IRepository_file<currentFile>
    {
        private string connectionString;

        public currentFileRepository(IConfiguration configuration)
        {
            connectionString = configuration.GetValue<string>("DBInfo:ConnectionString");
        }

        internal IDbConnection Connection
        {
            get
            {
                return new NpgsqlConnection(connectionString);
            }
        }        

        public IEnumerable<currentFile> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<currentFile>("SELECT * FROM current_file");
            }
        }    


        public currentFile FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<currentFile>("SELECT * FROM current_file WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }      

        public void Update(currentFile item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE current_file SET file_name = @File_Name WHERE id = @Id", item);
            }
        }

    }
}
