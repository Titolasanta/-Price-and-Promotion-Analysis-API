using ProductApi.Data;
using ProductApi.Models;

namespace ProductApi.Endpoints
{
    public static class ProductEndpoints
    {
        public static void MapProductEndpoints(this RouteGroupBuilder group)
        {
            // Map the endpoints for products
            group.MapGet("", GetAllProducts);       // Get all products
            group.MapGet("/{id}", GetProduct);      // Get a single product by id
            group.MapPost("/", CreateProduct);      // Create a new product
            group.MapPut("/{id}", UpdateProduct);   // Update a product
            group.MapDelete("/{id}", DeleteProduct); // Delete a product
        }

        // Get all products with their prices and discounted prices
        static IResult GetAllProducts(ProductDb db)
        {
            var productsWithDiscount = db.Products.Select(p => new
            {
                p.name,
                OriginalPrice = p.price,
                DiscountedPrice = p.price > 50 ? p.price * 0.8m : p.price
            }).ToList();

            return TypedResults.Ok(productsWithDiscount);
        }

        // Get a single product by its id
        static IResult GetProduct(int id, ProductDb db)
        {
            var product = db.Products.FirstOrDefault(p => p.id == id);

            if (product == null)
                return TypedResults.NotFound();

            var productWithDiscount = new
            {
                product.name,
                OriginalPrice = product.price,
                DiscountedPrice = product.price > 50 ? product.price * 0.8m : product.price
            };

            return TypedResults.Ok(productWithDiscount);
        }

        // Create a new product
        static IResult CreateProduct(Product product, ProductDb db)
        {
            db.Products.Add(product);
            return TypedResults.Created($"/product/{product.id}", product);
        }

        // Update an existing product
        static IResult UpdateProduct(int id, Product product, ProductDb db)
        {
            var existingProduct = db.Products.FirstOrDefault(p => p.id == id);

            if (existingProduct == null)
                return TypedResults.NotFound();

            existingProduct.name = product.name;
            existingProduct.price = product.price;

            return TypedResults.NoContent();
        }

        // Delete a product by its id
        static IResult DeleteProduct(int id, ProductDb db)
        {
            var product = db.Products.FirstOrDefault(p => p.id == id);

            if (product == null)
                return TypedResults.NotFound();

            db.Products.Remove(product);
            return TypedResults.NoContent();
        }
    }
}
