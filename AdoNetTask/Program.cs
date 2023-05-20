using System.Data;
using Microsoft.Data.SqlClient;

const string connectionString = @"Server=.;Initial Catalog=Shop;Integrated Security=true;Encrypt=false;";

using var connection = new SqlConnection(connectionString);

connection.Open();

Console.WriteLine();
Console.WriteLine($"Общее количество товаров: {GetProductCount()}");

Console.WriteLine();
Console.Write("Добавить категорию (выполняется транзакцией): ");
AddCategory(Console.ReadLine());

Console.WriteLine();
Console.WriteLine("Категории напитков:");
DisplayAllCategories();

Console.WriteLine();
Console.Write("Добавить категорию (выполняется без транзакции): ");
AddCategoryWithError(Console.ReadLine());

Console.WriteLine();
Console.WriteLine("Категории напитков:");
DisplayAllCategories();

Console.WriteLine();
Console.Write("Добавить товар: ");

var productName = Console.ReadLine();

AddProduct(productName);

Console.Write("\tустановить цену: ");
SetProductPrice(productName, Convert.ToDecimal(Console.ReadLine()));

Console.WriteLine();
Console.WriteLine("Категория товара не указана. Товар будет удалён");
RemoveProduct(productName);

Console.WriteLine();
Console.WriteLine("\t\tНАПИТКИ");
Console.WriteLine();
DisplayAllProducts();

Console.WriteLine();
Console.WriteLine("\t\tНАПИТКИ");
Console.WriteLine();
DisplayAllProductsWithAdapter();

void CheckName(string? name)
{
    if (string.IsNullOrEmpty(name) || name.Trim() == "")
    {
        throw new ArgumentException("Имя недействительно");
    }
}

void CheckPrice(decimal? price)
{
    if (price is null or < 0)
    {
        throw new ArgumentException("Цена недействительна");
    }
}

int GetProductCount()
{
    const string sql = "SELECT COUNT(*) FROM dbo.product";

    using var command = new SqlCommand(sql, connection);

    return (int)command.ExecuteScalar();
}

void AddCategory(string? name)
{
    CheckName(name);

    var transaction = connection?.BeginTransaction();

    try
    {
        const string sql = "INSERT INTO dbo.category(Name) " +
                           "VALUES(@name)";

        using var command = new SqlCommand(sql, connection);

        command.Transaction = transaction;

        command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar)
        {
            Value = name
        });

        command.ExecuteNonQuery();

        throw new Exception();

        transaction?.Commit();

        Console.WriteLine("Категория добавлена");
    }
    catch (Exception e)
    {
        Console.Write("Не удалось добавить категорию: ");
        Console.WriteLine(e.Message);

        transaction?.Rollback();
    }
}

void AddCategoryWithError(string? name)
{
    CheckName(name);

    try
    {
        const string sql = "INSERT INTO dbo.category(Name) " +
                           "VALUES(@name)";

        using var command = new SqlCommand(sql, connection);

        command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar)
        {
            Value = name
        });

        command.ExecuteNonQuery();

        throw new Exception();

        Console.WriteLine("Категория добавлена");
    }
    catch (Exception e)
    {
        Console.Write("Не удалось добавить категорию: ");
        Console.WriteLine(e.Message);
    }
}

void AddProduct(string? name, decimal price = 0M, int categoryId = 0)
{
    CheckName(name);

    CheckPrice(price);

    var transaction = connection?.BeginTransaction();

    try
    {
        const string sql = "INSERT INTO dbo.product(Name, Price, CategoryId) " +
                           "VALUES (@name, @price, @categoryId)";

        using var command = new SqlCommand(sql, connection);

        command.Transaction = transaction;

        command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar)
        {
            Value = name
        });

        command.Parameters.Add(new SqlParameter("@price", SqlDbType.Decimal)
        {
            Value = price
        });

        command.Parameters.Add(new SqlParameter("@categoryId", SqlDbType.Int)
        {
            Value = categoryId
        });

        command.ExecuteNonQuery();

        transaction?.Commit();

        Console.WriteLine("Товар добавлен");
    }
    catch (Exception e)
    {
        Console.Write("Не удалось добавить товар: ");
        Console.WriteLine(e.Message);

        transaction?.Rollback();
    }
}

void SetProductPrice(string? name, decimal price)
{
    CheckName(name);

    CheckPrice(price);

    var transaction = connection?.BeginTransaction();

    try
    {
        const string sql = "UPDATE dbo.product " +
                           "SET Price=@price " +
                           "WHERE Name=@name";

        using var command = new SqlCommand(sql, connection);

        command.Transaction = transaction;

        command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar)
        {
            Value = name
        });

        command.Parameters.Add(new SqlParameter("@price", SqlDbType.Decimal)
        {
            Value = price
        });

        command.ExecuteNonQuery();

        transaction?.Commit();
        Console.WriteLine("Цена изменена");
    }
    catch (Exception e)
    {
        Console.Write("Не удалось установить цену: ");
        Console.WriteLine(e.Message);

        transaction?.Rollback();
    }
}

void RemoveProduct(string? name)
{
    CheckName(name);

    var transaction = connection?.BeginTransaction();

    try
    {
        const string sql = "DELETE FROM dbo.product " +
                           "WHERE Name=@name";

        using var command = new SqlCommand(sql, connection);

        command.Transaction = transaction;

        command.Parameters.Add(new SqlParameter("@name", SqlDbType.NVarChar)
        {
            Value = name
        });

        command.ExecuteNonQuery();

        transaction?.Commit();
        Console.WriteLine("Товар удалён");
    }
    catch (Exception e)
    {
        Console.Write("Не удалось удалить товар: ");
        Console.WriteLine(e.Message);

        transaction?.Rollback();
    }
}

void DisplayAllCategories()
{
    try
    {
        const string sql = "SELECT * FROM dbo.category";

        using var command = new SqlCommand(sql, connection);

        using var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            return;
        }

        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetString("Name")}");
        }
    }
    catch (Exception e)
    {
        Console.Write("Не удалось загрузить данные: ");
        Console.WriteLine(e.Message);
    }
}

void DisplayAllProducts()
{
    try
    {
        const string sql = "SELECT p.Name AS Название, p.Price AS Цена, c.Name AS Категория " +
                           "FROM dbo.product AS p " +
                           "LEFT JOIN dbo.category AS c " +
                           "ON p.CategoryId = c.Id";

        using var command = new SqlCommand(sql, connection);

        using var reader = command.ExecuteReader();

        if (!reader.HasRows)
        {
            return;
        }

        Console.WriteLine($"{reader.GetName(0)} \t{reader.GetName(1)} \t{reader.GetName(2)}");

        Console.WriteLine();

        while (reader.Read())
        {
            Console.WriteLine($"{reader.GetString("Название")} \t{reader.GetDecimal("Цена")} \t{reader.GetString("Категория")}");
        }
    }
    catch (Exception e)
    {
        Console.Write("Не удалось загрузить данные: ");
        Console.WriteLine(e.Message);
    }
}

void DisplayAllProductsWithAdapter()
{
    try
    {
        const string sql = "SELECT p.Name AS Название, p.Price AS Цена, c.Name AS Категория " +
                           "FROM dbo.product AS p " +
                           "LEFT JOIN dbo.category AS c " +
                           "ON p.CategoryId = c.Id";

        using var adapter = new SqlDataAdapter(sql, connection);

        var database = new DataSet();

        adapter.Fill(database);

        foreach (DataTable table in database.Tables)
        {
            foreach (DataColumn column in table.Columns)
            {
                Console.Write($"{column.ColumnName} \t");
            }

            Console.WriteLine();
            Console.WriteLine();

            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine($"{row["Название"]} \t{row["Цена"]} \t{row["Категория"]}");
            }
        }
    }
    catch (Exception e)
    {
        Console.Write("Не удалось загрузить данные: ");
        Console.WriteLine(e.Message);
    }
}