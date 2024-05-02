using Microsoft.Data.SqlClient;
using KhumaloCraft.Model;
using System.Data;
using System.Text;
using System.Security.Cryptography;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace KhumaloCraft.Data
{
    public class DataManager
    {
        static string connStr = "Server=tcp:st10115884-sql-server.database.windows.net,1433;Initial Catalog=KhumaloCraft;Persist Security Info=False;User ID=st10115884;Password=Mashudu@1;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";
        private static SqlConnection conn = new SqlConnection(connStr);
        private static SqlCommand cmd;

        public DataManager() { }

        //public bool openConnection()
        //{
        //    conn.Open();
        //    return true;
        //}

        //public bool closeConnection()
        //{
        //    conn.Close(); return true;
        //}

        public User LoginUser(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sqlString = "SELECT * FROM User WHERE email=@email";
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 250).Value = email;

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                string dbPassword = reader["password"].ToString();
                                if (dbPassword == password)
                                {
                                    return new User
                                    {
                                        Firstname = reader["firstname"].ToString(),
                                        Lastname = reader["lastname"].ToString(),
                                        Email = reader["email"].ToString(),
                                        Role = reader["role"].ToString()
                                    };
                                }
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
            return null;
        }
        public bool Cart(Transaction transaction)
        {
            try
            {
                cmd = new SqlCommand("INSERT INTO Transaction(user, productId, quantity, totalPrice, orderDate) VALUES (@userid, @prodId, @quant,@totprice,@date)", conn);
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int).Value = transaction.user;
                cmd.Parameters.Add("@productId", System.Data.SqlDbType.Int).Value = transaction.product;
                cmd.Parameters.Add("@quant", System.Data.SqlDbType.Int).Value = transaction.Quantity;
                cmd.Parameters.Add("@totprice", System.Data.SqlDbType.Decimal).Value = transaction.TotalPrice;
                cmd.Parameters.Add("@date", System.Data.SqlDbType.Date).Value = transaction.OrderDate;

                int res = cmd.ExecuteNonQuery();
                conn.Close();

                return res == 1 ? true : false;
            }
            catch (Exception)
            {
                conn.Close();

                throw;
            }

        }


        // view purchase history 
        public List<Transaction> GetUserTransactionHistory(int userId)
        {
            List<Transaction> transactions = new List<Transaction>();

            try
            {
                cmd = new SqlCommand("SELECT * FROM TransactionHistory WHERE UserID = @userId", conn);
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.Int).Value = userId;

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Transaction transaction = new Transaction
                        {
                            TransactionId = (int)reader["TransactionId"],
                            user = new User { UserId = (int)reader["UserID"] },
                            product = new Product { ProductId = (int)reader["ProductID"] }, 
                            Quantity = (int)reader["Quantity"],
                            TotalPrice = (decimal)reader["TotalPrice"],
                            OrderDate = (DateTime)reader["OrderDate"]
                        };
                        transactions.Add(transaction);
                    }
                }

                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }

            return transactions;
        }

        // Create Product
        public List<Product> GetAllProducts()
        {
            List<Product> products = new List<Product>();

            try
            {
                cmd = new SqlCommand("SELECT * FROM Product", conn);

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Product product = new Product
                        {
                            ProductId = (int)reader["ProductId"],
                            Name = reader["Name"].ToString(),
                            Price = (int)reader["Price"],
                            Category = reader["Category"].ToString(),
                            Availability = reader["Availability"].ToString()
                        };
                        products.Add(product);
                    }
                }

                conn.Close();
            }
            catch (Exception)
            {
                conn.Close();
                throw;
            }

            return products;
        }

    }
}
