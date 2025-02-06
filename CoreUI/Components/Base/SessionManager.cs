
using CoreUI.Data;
using Entity;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace CoreUI.Components.Base
{
    public class SessionManager
    {
        private readonly ProtectedSessionStorage _sessionStroge;

        public SessionManager(ProtectedSessionStorage sessionStroge)
        {
            _sessionStroge = sessionStroge;
        }
        public void AddCart(Product product)
        {
            _sessionStroge.SetAsync("Cart",product.Id);
        }

        public async Task<OrderFicheModel> ReadCart()
        {
            var result = await _sessionStroge.GetAsync<OrderFicheModel>("Cart");
           return result.Value;
        }
    }
}
