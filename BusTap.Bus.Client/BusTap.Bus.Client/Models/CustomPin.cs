using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace BusTap.Bus.Client.Models
{
    public class CustomPin
    {
        public Pin Pin { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
    }
}
