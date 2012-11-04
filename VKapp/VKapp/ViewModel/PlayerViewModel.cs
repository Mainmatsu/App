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
    public class PlayerViewModel : ViewModelBase
    {
            private readonly IUserDataRepository _userDataRepository;
            private readonly IAppService _appService;
            private Song _mediaFile;

            public Song MediaFile
            {
                get { return _mediaFile; }
                set
                {
                    _mediaFile = value;
                    RaisePropertyChanged("MediaFile");
                }
            }

            public PlayerViewModel(IUserDataRepository userDataRepository, IAppService appService)
            {
                _userDataRepository = userDataRepository;
                _appService = appService;

                Messenger.Default.Register<Song>(this, PlayListChange);
            }

            private void PlayListChange(Song obj)
            {
                MediaFile = obj;
            }
    }
}
