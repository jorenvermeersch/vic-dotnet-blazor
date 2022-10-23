namespace Domain;
public class Port {
    public int Number { get; set; }
    public string Service { get; set; }
    public Port(int number, string service) {
        Number = number;
        Service = service;
    }
}
