using Microsoft.Data.Sqlite;
using System.Data;

/// <summary>
/// Класс OrderRepository представляет репозиторий для работы с заказами.
/// Этот класс предоставляет методы для сохранения, извлечения и управления заказами в базе данных.
/// 
/// В контексте паттерна "Агрегатор", данный класс служит для интеграции бизнес-логики приложения
/// с базой данных, где сохраняются и извлекаются данные о заказах.
/// </summary>
namespace org.example.agregator
{


    public class OrderRepository
    {

        // Соединение с базой данных
        private SqliteConnection connection;

        /// <summary>
        /// Конструктор, инициализирующий базу данных.
        /// </summary>
        /// <param name="databaseUrl"> URL базы данных для подключения </param>
        public OrderRepository(string databaseUrl)
        {
            connection = new SqliteConnection(databaseUrl);
            initDatabase();
        }

        /// <summary>
        /// Инициализация таблиц в базе данных (если они не существуют).
        /// </summary>
        private void initDatabase()
        {
            try
            {
                // Создание таблицы продуктов, если она еще не создана
                // Создание таблицы заказов, если она еще не создана
                // Создание таблицы элементов заказа, если она еще не создана
                String sql = "CREATE TABLE IF NOT EXISTS products (id INTEGER PRIMARY KEY, name TEXT, price REAL);" +
                    "CREATE TABLE IF NOT EXISTS orders (id INTEGER PRIMARY KEY AUTOINCREMENT, totalAmount REAL);" +
                    "CREATE TABLE IF NOT EXISTS order_items(orderId INTEGER, productId INTEGER, quantity INTEGER);";
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

        /// <summary>
        /// Сохранение информации о заказе в базе данных.
        /// </summary>
        /// <param name="order"> Заказ, который необходимо сохранить </param>
        public virtual void save(Order order)
        {
            SqliteCommand cmd; String sql;

            try
            {
                connection.Open();

                // Вставка данных о заказе в таблицу заказов
                cmd = connection.CreateCommand();
                cmd.CommandText = "INSERT INTO orders(totalAmount) VALUES (@Price); SELECT last_insert_rowid();";
                cmd.Parameters.AddWithValue("@Price", order.TotalPrice);

                long orderId = (long)cmd.ExecuteScalar();

                // Вставка данных о элементах заказа в таблицу элементов заказа
                foreach (OrderItem item in order.Items)
                {
                    cmd.CommandText = "INSERT INTO order_items(orderId, productId, quantity) VALUES (@orderId, @productid, @quantity)";
                    cmd.Parameters.AddWithValue("@orderId", orderId);
                    cmd.Parameters.AddWithValue("@productid", item.Product.Id);
                    cmd.Parameters.AddWithValue("@quantity", item.Quantity);
                    cmd.ExecuteNonQuery();
                }

                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        /// <summary>
        /// Извлечение последнего заказа из базы данных.
        /// </summary>
        /// <returns> Последний сохраненный заказ или null, если заказов нет </returns>
        public virtual Order LastOrder
        {
            get
            {
                String sql = "SELECT * FROM orders ORDER BY id DESC LIMIT 1";

                try
                {
                    connection.Open();

                    SqliteCommand cmd = connection.CreateCommand();
                    cmd.CommandText = sql;

                    IDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                        int orderId = Convert.ToInt32(reader["id"]);
                        Order order = new Order();
                        order.Items = getOrderItemsByOrderId(orderId);

                        return order;
                    }
                    reader.Close();
                    connection.Close();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                }

                return null;
            }
        }

        /// <summary>
        /// Извлечение заказа из базы данных по его идентификатору.
        /// </summary>
        /// <param name="id"> Идентификатор заказа </param>
        /// <returns> Заказ с указанным идентификатором или null, если такого заказа нет </returns>
        public virtual Order getById(int id)
        {
            Order order = new Order();
            order.Items = getOrderItemsByOrderId(id);
            return order;
        }

        /// <summary>
        /// Вспомогательный метод для извлечения элементов заказа по идентификатору заказа.
        /// </summary>
        /// <param name="orderId"> Идентификатор заказа </param>
        /// <returns> Список элементов указанного заказа </returns>
        private IList<OrderItem> getOrderItemsByOrderId(int orderId)
        {
            IList<OrderItem> orderItems = new List<OrderItem>();

            String sql = "SELECT * FROM order_items WHERE orderId=" + Convert.ToString(orderId);

            try
            {
                connection.Open();

                SqliteCommand cmd = connection.CreateCommand();
                cmd.CommandText = sql;

                IDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    long productId = (long)reader["productId"];
                    long quantity = (long)reader["quantity"];
                    Product product = new Product(productId, "Product " + productId, 10.0);
                    orderItems.Add(new OrderItem(product, quantity));
                }
                reader.Close();
                connection.Close();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }


            return orderItems;
        }

        // ... другие методы для работы с БД, например, для получения списка всех заказов или удаления
    }
}
