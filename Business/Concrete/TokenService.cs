using Business.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Json;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class TokenService : ITokenService
    {
       
        private string _token;
        private DateTime _expireDate;
       
        

        public async Task<string> GetToken(HttpClient _httpClient)
        {
            
            if (!string.IsNullOrEmpty(_token) && _expireDate > DateTime.Now)
                return _token;
            var loginRequest = new
            {
                username = "admin",
                password = "password"
            };
            var response = await _httpClient.PostAsJsonAsync("/login", loginRequest);
            response.EnsureSuccessStatusCode();

            var result = await response.Content.ReadFromJsonAsync<LoginResponse>();

            _token = result.Token;
            _expireDate = DateTime.Now.AddMinutes(30);

            return _token;
        }
    }

    public class LoginResponse
    {
        public string Token { get; set; }
    }
}
