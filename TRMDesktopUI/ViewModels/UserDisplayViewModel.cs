using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Dynamic;
using System.Linq;
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
        private Dictionary<string, string> _roles;

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

        private UserModel _selectedUser;
        public UserModel SelectedUser
        {
            get { return _selectedUser; }
            set
            {
                _selectedUser = value;
                SelectedUserName = value.Email;
                PopulateRoleLists();
                NotifyOfPropertyChange(() => SelectedUser);
            }
        }

        private string _selectedUserName;
        public string SelectedUserName
        {
            get { return _selectedUserName; }
            set
            {
                _selectedUserName = value;
                NotifyOfPropertyChange(() => SelectedUserName);
            }
        }

        private BindingList<string> _userRoles = new BindingList<string>();
        public BindingList<string> UserRoles
        {
            get { return _userRoles; }
            set
            {
                _userRoles = value;
                NotifyOfPropertyChange(() => UserRoles);
            }
        }

        private string _selectedUserRole;
        public string SelectedUserRole
        {
            get { return _selectedUserRole; }
            set
            {
                _selectedUserRole = value;
                NotifyOfPropertyChange(() => SelectedUserRole);
                NotifyOfPropertyChange(() => CanRemoveUserRole);
            }
        }

        private BindingList<string> _availableRoles;
        public BindingList<string> AvailableRoles
        {
            get { return _availableRoles; }
            set
            {
                _availableRoles = value;
                NotifyOfPropertyChange(() => AvailableRoles);
            }
        }

        private string _selectedAvailableRole;
        public string SelectedAvailableRole
        {
            get { return _selectedAvailableRole; }
            set
            {
                _selectedAvailableRole = value;
                NotifyOfPropertyChange(() => SelectedAvailableRole);
                NotifyOfPropertyChange(() => CanAddUserRole);
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
                await LoadRoles();
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
            var userList = await _userEndpoint.GetAllUsers();
            Users = new BindingList<UserModel>(userList);
        }

        private async Task LoadRoles()
        {
            _roles = await _userEndpoint.GetAllRoles();
        }

        private void PopulateRoleLists()
        {
            UserRoles = new BindingList<string>(SelectedUser.Roles.Select(x => x.Value).ToList());
            AvailableRoles = new BindingList<string>(_roles.Values.Except(UserRoles).ToList());
        }

        public bool CanRemoveUserRole
        {
            get
            {
                if (SelectedUserRole != null)
                {
                    return true;
                }

                return false;
            }
        }

        public async Task RemoveUserRole()
        {
            await _userEndpoint.RemoveRole(SelectedUser.Id, SelectedUserRole);

            AvailableRoles.Add(SelectedUserRole);
            UserRoles.Remove(SelectedUserRole);
        }

        public bool CanAddUserRole
        {
            get
            {
                if (SelectedAvailableRole != null)
                {
                    return true;
                }

                return false;
            }
        }

        public async Task AddUserRole()
        {
            await _userEndpoint.AddUserToRole(SelectedUser.Id, SelectedAvailableRole);

            UserRoles.Add(SelectedAvailableRole);
            AvailableRoles.Remove(SelectedAvailableRole);
        }
    }
}
