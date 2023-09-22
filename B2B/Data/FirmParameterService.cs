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
            using var scope = _serviceProvider.CreateScope();
            _firmParamRepository = scope.ServiceProvider.GetRequiredService<IFirmParamRepository>();
            _firmParam = _firmParamRepository.firmParam();
        }

        public void SetLastUpdate(DateTime date)
        {
            using var scope = _serviceProvider.CreateScope();
            _firmParamRepository = scope.ServiceProvider.GetRequiredService<IFirmParamRepository>();
            _firmParam.LastSync = date;
            _firmParamRepository.Update(_firmParam);
        }

        public FirmParam FirmParam
        {
            get
            {
                return _firmParam;
            }
        }


    }
}
