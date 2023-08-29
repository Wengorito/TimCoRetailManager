using System;
using System.Net.Http;
using System.Threading.Tasks;
using TRMDesktopUI.ViewModels;

namespace TRMDesktopUI.Library.Api
{
    public class InventoryEndpoint : IInventoryEndpoint
    {
        private IApiHelper _apiHelper;

        public InventoryEndpoint(IApiHelper apiHelper)
        {
            _apiHelper = apiHelper;
        }

        public async Task PostPurchase(InventoryModel purchase)
        {
            using (HttpResponseMessage response = await _apiHelper.ApiClient.PostAsJsonAsync("/api/Inventory", purchase))
            {
                if (response.IsSuccessStatusCode)
                {
                    // Log successful call?
                }
                else
                {
                    throw new Exception(response.ReasonPhrase);
                }
            }
        }
    }
}
