using System.Threading.Tasks;
using TRMDesktopUI.ViewModels;

namespace TRMDesktopUI.Library.Api
{
    public interface IInventoryEndpoint
    {
        Task PostPurchase(InventoryModel purchase);
    }
}