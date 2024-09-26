using Exercise_2_1_3;

Console.WriteLine("Exercise_2_1_1\n");

var relation = new AttributeList("A,B,C,G,H,I");

Console.WriteLine($"Relation: {relation}");
Console.WriteLine();

var fdList = new List<FunctionalDependency>()
{
    new FunctionalDependency("A", "B"),
    new FunctionalDependency("A", "C"),
    new FunctionalDependency("C G", "H"),
    new FunctionalDependency("C G", "I"),
    new FunctionalDependency("B", "H")
};

Console.WriteLine("Functional dependencies:");
fdList.Print();
Console.WriteLine();

var keys = AttributeClosure.FindKeys(relation, fdList);
Console.WriteLine("Keys:");
foreach (var key in keys)
{
    Console.WriteLine(key);
}
Console.WriteLine();

var superkeys = AttributeClosure.FindSuperKeys(relation, fdList);

Console.WriteLine("Superkeys:");
foreach (var key in superkeys)
{
    Console.WriteLine(key);
}