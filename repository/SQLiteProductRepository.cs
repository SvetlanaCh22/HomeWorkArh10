using Microsoft.Data.Sqlite;
using System.Data;

namespace org.example.repository
{
	public class SQLiteProductRepository : ProductRepository
	{
		private SqliteConnection connection;

		public SQLiteProductRepository(string connectionString)
		{
			try
			{
				connection = new SqliteConnection(connectionString);
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

		public virtual void createTable()
		{
			try
			{
				String sql = "CREATE TABLE IF NOT EXISTS products (id INTEGER PRIMARY KEY,name TEXT,price REAL)";
                connection.Open();
                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
		public virtual Product getById(int id)
		{
			Product product = null;

			try
			{
                String sql = "SELECT * FROM products WHERE id = " + Convert.ToString(id);

                connection.Open();

                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                IDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
				{
					int productId = (int)reader["id"];
					String name = (String)reader["name"];
					double price = (double)reader["price"];
					product = new Product(productId, name, price);
				}

                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return product;
		}

		public virtual IList<Product> All
		{
			get
			{
				IList<Product> products = new List<Product>();
    
				try
				{
                    String sql = "SELECT * FROM products";

                    connection.Open();

                    SqliteCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;

                    IDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
					{
                        int id = (int)reader["id"];
                        String name = (String)reader["name"];
                        double price = (double)reader["price"];
                        Product product = new Product(id, name, price);
						products.Add(product);
					}

                    reader.Close();
                    connection.Close();
				}
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                return products;
			}
		}

		public virtual void add(Product product)
		{
			try
			{
                String sql = "INSERT INTO products (name, price) VALUES (" + product.Name + ", " + Convert.ToString(product.Price);
                connection.Open();
                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

		public virtual void update(Product product)
		{
			try
			{
                String sql = "UPDATE products SET name = " + product.Name + ", price = " + Convert.ToString(product.Price) + " WHERE id = " + Convert.ToString(product.Id);
                connection.Open();
                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

		public virtual void delete(int id)
		{
			try
			{
                String sql = "DELETE FROM products WHERE id = " + Convert.ToString(id);
                connection.Open();
                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }
	}

}
