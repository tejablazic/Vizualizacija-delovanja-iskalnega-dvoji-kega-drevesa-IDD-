using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IskalnoDvojiskoDrevo
{
    internal class Korak
    {
        public int? TrenutniPodatek { get; set; } // vozlisce
        public string Akcija { get; set; } // to so koraki pri iskanju, brisanju, vstavljanju v IDD
    }
}
