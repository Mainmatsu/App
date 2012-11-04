using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using VKapp.Model;
using VKapp.Repository;
using VKapp.Service;

namespace VKapp.ViewModel
{
    class PlayListViewModel : ViewModelBase
    {
        private readonly IUserDataRepository _userDataRepository;
        private readonly IAppService _appService;
        private Song _selectedIndex;

        public ObservableCollection<Song> Songs { get { return _userDataRepository.I.Songs; } }

        public Song Selected
        {
            get
            {
                return _selectedIndex;
            }
            set
            {
                _selectedIndex = value;
                RaisePropertyChanged("Selected");
            }
        }

        public PlayListViewModel(IUserDataRepository userDataRepository, IAppService appService)
        {
            _userDataRepository = userDataRepository;
            _appService = appService;
        }
    }
}
