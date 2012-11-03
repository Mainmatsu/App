using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;
using VKapp.Repository;
using Windows.Security.Authentication.Web;
using Windows.Storage;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;

namespace VKapp.Service
{
    class NotificationService :  INotificationService
    {

        private readonly IConvertService _convertService;
        private readonly IUserDataRepository _userDataRepository;
        public ApplicationDataContainer Current { get; set; }
        private const string AppId = "3136886";
        private const string Settings = "audio,friends";
        public bool IsLogin;
        private CoreWindow _coreWindow;
        private MessageDialog _messageDialog;
        

        public NotificationService(IConvertService convertService,IUserDataRepository userDataRepository)
        {
            Current = ApplicationData.Current.LocalSettings;
            _convertService = convertService;
            _userDataRepository = userDataRepository;
            _coreWindow = Window.Current.CoreWindow;
        }

        public async void LogInAsync()
        {
            var startUri = new Uri(
               string.Format(@"https://oauth.vk.com/authorize?client_id={0}&scope={1}&redirect_uri=http://api.vk.com/blank.html&display=page&response_type=token", AppId, Settings));

            var endUri = new Uri("http://api.vk.com/blank.html");

            try
            {
                var webAuthenticationResult = await WebAuthenticationBroker.AuthenticateAsync(
                WebAuthenticationOptions.None,
                startUri,
                endUri);

                switch (webAuthenticationResult.ResponseStatus)
                {
                    case WebAuthenticationStatus.Success:
                        {
                            var appToken = await _convertService.ToAccessTokenAsync(webAuthenticationResult.ResponseData);
                            if (appToken != string.Empty)
                                Current.Values["AppToken"] = appToken;


                        } break;
                    case WebAuthenticationStatus.ErrorHttp:
                        _messageDialog = new Windows.UI.Popups.MessageDialog("WebAuthentication Http Error", "Ошибка");
                        await _messageDialog.ShowAsync();
                        LogInAsync();
                        break;
                    case WebAuthenticationStatus.UserCancel:
                        _messageDialog = new Windows.UI.Popups.MessageDialog("Прервано пользователем", "Ошибка");
                        await _messageDialog.ShowAsync();
                        LogInAsync();
                        break;
                    default:
                        _messageDialog = new Windows.UI.Popups.MessageDialog("Неудалось авторизироватся", "Ошибка");
                        await _messageDialog.ShowAsync(); 
                        LogInAsync();
                        break;

                }
            }
            catch
            {
                
            }
        }
        public void LogOutAsync()
        {
            Current.Values["AppToken"] = null;
        }


    }
}
