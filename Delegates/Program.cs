// Task:
// 1. Add multiplier and divider method to the Calculator
// 2. Refactor Calculator to use delegates


using Delegates2._0;

Console.WriteLine("enter calculation to perform (options: [Add|Minus]): ");
var userInput = Console.ReadLine();
Enum.TryParse(userInput, out CalculationType calculationType);

Console.WriteLine("enter first value: ");
var variableOne = Console.ReadLine();
Console.WriteLine("enter second value: ");
var variableTwo = Console.ReadLine();

var result = Calculator.Calculate(int.Parse(variableOne), int.Parse(variableTwo), calculationType);

Console.WriteLine();
Console.WriteLine(result);

Console.WriteLine("Press any key to exit...");
Console.ReadLine();
