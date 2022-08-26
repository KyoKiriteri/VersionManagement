﻿using InventoryManagement;

Start();

static void Start()
{
    //Variables
    var id = 0;
    var productname = "a";
    var productprice = 0;
    var productinventory = 0;

    int deletionInput = 5;

    var removeID = 0;
    var removeName = "a";

    //Starting point for the program
    Console.WriteLine("INVENTORY MANAGEMENT - Input a number for the desider function\n" +
        "1 - Add a new product\n" +
        "2 - Remove product\n" +
        "3 - Print product inventory\n" +
        "4 - Edit product name\n" +
        "0 - Exit program\n" +
        "");

    int initialInput = Convert.ToInt32(Console.ReadLine());

    //Switch, checking what to do with a specific input integer
    switch (initialInput)
    {
        case 0:
            Environment.Exit(0);
            break;
        case 1:
            Console.WriteLine("\n" +
                "Please input data in this order to add a new product\n" +
                "Product name:");
            productname = Console.ReadLine();

            Console.WriteLine("\n" +
                "Product price:");
            productprice = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\n" +
                "Product inventory:");
            productinventory = Convert.ToInt32(Console.ReadLine());

            Console.WriteLine("\n" +
                "\n" +
                "Processing...");
            id = HighestIDCheck();

            AddNewProduct(id, productname, productprice, productinventory);

            Console.WriteLine("Product " + productname +
            " has been added with a price of " + productprice +
            " and inventory of " + productinventory +
            " residing in slot " + id +
            " of the database.");
            break;
        case 2:
            Console.WriteLine("Product deletion from database, choose to input an ID or Exit the program.\n" +
                "1 - By ID\n" +
                "0 - Exit program");
            deletionInput = Convert.ToInt32(Console.ReadLine());

            if (deletionInput == 0)
            {
                Environment.Exit(0);
            }
            else if (deletionInput == 1)
            {
                Console.WriteLine("Please input the product ID you wish to remove from the database.");
                removeID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\n" +
                "\n" +
                "Processing...");

                RemoveProductID(removeID);
            }

            Console.WriteLine("The chosen product has been removed from the database.");

            break;
        case 3:

            //PrintProductInventory Method
            break;
        case 4:
            //EditProfuctName Method
            break;
        default:
            break;
    }

}




//Method 1, AddProduct according to the user's inputs
static bool AddNewProduct(int newId, string newProductName, int newProductPrice, int newProductInventory)
{
    using (VersionManagement versionManagement = new())
    {
        Product product = new()
        {
            Id = newId,
            ProductName = newProductName,
            ProductPrice = newProductPrice,
            ProductInventory = newProductInventory
        };

        versionManagement.Products?.Add(product);

        int affected = versionManagement.SaveChanges();
        return (affected == 1);
    }
}

static int HighestIDCheck()
{
    int currentHighestID = 0;

    using (VersionManagement versionManagement = new())
    {
        IQueryable<Product>? products = versionManagement.Products;

        if (products is null)
        {
            Console.WriteLine("No products on record.");
            return 0;
        }

        foreach (Product product in products)
        {
            if (product.Id == currentHighestID + 1)
            {
                currentHighestID = product.Id;
            }
        }
        currentHighestID++;
    }
    return currentHighestID;
}

static bool RemoveProductID(int removeProductID)
{
    using (VersionManagement versionManagement = new())
    {
        Product removeIDproduct = versionManagement.Products.Find(removeProductID);
        
        if (removeIDproduct is null)
        {
            return false;
        }
        else
        {
            versionManagement.Products?.Remove(removeIDproduct);
        }

        int affected = versionManagement.SaveChanges();
        return (affected == 1);
    }
}