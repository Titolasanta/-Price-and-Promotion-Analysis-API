using System.IO;
using System.Text.Json;
using ProductApi.Models;

namespace ProductApi.Data
{
    public class ProductDb
    {
        private const string FilePath = "./products.json";  // Path to the JSON file
        public List<Product> Products { get; set; }

        public ProductDb()
        {
            Products = LoadProductsFromFile();
        }

        // Load products from the JSON file
        private List<Product> LoadProductsFromFile()
        {
            if (!File.Exists(FilePath))
            {
                return new List<Product>(); // Return an empty list if the file does not exist
            }

            var jsonData = File.ReadAllText(FilePath);
            return JsonSerializer.Deserialize<List<Product>>(jsonData) ?? new List<Product>();
        }

        // Save products to the JSON file
        public void SaveProductsToFile()
        {
            var jsonData = JsonSerializer.Serialize(Products, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(FilePath, jsonData);
        }

        // Add a product and save to the file
        public void AddProduct(Product product)
        {
            Products.Add(product);
            SaveProductsToFile();
        }

        // Update an existing product and save to the file
        public void UpdateProduct(Product updatedProduct)
        {
            var product = Products.FirstOrDefault(p => p.id == updatedProduct.id);
            if (product != null)
            {
                product.name = updatedProduct.name;
                product.price = updatedProduct.price;
                SaveProductsToFile();
            }
        }

        // Delete a product and save the changes to the file
        public void DeleteProduct(int id)
        {
            var product = Products.FirstOrDefault(p => p.id == id);
            if (product != null)
            {
                Products.Remove(product);
                SaveProductsToFile();
            }
        }
    }
}
