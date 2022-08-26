using InventoryManagement;

//Variables
var id = 0;
var productname = "a";
var productprice = 0;
var productinventory = 0;


//Starting point for the program
Console.WriteLine("INVENTORY MANAGEMENT\n" +
    "1 - Add a new product\n" +
    "2 - Remove product\n" +
    "3 - Print product inventory\n" +
    "4 - Edit product name\n" +
    "0 - Exit program");

int initialInput = Convert.ToInt32(Console.ReadLine());

//Switch, checking what to do with a specific input integer
switch (initialInput)
{
    case 0:
        Environment.Exit(0);
        break;
    case 1:
        Console.WriteLine("Please input data in this order to add a new product\n" +
            "Product name");
        productname = Console.ReadLine();

        Console.WriteLine("Product price");
        productprice = Convert.ToInt32(Console.ReadLine());

        Console.WriteLine("Product inventory");
        productinventory = Convert.ToInt32(Console.ReadLine());
        break;
    case 2:
        //RemoveProduct Method
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




if (AddNewProduct(id, productname, productprice, productinventory))
{
    Console.WriteLine("Product " + productname + 
        " has been added with a price of " + productprice + 
        " and inventory of " + productinventory + 
        " residing in slot " + id + 
        ".");
}
else
{
    Console.WriteLine("Incorrect data has been inputted.");
}