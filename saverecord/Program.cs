using System;
using saverecord.Models;

namespace saverecord
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("SaveRecord - 2021 - diego.mari@studenti.ittsrimini.edu.it");

            // 1) Leggere un file CSV con i comuni e trasformarlo in una List<Comune>.
            Comuni c = new Comuni("Models/comuni.csv");
            Console.WriteLine($"Ho letto {c.Count} righe...");

            // 2) Scrivere la List<Comune> in un file binario.
            c.Save();

            // Rileggere il file binario in una List<Comune>.
        }
    }
}
