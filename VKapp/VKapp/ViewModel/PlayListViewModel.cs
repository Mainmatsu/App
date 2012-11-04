using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using VKapp.Model;
using VKapp.Repository;
using VKapp.Service;

namespace VKapp.ViewModel
{
    public class PlayListViewModel : ViewModelBase
    {
        private readonly IUserDataRepository _userDataRepository;
        private readonly IAppService _appService;
        public RelayCommand SelectionChanged { get; set; }
        private PlayList _playList;

        public PlayList PlayList
        {
            get
            {
                return _playList;
            }
            set
            {
                _playList = value;
                RaisePropertyChanged("PlayList");
            }
        }

        public Song Selected
        {
            get
            {
                return _userDataRepository.SelectedSong;
            }
            set
            {
                _userDataRepository.SelectedSong = value;
                RaisePropertyChanged("Selected");
            }
        }

        public PlayListViewModel(IUserDataRepository userDataRepository, IAppService appService)
        {
            _userDataRepository = userDataRepository;
            _appService = appService;

            SelectionChanged = new RelayCommand(Go);
            Messenger.Default.Register<PlayList>(this,PlayListChange);
        }

        private void PlayListChange(Model.PlayList obj)
        {
            PlayList = obj;
        }

        private void Go()
        {
            Messenger.Default.Send<Song>(Selected);
        }
    }
}
