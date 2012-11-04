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

        public async void LoadFriends()
        {                                                                   
            _document = await _apiService.GetApiAsync("friends.get",
                                              string.Format("count=40&offset={0}&fields=uid,first_name,last_name,photo&order=hints", _userDataRepository.Offset));

            var downloadList = (from item in _document.Root.Elements()
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

        public async void LoadPlayList()
        {
            if (_userDataRepository.Friends != null)
            {
                _document = await _apiService.GetApiAsync("audio.get",
                                                          string.Format("count=40&offset={0}", _userDataRepository.I.Offset));

                var downloadList = (from item in _document.Root.Elements()
                                    select new Song()
                                    {
                                        Id = item.Element("aid").Value,
                                        Uri = new Uri(item.Element("url").Value),
                                        Artist = item.Element("artist").Value,
                                        Title = item.Element("title").Value
                                    }).ToList();

                foreach (var item in downloadList)
                {
                    _userDataRepository.I.Songs.Add(item);
                    _userDataRepository.I.Offset++;
                }
            }
        }
        
        public async void LoadFriendPlayList(int friendId)

        {
            if (_userDataRepository.Friends != null)
            {
                var friend  =  await _userDataRepository.GetFriendById(friendId);

                _document = await _apiService.GetApiAsync("audio.get",
                                                          string.Format("count=40&offset={0}&uid={1}",friend.Offset,friendId));

                var downloadList = (_document.Root.Elements().Select(item => new Song()
                                                                                 {
                                                                                     Id = item.Element("aid").Value,
                                                                                     Uri =
                                                                                         new Uri(
                                                                                         item.Element("url").Value),
                                                                                     Artist =
                                                                                         item.Element("artist").Value,
                                                                                     Title = item.Element("title").Value
                                                                                 })).ToList();

                foreach (var item in downloadList)
                {
                    _userDataRepository.Add(item, friend);
                    friend.Offset++;
                }
            }
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
