using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Storage;

namespace VKapp.Service
{
    class ApiService: IApiService
    {
        private readonly INotificationService _notificationService;

        public ApiService(INotificationService notificationService)
        {
            _notificationService = notificationService;
        }

        public async Task<XDocument> GetApiAsync(string methodName, string parametrs, bool xml)
        {
            return  XDocument.Load("https://api.vk.com/method/" + methodName +
            (xml ? ".xml" : "") +
            "?" + parametrs +
            "&access_token=" + _notificationService.Current.Values["AppToken"]);
        }

    }
}
