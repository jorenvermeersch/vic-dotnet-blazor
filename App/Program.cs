//testen VM repo
using Domain.Controllers;
using Domain.Domain;

#region VM Testen in CL
VirtualMachineController vmController = new();
//lijst zou 2 vm's moeten hebben (die zijn geseed)
Console.WriteLine($"Aantal vms in de lijst: {vmController.FindAllVirtualMachines().Count()}");

//We kunnen zoeken op een vm via zijn fqdn
Console.WriteLine($"Fqdn van vm1: {vmController.FindAllVirtualMachines().First().Fqdn}");
Console.WriteLine($"Fqdn van vm2: {vmController.FindAllVirtualMachines().Last().Fqdn}");

Console.WriteLine($"vm1 opzoeken via zijn Fqdn:" +
    $"\n    -Name: {vmController.FindVirtualMachineByFqdn("com.vic.vm1").Name}" +
    $"\n    -Reason: {vmController.FindVirtualMachineByFqdn("com.vic.vm1").Reason}" +
    $"\n    -Fqdn: {vmController.FindVirtualMachineByFqdn("com.vic.vm1").Fqdn}");

//Toevoegen van een nieuwe vm
VirtualMachineArgs vm = new();
Console.Write("naam: ");
vm.name = Console.ReadLine();
Console.Write("reason: ");
vm.reason = Console.ReadLine();
Console.Write("Fqdn: ");
vm.fqdn = Console.ReadLine();
vmController.AddVirtualMachine(vm);

Console.WriteLine($"Aantal vms in de lijst: {vmController.FindAllVirtualMachines().Count()}");
Console.WriteLine($"Fqdn van new vm: {vmController.FindVirtualMachineByFqdn(vm.fqdn).Fqdn}");
Console.WriteLine($"new vm opzoeken via zijn Fqdn:" +
    $"\n    -Name: {vmController.FindAllVirtualMachines().Last().Name}" +
    $"\n    -Reason: {vmController.FindAllVirtualMachines().Last().Reason}" +
    $"\n    -Fqdn: {vmController.FindAllVirtualMachines().Last().Fqdn}");

//Opezoeken vms van een user via zijn mail
Console.WriteLine($"Vm van kerem.yilmaz@valid.com: ");
ISet<VirtualMachine> vms = vmController.FindVirtualMachinesByUserEmail("kerem.yilmaz@valid.com");
Console.WriteLine(vms.First());

#endregion