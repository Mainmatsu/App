/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:VKapp"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using VKapp.Repository;
using VKapp.Service;

namespace VKapp.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            ////if (ViewModelBase.IsInDesignModeStatic)
            ////{
            ////    // Create design time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DesignDataService>();
            ////}
            ////else
            ////{
            ////    // Create run time view services and models
            ////    SimpleIoc.Default.Register<IDataService, DataService>();
            ////}
            SimpleIoc.Default.Register<IApiService,ApiService>();
            SimpleIoc.Default.Register<INotificationService,NotificationService>();
            SimpleIoc.Default.Register<IAppService,AppService>();
            SimpleIoc.Default.Register<IConvertService,ConvertService>();
            SimpleIoc.Default.Register<IUserDataRepository,InMamoryUserDataRepository>();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<FriendListViewModel>();
            SimpleIoc.Default.Register<PlayListViewModel>();
            SimpleIoc.Default.Register<PlayerViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public FriendListViewModel FriendList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<FriendListViewModel>();
            }
        }

        public PlayListViewModel PlayList
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlayListViewModel>();
            }
        }

        public PlayerViewModel Player
        {
            get
            {
                return ServiceLocator.Current.GetInstance<PlayerViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}