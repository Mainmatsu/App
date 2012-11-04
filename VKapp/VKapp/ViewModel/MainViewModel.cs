using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using VKapp.Model;
using VKapp.Repository;
using VKapp.Service;

namespace VKapp.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private readonly IUserDataRepository _userDataRepository;
        private readonly IAppService _appService;
        private Song _selectedIndex = new Song();

        

        public ObservableCollection<Song> Songs { get { return _userDataRepository.I.Songs; } }

        public bool AutoPlay { get; set; }

        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        /// 
        /// 
        private ICommand _addNewTagCommand;
        private ICommand _playListSelectionChanged;

        public ICommand PlayerSelectionChanged
        {
            get
            {
                if (_playListSelectionChanged == null)
                    _playListSelectionChanged = new RelayCommand(SelectionChanged);

                return _playListSelectionChanged;
            }
        }

        public ICommand AddNewTagCommand
        {
            get
            {
                if (_addNewTagCommand == null)
                    _addNewTagCommand = new RelayCommand(AddNewTagExecute);

                return _addNewTagCommand;
            }
        }

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

        public MainViewModel(IUserDataRepository userDataRepository,IAppService appService)
        {
            _userDataRepository = userDataRepository;
            _appService = appService;


            _appService.AuthenticateUser();
        }

        public void AddNewTagExecute()
        {
            _appService.LoadPlayList();
        }

        public void SelectionChanged()
        {

        }

    }
}