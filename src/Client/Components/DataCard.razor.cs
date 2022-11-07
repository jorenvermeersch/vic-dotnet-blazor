using Microsoft.AspNetCore.Components;

namespace Client.Components;

public partial class DataCard
{
    [Parameter]
    public IDictionary<string, string> Entries { get; set; } = new Dictionary<string, string>();

    public DataCard()
    {
        Entries.Add("Email", "joren.vermeersch@student.hogent.be");
        Entries.Add("Telefoonnummer", "09 325 05 27");
    }
}