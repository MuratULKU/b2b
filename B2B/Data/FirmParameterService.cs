using DataAccess.Abstract;
using Entity;

namespace B2B.Data
{
    public class FirmParameterService
    {
        //singleton service
        private readonly IServiceProvider _serviceProvider;
        private IFirmParamRepository _firmParamRepository;
        private FirmParam _firmParam;

        public FirmParameterService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            
        }

        public object Get(int no)
        {
            using var scope = _serviceProvider.CreateScope();
            _firmParamRepository = scope.ServiceProvider.GetRequiredService<IFirmParamRepository>();
            return _firmParamRepository.Get(no);
        }

        public void Set(int no, object value)
        {
            using var scope = _serviceProvider.CreateScope();
            _firmParamRepository = scope.ServiceProvider.GetRequiredService<IFirmParamRepository>();
            _firmParamRepository.Update(no,value);
        }
        

    }
}
