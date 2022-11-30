namespace Shared.Ports
{
    public class PortService : IPortService
    {
        public PortService()
        {

        }

        public Task<PortResponse.GetAll> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<PortResponse.GetAll> GetAllAsync(PortRequest.GetAll request)
        {
            throw new NotImplementedException();
        }

        public Task<PortResponse.GetDetail> GetDetailAsync(PortRequest.GetDetail request)
        {
            throw new NotImplementedException();
        }
    }
}

