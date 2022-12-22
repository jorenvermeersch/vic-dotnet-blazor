using Client.Extensions;
using Shared.Customers;
using System.Net.Http.Json;

namespace Client.Customers;

public class CustomerService : ICustomerService
{
    private readonly HttpClient authenticatedClient;
    private const string endpoint = "api/customers";
    public CustomerService(HttpClient authenticatedClient)
    {
        this.authenticatedClient = authenticatedClient;
    }
    public async Task<CustomerResponse.Create> CreateAsync(CustomerRequest.Create request)
    {
        var response = await authenticatedClient.PostAsJsonAsync(endpoint, request);
        var content = await response.Content.ReadFromJsonAsync<CustomerResponse.Create>();
        return content!;
    }

    public Task DeleteAsync(CustomerRequest.Delete request)
    {
        throw new NotImplementedException();
    }

    public async Task<CustomerResponse.Edit> EditAsync(CustomerRequest.Edit request)
    {
        var response = await authenticatedClient.PutAsJsonAsync(endpoint, request);
        var content = await response.Content.ReadFromJsonAsync<CustomerResponse.Edit>();
        return content!;
    }

    public async Task<CustomerResponse.GetDetail> GetDetailAsync(CustomerRequest.GetDetail request)
    {
        var response = await authenticatedClient.GetFromJsonAsync<CustomerResponse.GetDetail>($"{endpoint}/{request.CustomerId}");
        return response!;
    }

    public async Task<CustomerResponse.GetIndex> GetIndexAsync(CustomerRequest.GetIndex request)
    {
        var queryParameters = request.GetQueryString();
        var response = await authenticatedClient.GetFromJsonAsync<CustomerResponse.GetIndex>($"{endpoint}?{queryParameters}");
        return response!;
    }

    public Task<CustomerResponse.GetAllDetail> GetAllDetailAsync(CustomerRequest.GetAllDetails request)
    {
        throw new NotImplementedException();
    }
}
