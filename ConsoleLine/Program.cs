// See https://aka.ms/new-console-template for more information
using Domain.Controllers;
using Domain.Domain;

Console.WriteLine("Hello, World!");

VirtualMachineController vmController = new VirtualMachineController();
foreach(var item in vmController.GetVirtualMachines()) {
    Console.WriteLine(item);
}