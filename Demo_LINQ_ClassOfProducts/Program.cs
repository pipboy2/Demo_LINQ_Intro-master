﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace Demo_LINQ_ClassOfProducts
{


    // demo adapted from MSDN demo
    // https://code.msdn.microsoft.com/SQL-Ordering-Operators-050af19e/sourcecode?fileId=23914&pathId=1978010539
    //
    class Program
    {
        static void Main(string[] args)
        {
            //
            // write all data files to Data folder
            //
            GenerateDataFiles.InitializeXmlFile();

            List<Product> productList = ReadAllProductsFromXml();

            OrderByCatagory(productList);

            OrderByCatagoryAnoymous(productList);

            OrderByUnits(productList);

            OrderByPrice(productList);

            FindExpensive(productList);

            OrderByTotalValue(productList);

            OrderByName(productList);

            //FindExpensive(productList);

            //
            // Write the following methods
            //

            // OrderByUnits(): List the names and units of all products with less than 10 units in stock. Order by units.

            // OrderByPrice(): List all products with a unit price less than $10. Order by price.

            // FindExpensive(): List the most expensive Seafood. Consider there may be more than one.

            // OrderByTotalValue(): List all condiments with total value in stock (UnitPrice * UnitsInStock). Sort by total value.

            // OrderByName(): List all products with names that start with "S" and calculate the average of the units in stock.

            // Query: Student Choice - Minimum of one per team member
        }


        /// <summary>
        /// read all products from an XML file and return as a list of Product
        /// in descending order by price
        /// </summary>
        /// <returns>List of Product</returns>
        private static List<Product> ReadAllProductsFromXml()
        {
            string dataPath = @"Data\Products.xml";
            List<Product> products;

            try
            {
                StreamReader streamReader = new StreamReader(dataPath);
                XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<Product>), new XmlRootAttribute("Products"));

                using (streamReader)
                {
                    products = (List<Product>)xmlSerializer.Deserialize(streamReader);
                }
            }
            catch (Exception)
            {
                throw;
            }

            return products;
        }


        private static void OrderByCatagory(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List all beverages and sort by the unit price.");
            Console.WriteLine();

            //
            // query syntax
            //
            var sortedProducts =
                from product in products
                where product.Category == "Beverages"
                orderby product.UnitPrice descending
                select product;

            //
            // lambda syntax
            //
            //var sortedProducts = products.Where(p => p.Category == "Beverages").OrderByDescending(p => p.UnitPrice);

            Console.WriteLine(TAB + "Category".PadRight(15) + "Product Name".PadRight(25) + "Unit Price".PadLeft(10));
            Console.WriteLine(TAB + "--------".PadRight(15) + "------------".PadRight(25) + "----------".PadLeft(10));

            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(TAB + product.Category.PadRight(15) + product.ProductName.PadRight(25) + product.UnitPrice.ToString("C2").PadLeft(10));
            }

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }

        private static void OrderByCatagoryAnoymous(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List all beverages that cost more the $15 and sort by the unit price.");
            Console.WriteLine();

            //
            // query syntax
            //
            var sortedProducts =
                from product in products
                where product.Category == "Beverages" &&
                    product.UnitPrice > 15
                orderby product.UnitPrice descending
                select new
                {
                    Name = product.ProductName,
                    Price = product.UnitPrice
                };

            //
            // lambda syntax
            //
            //var sortedProducts = products.Where(p => p.Category == "Beverages" && p.UnitPrice > 15).OrderByDescending(p => p.UnitPrice).Select(p => new
            //{
            //    Name = p.ProductName,
            //    Price = p.UnitPrice
            //});


            decimal average = products.Average(p => p.UnitPrice);

            Console.WriteLine(TAB + "Product Name".PadRight(20) + "Product Price".PadLeft(15));
            Console.WriteLine(TAB + "------------".PadRight(20) + "-------------".PadLeft(15));

            foreach (var product in sortedProducts)
            {
                Console.WriteLine(TAB + product.Name.PadRight(20) + product.Price.ToString("C2").PadLeft(15));
            }

            Console.WriteLine();
            Console.WriteLine(TAB + "Average Price:".PadRight(20) + average.ToString().PadLeft(15));

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }
        private static void OrderByUnits(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List all products and sort by the number of units less then 10.");
            Console.WriteLine();

            //
            // query syntax
            //
            var sortedProducts =
                from product in products
                where product.UnitsInStock < 10
                orderby product.UnitsInStock descending
                select product;

            //
            // lambda syntax
            //
            //var sortedProducts = products.Where(p => p.Category == "Beverages").OrderByDescending(p => p.UnitPrice);

            Console.WriteLine(TAB + "Product Name".PadRight(35) + "Total Units");
            Console.WriteLine(TAB + "--------".PadRight(35) + "------------");

            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(TAB + product.ProductName.PadRight(35) + product.UnitsInStock.ToString());
            }

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }


        private static void OrderByPrice(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List all products that are less then $10.");
            Console.WriteLine();

            //
            // query syntax
            //
            var sortedProducts =
                from product in products
                where product.UnitPrice < 10
                orderby product.UnitPrice descending
                select product;

            //
            // lambda syntax
            //
            //var sortedProducts = products.Where(p => p.Category == "Beverages").OrderByDescending(p => p.UnitPrice);

            Console.WriteLine(TAB + "Product Name".PadRight(35) + "Unit Price");
            Console.WriteLine(TAB + "--------".PadRight(35) + "------------");

            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(TAB + product.ProductName.PadRight(35) + product.UnitPrice.ToString("C2"));
            }

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }

        private static void FindExpensive(List<Product> products)
        {
            string TAB = "   ";

            Console.Clear();
            Console.WriteLine(TAB + "List of the most expensive Seafood.");
            Console.WriteLine();


            // query syntax

            var sortedProducts =
            from product in products
            where product.Category == "Seafood"
            group product by product.Category into productGroup
            let maxPrice = productGroup.Max(p => p.UnitPrice)
            select new { Category = productGroup.Key, MostExpensiveProducts = productGroup.Where(product => product.UnitPrice == maxPrice) };

            //  //
            //  // lambda syntax 
            // //
            var sortedProduct = products.Where(p => p.Category == "Beverages").OrderByDescending(p => p.UnitPrice);

            Console.WriteLine(TAB + "Product Name".PadRight(35) + "Unit Price");
            Console.WriteLine(TAB + "--------".PadRight(35) + "------------");

            foreach (Product product in sortedProduct)
            {
                Console.WriteLine(TAB + product.ProductName.PadRight(35) + product.UnitPrice.ToString("C2"));
            }

            Console.WriteLine();
            Console.WriteLine(TAB + "Press any key to continue.");
            Console.ReadKey();
        }

        private static void OrderByTotalValue(List<Product> products)
        {

            var sortedProducts =
                from product in products
                orderby (product.UnitPrice * product.UnitsInStock) descending
                select product;

            string TAB = "\t";
            Console.WriteLine("\n" + TAB + "ID ".PadRight(8) + "Name".PadRight(35) + "Unit Price" + TAB + "Stock" + TAB + TAB + "Total\n");
            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(TAB + product.ProductID.ToString().PadRight(8) + product.ProductName.PadRight(35) + product.UnitPrice.ToString("C2") + TAB + product.UnitsInStock.ToString() + TAB + TAB + (product.UnitPrice * product.UnitsInStock).ToString());

            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();

        }
        private static void OrderByName(List<Product> products)
        {
            var sortedProducts =
                from product in products
                orderby (product.ProductName.StartsWith("S")) descending
                select product;

            string TAB = "\t";
            Console.WriteLine("\n" + TAB + "ID ".PadRight(8) + "Name".PadRight(35) + "Unit Price" + TAB + "Stock\n");
            foreach (Product product in sortedProducts)
            {
                Console.WriteLine(TAB + product.ProductID.ToString().PadRight(8) + product.ProductName.PadRight(35) + product.UnitPrice.ToString("C2") + TAB + product.UnitsInStock.ToString());

            }

            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
        }

    }
}
