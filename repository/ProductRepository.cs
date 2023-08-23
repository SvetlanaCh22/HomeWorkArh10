using System.Collections.Generic;

namespace org.example.repository
{
    public interface ProductRepository
    {
        Product getById(int id);
        IList<Product> All { get; }
        void add(Product product);
        void update(Product product);
        void delete(int id);

        void createTable();
    }
}
