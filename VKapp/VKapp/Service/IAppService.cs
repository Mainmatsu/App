using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKapp.Model;

namespace VKapp.Service
{
    public interface IAppService
    {
        void AuthenticateUser();
        void LogOut();
        void LogIn();

        // Friends
        void LoadFriends();
        void ChangeFriendPositionTo(int newPosition);
        void LoadFriendPlayList(int userId);
       

        // Songs
        void LoadPlayList();
        void RemoveSong();
        void AddSong();
        void ChangeSongPosition(int newPosition);
    }
}
