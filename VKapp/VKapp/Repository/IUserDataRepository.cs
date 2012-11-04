﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKapp.Model;

namespace VKapp.Repository
{
    public interface IUserDataRepository
    {
        FriendList Friends { get; }

        int Offset { get; set; }

        Person I { get; set; }

        PlayList GetSongsByFrind(int friendId);
        Task<Person> GetFriendById(int friendId);

        //ChatUser GetUserById(string userId);

        void Add(Person friend);
        void Add(Song song,Person friend);

        void RemoveMy(Song song);
        void RemoveAll();
        void CommitChanges();
    }
}
