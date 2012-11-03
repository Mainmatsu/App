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

        public async void AuthenticateUser()
        {
            await Task.Run(() =>  _notificationService.LogInAsync());
        }

        public void LogOut()
        {
            _notificationService.LogOutAsync();
        }

        public void LogIn()
        {
            
                _notificationService.LogInAsync();
            
        }

        public async void LoadFriendsAsync()
        {                                                                   
            _document = await _apiService.GetApiAsync("friends.get",
                                              string.Format("count=100&offset={0}&fields=uid,first_name,last_name,photo&order=hints", _userDataRepository.Offset));

            List<Person> downloadList = (from item in _document.Root.Elements()
             select new Person()
                        {
                            Foto = new Uri(item.Element("photo").Value),
                            FirstName = item.Element("first_name").Value,
                            LastName = item.Element("last_name").Value,
                            Id = Convert.ToInt32(item.Element("uid").Value)
                        }).ToList();

            foreach (var item in downloadList)
            {
                _userDataRepository.Add(item);
            }
                                                                             
        }

        public void ChangeFriendPositionTo(int newPosition)
        {
            throw new NotImplementedException();
        }

        public void LoadFriendPlayList(int userId)
        {
            throw new NotImplementedException();
        }

        public void LoadPlayList()
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
