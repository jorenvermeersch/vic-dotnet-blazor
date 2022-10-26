using Domain.Domain;
using Shouldly;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests;

public class PortTests
{
    [Fact]
    public void Port_creation_is_correct()
    {
        Port port = new(22, "SSH");
        port.Number.ShouldBe(22);
        port.Service.ShouldBe("SSH");
    }

    [Theory]
    [InlineData(" ")]
    [InlineData("   ")]
    [InlineData("")]
    public void Port_with_empty_service_is_invalid(string service)
    {
        Should.Throw<ArgumentException>(() =>new Port(120, service));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(-5)]
    public void Port_with_negative_number_is_invalid(int number)
    {
        Should.Throw<ArgumentException>(() => new Port(number, "service"));
    }






}
