namespace org.example.cacheaside
{

    /// <summary>
    /// Пример реализации паттерна Кэширование Cache-Aside с использованием коллекции продуктов.
    /// </summary>
    public class CacheAsideExample
    {
        public static void Main(string[] args)
        {
            // Создание списка продуктов
            IList<Product> productList = createProductList();

            // Создание кэша для продуктов
            ProductCache productCache = new ProductCache();

            // Запрос продукта по ID
            int productId = 1;
            Product product = productCache.getProduct(productId, productList);
            Console.WriteLine("Product from cache or list: " + product);

            // Обновление цены продукта
            product.Price = 15.99;
            productCache.updateProduct(product, productList);

            // Запрос обновленного продукта по тому же ID
            product = productCache.getProduct(productId, productList);
            Console.WriteLine("Updated product from cache or list: " + product);
        }

        // Создание списка продуктов для примера
        private static IList<Product> createProductList()
        {
            IList<Product> productList = new List<Product>();
            productList.Add(new Product(1, "Product 1", 9.99));
            productList.Add(new Product(2, "Product 2", 19.99));
            productList.Add(new Product(3, "Product 3", 29.99));
            return productList;
        }
    }

    internal class Product
    {
        private int id;
        private string name;
        private double price;

        public Product(int id, string name, double price)
        {
            this.id = id;
            this.name = name;
            this.price = price;
        }

        public virtual int Id
        {
            get
            {
                return id;
            }
        }

        public virtual string Name
        {
            get
            {
                return name;
            }
        }

        public virtual double Price
        {
            get
            {
                return price;
            }
            set
            {
                this.price = value;
            }
        }


        public override string ToString()
        {
            return "Product{" + "id=" + id + ", name='" + name + '\'' + ", price=" + price + '}';
        }
    }

    internal class ProductCache
    {
        private IDictionary<int, Product> cache = new Dictionary<int, Product>();

        /// <summary>
        /// Получить продукт из кэша или основного списка.
        /// </summary>
        /// <param name="id">           ID продукта. </param>
        /// <param name="productList"> Основной список продуктов. </param>
        /// <returns> Продукт из кэша или null, если продукт не найден. </returns>
        public virtual Product getProduct(int id, IList<Product> productList)
        {
            if (cache.ContainsKey(id))
            {
                return cache[id];
            }
            else
            {
                foreach (Product product in productList)
                {
                    if (product.Id == id)
                    {
                        cache[id] = product;
                        return product;
                    }
                }
                return null;
            }
        }

        /// <summary>
        /// Обновить информацию о продукте в кэше и основном списке.
        /// </summary>
        /// <param name="product">      Обновленная информация о продукте. </param>
        /// <param name="productList"> Основной список продуктов. </param>
        public virtual void updateProduct(Product product, IList<Product> productList)
        {
            cache[product.Id] = product;
            for (int i = 0; i < productList.Count; i++)
            {
                if (productList[i].Id == product.Id)
                {
                    productList[i] = product;
                    break;
                }
            }
        }
    }
}
