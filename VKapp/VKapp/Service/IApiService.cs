using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace VKapp.Service
{
    interface IApiService
    {
        Task<XDocument> GetApiAsync(string methodName, string parametrs, bool xml = true);

    }
}
