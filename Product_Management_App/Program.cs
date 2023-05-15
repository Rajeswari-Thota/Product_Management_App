using System.Data.SqlClient;
using Spectre.Console;
namespace Product_Management_App
{
    class Product
    {
        SqlConnection con = new SqlConnection("server=IN-8JRQ8S3;database=mydb;Integrated Security=true");
        public static void Addproduct(SqlConnection con)
        {         
            var pname = AnsiConsole.Ask<string>("Enter Product Name: ");
            var pdesc = AnsiConsole.Ask<string>("Enter Product Description: ");
            var pquantity= AnsiConsole.Ask<int>("Enter Quantity: ");
            var pprice= AnsiConsole.Ask<decimal>("Enter Price: ");
            SqlCommand cmd = new SqlCommand("insert into Product_Management values(@Product_Name,@Product_Description,@Quantity,@Price)", con);
            cmd.Parameters.AddWithValue("@Product_Name", pname);
            cmd.Parameters.AddWithValue("@Product_Description", pdesc);
            cmd.Parameters.AddWithValue("@Quantity",pquantity );
            cmd.Parameters.AddWithValue("@Price",pprice);
            cmd.ExecuteNonQuery();
            Console.WriteLine("Product Added Successfully");
        }
        public static void Viewproduct(SqlConnection con)
        {

            var id = AnsiConsole.Ask<int>("Enter an Id to view product: ");
            SqlCommand cmd = new SqlCommand($"select*from Product_Management where Product_Id={id}", con);
            SqlDataReader dr = cmd.ExecuteReader();
            var table = new Table();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                table.AddColumn(dr.GetName(i));
            }
            string[] arr = new string[dr.FieldCount];
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    arr[i] = dr.GetValue(i).ToString();

                }
                table.AddRow(arr);
            }
            AnsiConsole.Write(table);
            dr.Close();
        }
        public static void Viewallproducts(SqlConnection con)
        {

            SqlCommand cmd = new SqlCommand($"select*from Product_Management", con);
            SqlDataReader dr = cmd.ExecuteReader();
            var table = new Table();
            for (int i = 0; i < dr.FieldCount; i++)
            {
                table.AddColumn(dr.GetName(i));
            }
            string[] arr = new string[dr.FieldCount];
            while (dr.Read())
            {
                for (int i = 0; i < dr.FieldCount; i++)
                {
                    arr[i] = dr.GetValue(i).ToString();

                }
                table.AddRow(arr);

            }
            AnsiConsole.Write(table);
            dr.Close();
        }
        public static void Updateproduct(SqlConnection con)
        {
            var id = AnsiConsole.Ask<int>("Enter Id: ");
            SqlCommand cmd = new SqlCommand($"update Product_Management set Product_Name=@Product_Name,Product_Description=@Product_Description,Quantity=@Quantity,Price=@Price where Product_Id={id}", con);
            var pname = AnsiConsole.Ask<string>("Enter Product Name: ");
            var pdesc = AnsiConsole.Ask<string>("Enter Product Description: ");
            var pquantity = AnsiConsole.Ask<int>("Enter Quantity: ");
            var pprice = AnsiConsole.Ask<decimal>("Enter Price: ");
            cmd.Parameters.AddWithValue("@Product_Name", pname);
            cmd.Parameters.AddWithValue("@Product_Description", pdesc);
            cmd.Parameters.AddWithValue("@Quantity", pquantity);
            cmd.Parameters.AddWithValue("@Price", pprice);
            int rowseffected=cmd.ExecuteNonQuery();
            if(rowseffected > 0)
            {
                Console.WriteLine("Product Updated Successfully");
            }
            else
            {
                Console.WriteLine("Product not found");
            }
            
        }
        public static void DeleteProduct(SqlConnection con)
        {
            var id = AnsiConsole.Ask<int>("Enter Id: ");
            SqlCommand cmd = new SqlCommand($"Delete from Product_Management where Product_Id={id} ", con);
            int rowseffected = cmd.ExecuteNonQuery();
            if (rowseffected > 0)
            {
                Console.WriteLine("Product deleted Successfully");
            }
            else 
            { 
                Console.WriteLine("Product not found");
            }
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            SqlConnection con = new SqlConnection("server=IN-8JRQ8S3; database=mydb;Integrated Security=true");
            con.Open();
            var a = "";
            do
            {
                AnsiConsole.MarkupLine("WELCOME TO [bold yellow]PRODUCT-MANAGEMENT-APP [/]");
                var choice = AnsiConsole.Prompt(new SelectionPrompt<string>()
                        .Title("[green] Select your option [/]?")
                        .AddChoices(new[] { "Add Product", "View Product", "View All Products", "Update Product", "Delete Product" }));
                switch (choice)
                {
                    case "Add Product":
                        {
                            Product.Addproduct(con);
                            break;

                        }
                    case "View Product":
                        {
                            Product.Viewproduct(con);
                            break;

                        }
                    case "View All Products":
                        {
                            Product.Viewallproducts(con);
                            break;

                        }
                    case "Update Product":
                        {
                            Product.Updateproduct(con);
                            break;

                        }
                    case "Delete Product":
                        {
                            Product.DeleteProduct(con);
                            break;

                        }
                }
                a = AnsiConsole.Ask<string>("Do wish to continue(y/n): ");
            } while (a.ToLower() == "y");
            con.Close();
        }
    }
}