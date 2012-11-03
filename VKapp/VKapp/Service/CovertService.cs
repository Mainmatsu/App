using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace VKapp.Service
{
    public  class ConvertService :IConvertService
    {
        private string _token;

        public async Task<string> ToAccessTokenAsync(string link)
        {
            Regex regex = new Regex(@"accdess_token=+[0-9a-fA-F]*");
            await Task.Run(() =>
                               {
                                   _token = regex.Match(link).ToString();
                               });
            return _token != "" ? _token.Substring(13) : string.Empty;
        }
    }
}
