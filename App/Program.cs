using Domain.Customers;
using Newtonsoft.Json;


var c = JsonConvert.DeserializeObject<ContactPerson>("{\"Firstname\":\"Sem\",\"Lastname\":\"Dijk\",\"Email\":\"Lucas.Wit70@yahoo.com\",\"PhoneNumber\":\"0601299945\",\"Id\":26}");

Console.WriteLine(c);