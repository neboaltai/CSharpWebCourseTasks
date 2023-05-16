using LogTask;
using NLog;

LogManager.Setup().LoadConfiguration(builder =>
{
    builder.ForLogger().FilterMinLevel(LogLevel.Debug).WriteToConsole();
    builder.ForLogger().FilterMinLevel(LogLevel.Info).WriteToFile(fileName: "../../../logs.txt");
});

var logger = LogManager.GetCurrentClassLogger();

var swordDamage = new SwordDamage();

var random = new Random();

var rules = "";

try
{
    using var reader = new StreamReader("../../../rules.txt");

    rules = reader.ReadToEnd();
}
catch (IOException e)
{
    logger.Error(e);
}

while (true)
{
    Console.Write(rules);

    var key = Console.ReadKey(false).KeyChar;

    Console.WriteLine();

    if (key is not '0' and not '1' and not '2' and not '3')
    {
        break;
    }

    swordDamage.Roll = RollDice(random);

    swordDamage.IsMagic = key is '1' or '3';

    swordDamage.IsFlaming = key is '2' or '3';

    Console.WriteLine();
    Console.WriteLine($"Rolled {swordDamage.Roll} for {swordDamage.Damage} HP");

    Console.WriteLine();
}

static int RollDice(Random random)
{
    return random.Next(1, 7) + random.Next(1, 7) + random.Next(1, 7);
}