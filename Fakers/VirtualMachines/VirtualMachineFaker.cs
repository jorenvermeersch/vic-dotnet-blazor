﻿using BogusStore.Fakers.Common;
using Domain.Accounts;
using Domain.Common;
using Domain.Constants;
using Domain.Customers;
using Domain.Hosts;
using Domain.VirtualMachines;
using Fakers.Accounts;
using Fakers.Credentials;
using Fakers.Hosts;
using Fakers.Specifications;
using Fakers.TimeSpan;
using System;

namespace Fakers.VirtualMachines;

public class VirtualMachineFaker : EntityFaker<VirtualMachine>
{
    public List<Customer>? Customers { get; set; } = new();
    public List<Account>? Accounts { get; set; } = new();
    public List<Domain.Common.Specifications>? Specifications { get; set; } = new();
    public List<Domain.VirtualMachines.Credentials>? Credentials { get; set; } = new();
    public List<Domain.VirtualMachines.TimeSpan>? TimeSpans { get; set; } = new();
    public List<Server> Hosts { get; set; } = new();


    public VirtualMachineFaker(string locale = "nl")
    {
        List<Port> ports = new();
        ports.AddRange(
            new List<Port>
            {
                new Port(number: 443, service: "HTTPS"),
                new Port(number: 80, service: "HTTP"),
                new Port(number: 22, "SSH")
            }
        );

        CustomInstantiator(f => new VirtualMachine(new VirtualMachineArgs
        {
            // EASY TO INITIALIZE
            Name = f.Internet.DomainWord(),
            Fqdn = f.Internet.DomainName(),
            Reason = f.PickRandom(new[] { "Bachelor proef", "AI trainen", "Database draaien" }),

            // ENUMS
            Template = f.PickRandom(Enum.GetValues(typeof(Template)).Cast<Template>().ToList()),
            Mode = f.PickRandom(Enum.GetValues(typeof(Mode)).Cast<Mode>().ToList()),
            Availabilities = Enumerable.Range(1, f.Random.Int(1, 5)).Select(x => f.PickRandom(Enum.GetValues(typeof(Availability)).Cast<Availability>().ToList())).ToList(),
            BackupFrequency = f.PickRandom(Enum.GetValues(typeof(BackupFrequency)).Cast<BackupFrequency>().ToList()),
            Status = f.PickRandom(Enum.GetValues(typeof(Status)).Cast<Status>().ToList()),

            // OTHER FAKERS
            User = f.PickRandom(Customers),
            Requester = f.PickRandom(Customers),
            Account = f.PickRandom(Accounts),
            Credentials = Enumerable.Range(1, f.Random.Int(1, 5)).Select(x => f.PickRandom(Credentials)).ToList(),
            //Host = f.PickRandom(Hosts),
            Host = null, // host gets initialized later on.
            Ports = Enumerable.Range(1, f.Random.Int(1, 2)).Select(x => f.PickRandom(ports)).ToList(),
            TimeSpan = f.PickRandom(TimeSpans),
            Specifications = f.PickRandom(Specifications)
        }));
    }
}
