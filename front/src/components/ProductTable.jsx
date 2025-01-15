import React, { useState } from 'react';
import useProducts from '../hooks/useProducts';
import '../styles/ProductTable.css'; // Import the CSS file

function ProductTable() {
  // Use the custom hook to fetch data
  const { products, loading, error } = useProducts();


  // State to manage the sorting order and which price to sort by
  const [sortOrder, setSortOrder] = useState('asc'); // ascending by default
  const [sortField, setSortField] = useState('originalPrice'); // default to originalPrice
  
  // Handle loading and error states
  if (loading) {
    return <p className="loading">Loading...</p>;
  }

  if (error) {
    return <p className="error">Error fetching products: {error}</p>;
  }

  // Sorting function
  const sortProducts = (a, b) => {
    const priceA = a[sortField];
    const priceB = b[sortField];

    if (sortOrder === 'asc') {
      return priceA - priceB;
    } else {
      return priceB - priceA;
    }
  };

  // Sort the products based on selected field and order
  const sortedProducts = [...products].sort(sortProducts);

  // Toggle the sorting order (ascending/descending)
  const toggleSortOrder = () => {
    setSortOrder(sortOrder === 'asc' ? 'desc' : 'asc');
  };

  // Toggle the sorting field (originalPrice/discountedPrice)
  const toggleSortField = () => {
    setSortField(sortField === 'originalPrice' ? 'discountedPrice' : 'originalPrice');
  };

  return (
    <div>
      <h1>Product List</h1>
      
      <div>
        <button onClick={toggleSortField}>
          Sort by {sortField === 'originalPrice' ? 'Discounted Price' : 'Original Price'}
        </button>
        <button onClick={toggleSortOrder}>
          {sortOrder === 'asc' ? 'Sort Descending' : 'Sort Ascending'}
        </button>
      </div>

      <table>
        <thead>
          <tr>
            <th>Product Name</th>
            <th>Original Price</th>
            <th>Discounted Price</th>
          </tr>
        </thead>
        <tbody>
          {sortedProducts.map((product) => {
            const isDiscounted = product.discountedPrice < product.originalPrice; // Condition for discount
            return (
              <tr key={product.id} className={isDiscounted ? 'discounted' : ''}>
                <td className={isDiscounted ? 'product-name' : ''}>{product.name}</td>
                <td>${product.originalPrice}</td>
                <td className={isDiscounted ? '' : 'no-discount'}>
                  {isDiscounted ? `$${product.discountedPrice}` : 'No Discount'}
                </td>
              </tr>
            );
          })}
        </tbody>
      </table>
    </div>
  );
}

export default ProductTable;
