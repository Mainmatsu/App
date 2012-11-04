using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using VKapp.Service;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace VKapp.Model
{
    public class PlayList : ObservableCollection<Song>, ISupportIncrementalLoading
    {
        private ApplicationDataContainer Current;
        private readonly string _methodName;
        private readonly bool _xml;
        private int _userId;
        private int _offset;
        private XDocument _document;


        public PlayList(int userId = 0)
        {
            _xml = true;
            _methodName = "audio.get";
            _userId = userId;
            Current = ApplicationData.Current.LocalSettings;
        }

        public bool HasMoreItems
        {
            get
            {
                return true;
            }
        }

        public IAsyncOperation<LoadMoreItemsResult> LoadMoreItemsAsync(uint count)
        {
            return LoadDataAsync(count).AsAsyncOperation();
        }

        private string ConvertDuration(string duration)
        {
            int _duration = Convert.ToInt32(duration);
            int _counter;

            _counter = _duration/60;
            _duration = _duration - 60*_counter;

            if (_duration<10)
                return string.Format("{0}:0{1}", _counter, _duration);
            else
                return string.Format("{0}:{1}",_counter,_duration);
        }

        public async Task<LoadMoreItemsResult> LoadDataAsync(uint count)
        {

            _document = null;

            try
            {
                await Task.Run(() =>
                {

                    _document = XDocument.Load("https://api.vk.com/method/" + _methodName +
                    (_xml ? ".xml" : "") +
                    "?" + "count=" + count + "&offset=" + _offset + "&uid=" + _userId +
                    "&access_token=" + Current.Values["AppToken"]);
                });

                var downloadList = (from item in _document.Root.Elements()
                                    select new Song()
                                    {
                                        Id = item.Element("aid").Value,
                                        Uri = new Uri(item.Element("url").Value),
                                        Artist = item.Element("artist").Value,
                                        Title = item.Element("title").Value,
                                        Duration = ConvertDuration(item.Element("duration").Value)
                                    }).ToList();

                foreach (var item in downloadList)
                {
                    this.Add(item);
                    _offset++;
                }
            }
            catch
            {


            }



            return new LoadMoreItemsResult { Count = count };
        }


    }
}
