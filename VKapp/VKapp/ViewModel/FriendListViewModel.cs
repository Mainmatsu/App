using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using VKapp.Model;
using VKapp.Repository;
using VKapp.Service;

namespace VKapp.ViewModel
{
    public class FriendListViewModel :ViewModelBase
    {
            private readonly IUserDataRepository _userDataRepository;
            private readonly IAppService _appService;
            public RelayCommand SelectionChanged { get; set; }

            public ObservableCollection<Person> Friends { get { return _userDataRepository.Friends; } }

            public Person Selected
            {
                get { return _userDataRepository.SelectedPerson; }
                set
                {
                    _userDataRepository.SelectedPerson = value;
                    RaisePropertyChanged("Selected");
                }
            }

            public FriendListViewModel(IUserDataRepository userDataRepository, IAppService appService)
            {
                _userDataRepository = userDataRepository;
                _appService = appService;

                SelectionChanged = new RelayCommand(Go);
            }

            private void Go()
            {
                Messenger.Default.Send<PlayList>(Selected.Songs);
            }
    }
}
