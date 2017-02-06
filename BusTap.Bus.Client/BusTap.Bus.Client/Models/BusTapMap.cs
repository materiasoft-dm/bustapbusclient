using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Maps;

namespace BusTap.Bus.Client.Models
{
    public class BusTapMap: Map
    {
        public List<CustomPin> CustomPins { get; set; }
    }
}
