namespace Shared.Customers;

public interface ICustomerService
{
    Task<CustomerResponse.GetIndex> GetIndexAsync(CustomerRequest.GetIndex request);
    Task<CustomerResponse.GetDetail> GetDetailAsync(CustomerRequest.GetDetail request);
    Task<CustomerResponse.GetAllDetail> GetAllDetailAsync(CustomerRequest.GetAllDetails request);
    Task<CustomerResponse.Create> CreateAsync(CustomerRequest.Create request);
    Task<CustomerResponse.Edit> EditAsync(CustomerRequest.Edit request);
    Task DeleteAsync(CustomerRequest.Delete request);
}