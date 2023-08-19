using Caliburn.Micro;
using System;
using System.ComponentModel;
using System.Dynamic;
using System.Threading.Tasks;
using System.Windows;
using TRMDesktopUI.Library.Api;
using TRMDesktopUI.Library.Models;

namespace TRMDesktopUI.ViewModels
{
    public class UserDisplayViewModel : Screen
    {
        private readonly StatusInfoViewModel _statusInfo;
        private readonly IWindowManager _windowManager;
        private readonly IUserEndpoint _userEndpoint;

        private BindingList<UserModel> _users;
        public BindingList<UserModel> Users
        {
            get { return _users; }
            set
            {
                _users = value;
                NotifyOfPropertyChange(() => Users);
            }
        }


        public UserDisplayViewModel
            (
            StatusInfoViewModel status,
            IWindowManager windowManager,
            IUserEndpoint userEndpoint
            )
        {
            _statusInfo = status;
            _windowManager = windowManager;
            _userEndpoint = userEndpoint;
        }

        protected override async void OnViewLoaded(object view)
        {
            base.OnViewLoaded(view);
            try
            {
                await LoadUsers();
            }
            catch (Exception ex)
            {
                dynamic settings = new ExpandoObject();
                settings.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                settings.ResizeMode = ResizeMode.NoResize;
                settings.Title = "System Error";

                if (ex.Message == "Unauthorized")
                {
                    _statusInfo.UpdateMessage("Unauthorized Access", "You do not have the permission to interact with the Sales Form");
                    _windowManager.ShowDialog(_statusInfo, null, settings);
                }
                else
                {
                    _statusInfo.UpdateMessage("Fatal Exception", ex.Message);
                    _windowManager.ShowDialog(_statusInfo, null, settings);
                }
                TryClose();
            }
        }

        private async Task LoadUsers()
        {
            var userList = await _userEndpoint.GetAll();
            Users = new BindingList<UserModel>(userList);
        }
    }
}
