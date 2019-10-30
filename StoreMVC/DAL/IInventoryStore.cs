using Dapper;
using StoreMVC.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace StoreMVC.DAL
{
    public interface IInventoryStore
    {
        IEnumerable<StoreDALModel> SelectAllProducts();
    }

    public class InventoryStore : IInventoryStore
    {
        private readonly Database _config;

        public InventoryStore(StoreMVCConfiguration config)
        {
            _config = config.Database;
        }

        public IEnumerable<StoreDALModel> SelectAllProducts()
        {
            var sql = @"SELECT * From inventory";

            using (var connection = new SqlConnection(_config.ConnectionString)) //Idisposable
            {
                var result = connection.Query<StoreDALModel>(sql) ?? new List<StoreDALModel>();
                return result;
            }
        }
    }
}
