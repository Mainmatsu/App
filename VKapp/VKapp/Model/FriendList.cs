using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Storage;
using Windows.UI.Xaml.Data;

namespace VKapp.Model
{
    public class FriendList : ObservableCollection<Person>, ISupportIncrementalLoading
    {
        private ApplicationDataContainer Current;
        private readonly string _methodName;
        private readonly bool _xml;
        private int _offset;
        private XDocument _document;

        public FriendList()
        {
            _xml = true;
            _methodName = "friends.get";
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

        public async Task<LoadMoreItemsResult> LoadDataAsync(uint count)
        {

            _document = null;

            try
            {
                await Task.Run(() =>
                {

                    _document = XDocument.Load("https://api.vk.com/method/" + _methodName +
                    (_xml ? ".xml" : "") +
                    "?" + "count=" + count + "&offset=" + _offset + "&fields=uid,first_name,last_name,photo_medium&order=hints" +
                    "&access_token=" + Current.Values["AppToken"]);
                });

                var downloadList = (from item in _document.Root.Elements()
                                    select new Person()
                                    {
                                        Foto = new Uri(item.Element("photo_medium").Value),
                                        FirstName = item.Element("first_name").Value,
                                        LastName = item.Element("last_name").Value,
                                        Id = Convert.ToInt32(item.Element("uid").Value),
                                        Songs = new PlayList(Convert.ToInt32(item.Element("uid").Value))
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
