using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VKapp.Model
{
    public class Song
    {
        public Uri Uri { get; set; }
        public string Id { get; set; }
        public string OwnerId { get; set; }

        public string Artist { get; set; }
        public string Title { get; set; }
        public string Text { get; set; }
        public string Duration { get; set; }
    }
}
