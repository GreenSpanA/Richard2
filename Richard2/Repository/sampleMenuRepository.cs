using System.Collections.Generic;
using System.Linq;
using Microsoft.Extensions.Configuration;
using Dapper;
using System.Data;
using Npgsql;
using Richard2.Models;


namespace Richard2.Repository
{
    public class sampleMenuRepository : IRepository<sampleMenu>
    {
        private string connectionString;

        public sampleMenuRepository(IConfiguration configuration)
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

        public void Add(sampleMenu item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("INSERT INTO samplemenu (category, dish, description," +
                    "veg_comment, price, file_name) VALUES(@Category, @Dish, @Description, " +
                    "@Veg_Comment, @Price, @File_Name)", item);
            }
        }

        public IEnumerable<sampleMenu> FindAll()
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<sampleMenu>("SELECT * FROM samplemenu WHERE id > 0");
            }
        }

        //TODO
        public IEnumerable<sampleMenu> FindCurrent(string file_name)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<sampleMenu>("SELECT FROM samplemenu WHERE file_name=@File_Name", new { File_Name = file_name });
            }
        }


        public sampleMenu FindByID(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<sampleMenu>("SELECT * FROM samplemenu WHERE id = @Id", new { Id = id }).FirstOrDefault();
            }
        }

        public sampleMenu FindByFile(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                return dbConnection.Query<sampleMenu>("SELECT file_name FROM samplemenu WHERE id = @Id", new { Id = 0 }).FirstOrDefault();
            }
        }



        public void Remove(int id)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Execute("DELETE FROM samplemenu WHERE Id=@Id", new { Id = id });
            }
        }

        public void Update(sampleMenu item)
        {
            using (IDbConnection dbConnection = Connection)
            {
                dbConnection.Open();
                dbConnection.Query("UPDATE samplemenu SET category = @Category,  dish  = @Dish, description = @Description, " +
                    "veg_comment = @Veg_Comment, price = @Price, file_name = @File_Name WHERE id = @Id", item);
            }
        }        
        
    }
}
