# Sorting Algorithm Visualization

A web application built with ASP.NET Core that demonstrates various sorting algorithms in action through a product filtering system.

## Features

- Interactive product filtering system
- Real-time visualization of 7 different sorting algorithms:
  - Quick Sort
  - Merge Sort
  - Heap Sort
  - Shell Sort
  - Insertion Sort
  - Selection Sort
  - Bubble Sort
- Filter products by:
  - Category
  - Brand
  - Price range
  - Rating
  - Features
  - Availability
- Pagination for better user experience

## Technology Stack

- ASP.NET Core MVC
- C# 
- HTML/CSS/JavaScript
- Bootstrap (assumed based on standard ASP.NET Core templates)

## Getting Started

### Prerequisites

- .NET 6.0 SDK or later
- Visual Studio 2022 or VS Code with C# extensions

### Installation

1. Clone the repository
   ```
   git clone https://github.com/yourusername/algorithm-visualization.git
   ```

2. Navigate to the project directory
   ```
   cd algorithm-visualization
   ```

3. Restore dependencies
   ```
   dotnet restore
   ```

4. Run the application
   ```
   dotnet run
   ```

5. Open your browser and navigate to `https://localhost:5001` or `http://localhost:5000`

## How It Works

The application displays a catalog of products that can be filtered and sorted using different algorithms. When you select a sorting algorithm (quick, merge, heap, etc.), the application uses that algorithm to sort the products based on your chosen criteria (price, rating, name).

This provides a practical demonstration of how different sorting algorithms perform in a real-world application context.

## Project Structure

- **Controllers**: Contains the ProductController that handles user requests
- **Models**: Defines the Product data model
- **Services**: Contains the implementation of product filtering and sorting algorithms
- **Services/Sorting**: Individual sorting algorithm implementations
- **Views**: UI templates for displaying products and algorithm information
- **ViewModel**: Data transfer objects for the views

## Contributing

Contributions are welcome! Feel free to submit pull requests with improvements or additional sorting algorithms.

## License

This project is licensed under the MIT License - see the LICENSE file for details. 
