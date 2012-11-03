using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKapp.Service
{
    public interface IConvertService
    {
        Task<string> ToAccessTokenAsync(string link);
    }
}
