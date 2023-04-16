using ExcelTask;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using OfficeOpenXml.Drawing;

var persons = new List<Person>()
{
    new (30, "Нурлан", "Сабуров", "+7(999)000-00-00"),
    new (33, "Алексей", "Щербаков", "+7(999)000-00-01"),
    new (37, "Ирина", "Мягкова", "+7(999)000-00-02"),
    new (39, "Юлия", "Ахмедова", "+7(999)000-00-03"),
    new (28, "Иван", "Усович", "+7(999)000-00-04"),
    new (29, "Андрей", "Бебуришвили", "+7(999)000-00-05"),
    new (34, "Зоя", "Яровицина", "+7(999)000-00-06"),
    new (39, "Стас", "Старовойтов", "+7(999)000-00-07"),
    new (33, "Ирина", "Приходько", "+7(999)000-00-08"),
    new (52, "Елена", "Новикова", "+7(999)000-00-09")
};

var columnNames = new[] { "Фамилия", "Имя", "Возраст", "Номер телефона" };

ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

using var package = new ExcelPackage();

var worksheet = package.Workbook.Worksheets.Add("Persons");

for (var i = 1; i <= columnNames.Length; i++)
{
    worksheet.Cells[1, i].Value = columnNames[i - 1];
}

for (var i = 0; i < persons.Count; i++)
{
    worksheet.Cells[i + 2, 1].Value = persons[i].Surname;
    worksheet.Cells[i + 2, 2].Value = persons[i].Name;
    worksheet.Cells[i + 2, 3].Value = persons[i].Age;
    worksheet.Cells[i + 2, 4].Value = persons[i].PhoneNumber;
}

using (var tableRange = worksheet.Cells[1, 1, persons.Count + 1, columnNames.Length])
{
    tableRange.Style.Border.Right.Style = ExcelBorderStyle.Thin;
    tableRange.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
    tableRange.Style.Border.BorderAround(ExcelBorderStyle.Medium);

    tableRange.Style.HorizontalAlignment = ExcelHorizontalAlignment.Left;

    tableRange.AutoFitColumns();
}

using (var columnNamesRange = worksheet.Cells[1, 1, 1, columnNames.Length])
{
    columnNamesRange.Style.Fill.SetBackground(eThemeSchemeColor.Background2);
    columnNamesRange.Style.Fill.BackgroundColor.Tint = -0.75M;

    columnNamesRange.Style.Font.Bold = true;
    columnNamesRange.Style.Font.Color.SetColor(eThemeSchemeColor.Accent4);
    columnNamesRange.Style.Font.Color.Tint = 0.8M;

    columnNamesRange.Style.Border.BorderAround(ExcelBorderStyle.Medium);
}

package.SaveAs(new FileInfo(@"c:\temp\ExcelTask.xlsx"));