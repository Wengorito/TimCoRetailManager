using Caliburn.Micro;
using System.Threading;
using System.Threading.Tasks;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private IEventAggregator _events;
        private ILoggedInUserModel _loggedInUser;
        private IApiHelper _apiHelper;

        public bool IsLoggedIn
        {
            get
            {
                bool output = true;

                if (string.IsNullOrEmpty(_loggedInUser.Token))
                {
                    output = false;
                }

                return output;
            }
        }

        public ShellViewModel(IEventAggregator events,
                              ILoggedInUserModel loggedInUser,
                              IApiHelper apiHelper)
        {
            _events = events;
            _loggedInUser = loggedInUser;
            _apiHelper = apiHelper;

            _events.SubscribeOnPublishedThread(this);

            ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
        }

        public void ExitApplication()
        {
            TryCloseAsync();
        }

        public async Task UserManagementAsync()
        {
            await ActivateItemAsync(IoC.Get<UserDisplayViewModel>(), new CancellationToken());
        }

        public async Task LogOutAsync()
        {
            _loggedInUser.ResetUSerModel();
            _apiHelper.LogOffUser();
            await ActivateItemAsync(IoC.Get<LoginViewModel>(), new CancellationToken());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public async Task HandleAsync(LogOnEvent message, CancellationToken cancellationToken)
        {
            await ActivateItemAsync(IoC.Get<SalesViewModel>(), new CancellationToken());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
