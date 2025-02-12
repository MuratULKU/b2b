using Business.Abstract;
using Business.Concrete;
using Core.Abstract;
using Core.Concrete;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.SingletonServices
{
    public class FirmParameter
    {
        private IFirmParamService _firmParamService;
        private readonly IServiceProvider _serviceProvider;
        public readonly List<FirmParam> FirmParameters;

        public FirmParameter(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            using var scope = _serviceProvider.CreateScope();
            _firmParamService = scope.ServiceProvider.GetRequiredService<IFirmParamService>();
            FirmParameters = _firmParamService.GetAll().Result;
        }
        public object Get(int no)
        {
            using var scope = _serviceProvider.CreateScope();
            _firmParamService = scope.ServiceProvider.GetRequiredService<IFirmParamService>();
            var result =  _firmParamService.Get(no);
            return result;
        }
        public async void Set(int no, object value)
        {
            using var scope = _serviceProvider.CreateScope();
            _firmParamService = scope.ServiceProvider.GetRequiredService<IFirmParamService>();
            var firmparam = await _firmParamService.Get(no);

            firmparam.Value = ASCIIEncoding.ASCII.GetBytes(value.ToString());

            await _firmParamService.Update(firmparam);
        }
        public async Task<IResult> Update(FirmParam firmParam)
        {
            using var scope = _serviceProvider.CreateScope();
            _firmParamService = scope.ServiceProvider.GetRequiredService<IFirmParamService>();
            return await _firmParamService.Update(firmParam);
        }
        public string ToString(byte[] bytes)
        {
            return Encoding.UTF8.GetString(bytes);
        }
        public string ToString(int no)
        {
            var firmparam = FirmParameters.FirstOrDefault(x => x.No == no);
            if (firmparam != null)
            {
                return ToString(firmparam.Value);
           
            }
            throw new InvalidOperationException("Value cannot be converted to boolean.");
        }
        public bool ToBoolean(int no)
        {
            var firmparam = FirmParameters.FirstOrDefault(x => x.No == no);
            if (firmparam != null)
            {
                if (ToString(firmparam.Value) == "1")
                    return true;
                return false;

            }
            throw new InvalidOperationException("Value cannot be converted to boolean.");
        }
        public int ToInteger(int no)
        {
            return Convert.ToInt32(ToString(no));
        }
    }
}
