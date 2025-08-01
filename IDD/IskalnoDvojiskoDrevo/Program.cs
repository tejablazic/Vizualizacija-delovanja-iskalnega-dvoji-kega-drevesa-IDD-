using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IskalnoDvojiskoDrevo
{
    internal class Program
    {
        static void Main(string[] args)
        {
            // Ustvari drevo
            IDD drevo = new IDD();
            IDDKoraki viz = new IDDKoraki(drevo);

            // Test 1: Vstavi z animacijo
            Console.WriteLine("=== Vstavljanje 15 ===");
            var korakiVstavi = viz.VstaviZKoraki(15);
            IzpisiKorake(korakiVstavi);

            // Test 2: Vstavi še eno
            Console.WriteLine("\n=== Vstavljanje 10 ===");
            korakiVstavi = viz.VstaviZKoraki(10);
            IzpisiKorake(korakiVstavi);

            // Test 3: Iskanje
            Console.WriteLine("\n=== Iskanje 10 ===");
            var korakiIskanje = viz.IskanjeZKoraki(10);
            IzpisiKorake(korakiIskanje);

            // Test 4: Brisanje
            Console.WriteLine("\n=== Brisanje 15 ===");
            var korakiBrisi = viz.BrisiZKoraki(15);
            IzpisiKorake(korakiBrisi);

            Console.WriteLine("\n=== Trenutno stanje drevesa ===");
            Console.WriteLine(drevo.ToString());

            //
            Console.WriteLine("Novi testi: \n \n");
            int[] vrednosti = new int[] { 3, 5, 4, 1, 4, 2, 6 };
            IDD drevo2 = new IDD(vrednosti);
            Console.WriteLine(drevo2.ToString());
            drevo2.Brisi(5);
            Console.WriteLine(drevo2.ToString());

            //
            Console.WriteLine("Novi testi");
            IDD drevo3 = new IDD();
            drevo3.Vstavi(14);
            drevo3.Vstavi(1);
            drevo3.Vstavi(2);
            drevo3.Vstavi(3);
            Console.WriteLine(drevo3.ToString());
            drevo3.Vstavi(2);
            Console.WriteLine(drevo3.ToString());


        }


        static void IzpisiKorake(List<Korak> koraki)
        {
            foreach (var korak in koraki)
            {
                string podatek = korak.TrenutniPodatek.HasValue ? korak.TrenutniPodatek.ToString() : "null";
                Console.WriteLine($"• {korak.Akcija} pri: {podatek}");
            }
        }
    }
}
