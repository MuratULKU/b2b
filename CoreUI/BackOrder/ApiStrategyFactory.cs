namespace CoreUI.BackOrder
{
    public class ApiStrategyFactory
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IConfiguration _configuration;
        public ApiStrategyFactory(IServiceProvider serviceProvider, IConfiguration configuration)
        {
            _serviceProvider = serviceProvider;
            _configuration = configuration;
        }

      
        public IApiStrategy GetStrategy()
        {
            var apiType = _configuration["ApiSettings:ActiveApi"];

            return apiType switch
            {
                "Logo" => _serviceProvider.GetRequiredService<LogoApiStrategy>(),
                "Mikro" => _serviceProvider.GetRequiredService<MikroApiStrategy>(),
                "Ulku" => _serviceProvider.GetRequiredService<UlkuApiStrategy>(),
                _ => throw new Exception("Geçersiz API seçimi")
            };
        
        }

    }
}
