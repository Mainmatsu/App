using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;

namespace VKapp.Service
{
    interface INotificationService
    {
        bool IsLogin { get;}
        ApplicationDataContainer Current { get; set; }

        void LogInAsync();
        void LogOutAsync();
    }
}
