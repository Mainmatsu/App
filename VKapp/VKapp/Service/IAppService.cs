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
        void ChangeFriendPositionTo(int newPosition);
       
        // Songs
        void RemoveSong();
        void AddSong();
        void ChangeSongPosition(int newPosition);
    }
}
