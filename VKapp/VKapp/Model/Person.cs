using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKapp.Model
{
    public class Person
    {
        public int Offset { get; set; }
        public int Counter { get; set; }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Uri Foto { get; set; }
        //Возможно заменить на IEnumer...
        public ObservableCollection<Song> Songs { get; set; }
    }
}
