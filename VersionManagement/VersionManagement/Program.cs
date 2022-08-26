using InventoryManagement;

Start();

static void Start()
{
    //Variables
    int initialInput = 5;

    var id = 0;
    var productname = "a";
    var productprice = 0;
    var productinventory = 0;

    int deletionInput = 5;

    var removeID = 0;
    var removeName = "a";

    var editInput = -1;

    var editID = -1;
    var editName = "a";

    //Starting point for the program
    Console.WriteLine("INVENTORY MANAGEMENT - Input a number for the desider function\n" +
        "1 - Add a new product\n" +
        "2 - Remove product\n" +
        "3 - Print product inventory\n" +
        "4 - Edit product name\n" +
        "0 - Exit program\n" +
        "");

    initialInput = Convert.ToInt32(Console.ReadLine());

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
                "Processing...");
            id = HighestIDCheck();

            AddNewProduct(id, productname, productprice, productinventory);

            Console.WriteLine("Product " + productname +
            " has been added with a price of " + productprice +
            " and inventory of " + productinventory +
            " residing in slot " + id +
            " of the database.");

            Console.WriteLine("\n" +
                "\n" +
                "Do you want to continue or exit the program?\n" +
                "1 - Continue\n" +
                "0 - Exit the program\n" +
                "");
            if (Convert.ToInt32(Console.ReadLine()) == 1)
            {
                Console.WriteLine("\n" +
                    "\n" +
                    "");
                Start();
            }
            else
            {
                Environment.Exit(0);
            }

            break;
        case 2:
            Console.WriteLine("\n" +
                "Product deletion from database, choose to input an ID or Exit the program.\n" +
                "1 - By ID\n" +
                "2 - Go Back\n" +
                "0 - Exit program\n" +
                "");
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
            else if (deletionInput == 2)
            {
                Console.WriteLine("\n" +
                    "\n" +
                    "");
                Start();
            }

            Console.WriteLine("The chosen product has been removed from the database.");

            Console.WriteLine("\n" +
                "\n" +
                "Do you want to continue or exit the program?\n" +
                "1 - Continue\n" +
                "0 - Exit the program\n" +
                "");
            if (Convert.ToInt32(Console.ReadLine()) == 1)
            {
                Console.WriteLine("\n" +
                    "\n" +
                    "");
                Start();
            }
            else
            {
                Environment.Exit(0);
            }

            break;
        case 3:
            QueringProducts();
            break;
        case 4:
            Console.WriteLine("\n" +
                "Choose to enter a product ID, go back or exit the program\n" +
                "1 - Enter ID\n" +
                "2 - Go back\n" +
                "0 - Exit the program\n" +
                "");

            editInput = Convert.ToInt32(Console.ReadLine());

            if (editInput == 1)
            {
                Console.WriteLine("Enter the ID of the product of which you want to change the name of");
                editID = Convert.ToInt32(Console.ReadLine());

                Console.WriteLine("\n" +
                    "Enter the new name");
                editName = Console.ReadLine();

                Console.WriteLine("\n" +
                "\n" +
                "Processing...");

                EditProductName(editID, editName);

                Console.WriteLine("The name of product " + editID + " has been changed to " + editName + ".");
            }
            else if (editInput == 2)
            {
                Console.WriteLine("\n" +
                    "\n" +
                    "");
                Start();
            }
            else if (editInput == 0)
            {
                Environment.Exit(0);
            }

            Console.WriteLine("\n" +
                "\n" +
                "Do you want to continue or exit the program?\n" +
                "1 - Continue\n" +
                "0 - Exit the program\n" +
                "");
            if (Convert.ToInt32(Console.ReadLine()) == 1)
            {
                Console.WriteLine("\n" +
                    "\n" +
                    "");
                Start();
            }
            else
            {
                Environment.Exit(0);
            }



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

static void QueringProducts()
{
    using (VersionManagement versionManagement = new())
    {
        Console.WriteLine("Retrieving list of all current products in database...");

        IQueryable<Product>? products = versionManagement.Products;

        if (products is null)
        {
            Console.WriteLine("No products listed within the database.");

            Console.WriteLine("\n" +
                "\n" +
                "Do you want to continue or exit the program?\n" +
                "1 - Continue\n" +
                "0 - Exit the program\n" +
                "");
            if (Convert.ToInt32(Console.ReadLine()) == 1)
            {
                Console.WriteLine("\n" +
                    "\n" +
                    "");
                Start();
            }
            else
            {
                Environment.Exit(0);
            }
        }

        Console.WriteLine("Products in order of:\n" +
            "ID, Name, Price, Inventory\n" +
            "");

        foreach (Product product in products)
        {
            Console.WriteLine(product.Id + " " + product.ProductName + " " + product.ProductPrice + " " + product.ProductInventory);
        }

        Console.WriteLine("\n" +
                "\n" +
                "Do you want to continue or exit the program?\n" +
                "1 - Continue\n" +
                "0 - Exit the program\n" +
                "");
        if (Convert.ToInt32(Console.ReadLine()) == 1)
        {
            Console.WriteLine("\n" +
                "\n" +
                "");
            Start();
        }
        else
        {
            Environment.Exit(0);
        }
    }
}

static bool EditProductName(int editProductID, string newProductName)
{
    using (VersionManagement versionManagement = new())
    {
        Product editNameProduct = versionManagement.Products.Find(editProductID);

        if (editNameProduct is null)
        {
            return false;
        }
        else
        {
            editNameProduct.ProductName = newProductName;
            int affected = versionManagement.SaveChanges();
            return (affected == 1);
        }
    }
}