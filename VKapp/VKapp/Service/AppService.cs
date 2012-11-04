using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VKapp.Model;
using VKapp.Repository;
using Windows.Data.Xml.Dom;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace VKapp.Service
{
    class AppService:IAppService
    {
        private CoreWindow coreWindow = Window.Current.CoreWindow;
        private readonly INotificationService _notificationService;
        private readonly IUserDataRepository _userDataRepository;
        private readonly IApiService _apiService;
        private XDocument _document;

        public AppService(
            INotificationService notificationService,
            IUserDataRepository userDataRepository,
            IApiService apiService)
        {
            _notificationService = notificationService;
            _userDataRepository = userDataRepository;
            _apiService = apiService;
        }

        public  void AuthenticateUser()
        {

            if (!_notificationService.IsLogin)
            {
                _notificationService.LogInAsync(); 
            }
        }

        public void LogOut()
        {
            _notificationService.LogOutAsync();
        }

        public void LogIn()
        {
            
            _notificationService.LogInAsync();
            
        }

        public void ChangeFriendPositionTo(int newPosition)
        {
            throw new NotImplementedException();
        }

        public void RemoveSong()
        {
            throw new NotImplementedException();
        }

        public void AddSong()
        {
            throw new NotImplementedException();
        }

        public void ChangeSongPosition(int newPosition)
        {
            throw new NotImplementedException();
        }
    }
}
