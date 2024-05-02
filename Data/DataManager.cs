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

        public bool openConnection()
        {
            conn.Open();
            return true;
        }

        //Register user
        public void RegisterUser(User user)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sqlString = "INSERT INTO Users (firstnamr, lastname, email, password, role) VALUES (@fname, @lname, @email, @pwd, @role)";
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    // Hash the password
                    string hashedPassword = HashPassword(user.Password);
                    cmd.Parameters.Add("@fname", SqlDbType.VarChar, 250).Value = user.Firstname;
                    cmd.Parameters.Add("@lname", SqlDbType.VarChar, 250).Value = user.Lastname;
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 250).Value = user.Email;
                    cmd.Parameters.Add("@pwd", SqlDbType.VarChar, 250).Value = hashedPassword;
                    cmd.Parameters.Add("@role", SqlDbType.VarChar, 50).Value = user.Role;

                    try
                    {
                        cmd.ExecuteNonQuery();
                    }
                    catch (Exception ex)
                    {
                        throw;
                    }
                }
            }
        }

        // Password hashed
        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }


        //User Login
        public User LoginUser(string email, string password)
        {
            using (SqlConnection conn = new SqlConnection(connStr))
            {
                conn.Open();
                string sqlString = "SELECT * FROM Users WHERE email=@email";
                using (SqlCommand cmd = new SqlCommand(sqlString, conn))
                {
                    cmd.Parameters.Add("@email", SqlDbType.VarChar, 250).Value = email;

                    try
                    {
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                // Decrypt the password
                                string encryptedPasswordFromDb = reader["password"].ToString();
                                string decryptedPassword = DecryptPassword(encryptedPasswordFromDb);

                                if (decryptedPassword == password)
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

        private string DecryptPassword(string encryptedPassword)
        {
            // Implement decryption logic using AES or another symmetric encryption algorithm
            // This is just a placeholder and should be replaced with actual decryption code
            return encryptedPassword;
        }

        // Process transaction 
        public bool Cart(Transaction transaction)
        {
            try
            {
                cmd = new SqlCommand("INSERT INTO Transaction(userId, productId, quantity, totalPrice, orderDate) VALUES (@userid, prodIDd @quant,@category,@desc, @donor)", conn);
                cmd.Parameters.Add("@userId", System.Data.SqlDbType.Date).Value = transaction.donationDate;
                cmd.Parameters.Add("@productId", System.Data.SqlDbType.Int).Value = goods.quantity;
                cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar, 250).Value = goods.category;
                cmd.Parameters.Add("@desc", System.Data.SqlDbType.VarChar, 250).Value = goods.description;
                cmd.Parameters.Add("@donor", System.Data.SqlDbType.VarChar, 250).Value = goods.donor;

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
        public int TransactionId { get; set; }
        [ForeignKey("UserId")]
        public User user { get; set; }
        [ForeignKey("ProductId")]
        public Product product { get; set; }
        [Required]
        public int Quantity { get; set; }
        [Required]
        public decimal TotalPrice { get; set; }
        [DataType(DataType.Date)]
        public DateTime OrderDate { get; set; }

        // view purchase history 



        // Create Product
        public bool CreateProduct(Product product)
        {
            try
            {
                cmd = new SqlCommand("INSERT INTO Product(name, price, category, availability) VALUES (@name, @price, @category, @availability)", conn);
                cmd.Parameters.Add("@name", System.Data.SqlDbType.VarChar, 250).Value = product.Name;
                cmd.Parameters.Add("@price", System.Data.SqlDbType.VarChar, 250).Value = product.Price;
                cmd.Parameters.Add("@category", System.Data.SqlDbType.VarChar, 250).Value = product.Category;
                cmd.Parameters.Add("@availability", System.Data.SqlDbType.VarChar, 250).Value = product.Availability;

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
    }
}
