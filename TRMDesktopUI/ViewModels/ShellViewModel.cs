using Caliburn.Micro;
using TRMDesktopUI.EventModels;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>, IHandle<LogOnEvent>
    {
        private SalesViewModel _salesVM;
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

        public ShellViewModel(SalesViewModel salesVM, IEventAggregator events, ILoggedInUserModel loggedInUser, IApiHelper apiHelper)
        {
            _salesVM = salesVM;
            _events = events;
            _loggedInUser = loggedInUser;
            _apiHelper = apiHelper;

            _events.Subscribe(this);

            ActivateItem(IoC.Get<LoginViewModel>());
        }

        public void ExitApplication()
        {
            TryClose();
        }

        public void LogOut()
        {
            _loggedInUser.ResetUSerModel();
            _apiHelper.LogOffUser();
            ActivateItem(IoC.Get<LoginViewModel>());
            NotifyOfPropertyChange(() => IsLoggedIn);
        }

        public void Handle(LogOnEvent message)
        {
            ActivateItem(_salesVM);
            NotifyOfPropertyChange(() => IsLoggedIn);
        }
    }
}
