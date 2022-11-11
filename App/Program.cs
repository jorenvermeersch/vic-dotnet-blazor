//Server server = new($"test-server-1", new Specifications(1, 1, 1), new HashSet<VirtualMachine>());

//VirtualMachineArgs arguments = new()
//{
//    Host = server,
//    Specifications = new Specifications(1, 1, 1),
//};
//VirtualMachine machine = new(arguments);

//Console.WriteLine(server.RemainingResources);
//machine.Specifications = new Specifications(1, 1, 2);
//Console.WriteLine(server.RemainingResources);

string s1 = "joren";
string s2 = "joren";

Console.WriteLine(s1 == s2);
Console.WriteLine(s2.Equals(s1));