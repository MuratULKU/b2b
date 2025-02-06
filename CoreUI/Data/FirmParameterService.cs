using Business.Abstract;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using Entity;
using Microsoft.EntityFrameworkCore;
using System.Text;
using static QuestPDF.Helpers.Colors;

namespace CoreUI.Data
{
    public class FirmParameterService
    {
        private readonly IFirmParamService _firmParamService;
        private readonly IServiceProvider _serviceProvider;
        public readonly List<FirmParam> FirmParameters;

        public FirmParameterService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            using var scope = _serviceProvider.CreateScope();
            _firmParamService = scope.ServiceProvider.GetRequiredService<IFirmParamService>();
            FirmParameters = _firmParamService.GetAll().Result;
        }
        public object Get(int no)
        {

            return _firmParamService.Get(no);
        }
        public async void Set(int no, object value)
        {

            var firmparam = await _firmParamService.Get(no);

            firmparam.Value = ASCIIEncoding.ASCII.GetBytes(value.ToString());

            await _firmParamService.Update(firmparam);
        }

        public string ToString(int no)
        {
            FirmParam result = (FirmParam)Get(no);
            return Encoding.UTF8.GetString((byte[])result.Value);
        }
    }


}
