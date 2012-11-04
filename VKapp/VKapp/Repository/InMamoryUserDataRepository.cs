using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VKapp.Model;


namespace VKapp.Repository
{
    class InMamoryUserDataRepository :IUserDataRepository
    {
        //подумать о том где хранить песни.
        //в друзьях или всё в одном с ид друга 

        private readonly FriendList _friends = new FriendList();

        public FriendList Friends
        {
            get { return _friends; }
        }

        public int Offset { get; set; }

        public Person I { get; set; }
        
        InMamoryUserDataRepository()
        {
            Offset = 0;
            I = new Person(); 
        }

        public PlayList GetSongsByFrind(int friendId)
        {
            return _friends.FirstOrDefault(friend => friend.Id == friendId).Songs;
        }

        public async Task<Person> GetFriendById(int friendId)
        {
            Person person = null;
            await Task.Run(() =>
                                {
                                   person = _friends.FirstOrDefault(friend => friend.Id == friendId);
                                });
            return person;
        }

        public void Add(Person friend)
        {
             _friends.Add(friend);
             Offset++;
        }

        public void Add(Song song,Person friend)
        {
            _friends.FirstOrDefault(thisFriend => thisFriend.Id ==friend.Id).Songs.Add(song);
        }

        public void RemoveMy(Song song)
        {
            I.Songs.Remove(song);
        }

        public void RemoveAll()
        {
            _friends.Clear();
            I = null;
        }

        public void CommitChanges()
        {
             
        }
    }
}
