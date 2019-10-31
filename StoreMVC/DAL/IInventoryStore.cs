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
        bool InsertNewProduct(StoreDALModel dalModel);
        StoreDALModel SelectProductID(int ID);
        bool DeleteProduct(StoreDALModel dalModel);
        bool UpdateProduct(StoreDALModel dalModel);
    }

    public class InventoryStore : IInventoryStore
    {
        private readonly Database _config;

        public InventoryStore(StoreMVCConfiguration config)
        {
            _config = config.Database;
        }

        public bool DeleteProduct(StoreDALModel dalModel)
        {
            var sql = $@"DELETE FROM inventory where ProductID = @ProductID";
            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Execute(sql, dalModel);

                //return true;
                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public bool InsertNewProduct(StoreDALModel dalModel)
        {
            var sql = $@"INSERT INTO inventory (ProductName, Quantity, Price)
                Values (@{nameof(dalModel.ProductName)}, @{nameof(dalModel.Quantity)},@{nameof(dalModel.Price)})";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Execute(sql, dalModel);

                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
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

        public StoreDALModel SelectProductID(int ID)
        {
            var sql = @"Select * From inventory Where ProductID = @ProductID";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.QueryFirstOrDefault<StoreDALModel>(sql, new { ProductID = ID });

                return result;
            }
        }

        public bool UpdateProduct(StoreDALModel dalModel)
        {

            var sql = @"UPDATE inventory SET ProductName = @ProductName,  
            Quantity = @Quantity, Price = @Price
            WHERE ProductID = @ProductID";

            using (var connection = new SqlConnection(_config.ConnectionString))
            {
                var result = connection.Execute(sql, dalModel);

                if (result == 1)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }
    }
}
