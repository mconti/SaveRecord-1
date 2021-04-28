using System;
using System.IO;
using System.Collections.Generic;

namespace saverecord.Models
{
    public class Comune
    {
        public int ID { get; set; }
        public string NomeComune { get; set; }
        public string CodiceCastale { get; set; }

        public Comune(){}

        public Comune(string riga, int id)
        {
            // Creo l'oggetto comune pertendo da una riga CSV
            string[] colonne = riga.Split(',');

            ID = id;
            CodiceCastale = colonne[0];
            NomeComune = colonne[1];
        }
    }

    public class Comuni : List<Comune> // Comuni è una List<Comune>.
    {
        public string NomeFile { get; }

        public Comuni(){}

        public Comuni(string fileName)
        {
            NomeFile = fileName;

            using(FileStream fin = new FileStream(fileName, FileMode.Open))
            {
                StreamReader sr = new StreamReader(fin);
            
                int id = 1;

                while(! sr.EndOfStream)
                {
                    string riga = sr.ReadLine();
                    Comune c = new Comune(riga, id);
                    Add(c);

                    riga = sr.ReadLine();
                    id++;
                }
            }            
        }

        public void Save()
        {
            string fileName = NomeFile.Split('.')[0] + ".bin";
            Save(fileName);
        }

        public void Save(string fileName)
        {
            FileStream fout = new FileStream(fileName, FileMode.Create);
            BinaryWriter br = new BinaryWriter(fout);

            foreach(Comune comune in this)
            {
                br.Write(comune.ID);
                br.Write(comune.CodiceCastale);
                br.Write(comune.NomeComune);
            }

            br.Flush();
            br.Close();
        }

        public void Load()
        {
            string fn = NomeFile.Split('.')[0] + ".bin";
            Load(fn);
        }

        public void Load(string fileName)
        {
            Clear();

            FileStream fin = new FileStream(fileName, FileMode.Open);
            BinaryReader br = new BinaryReader(fin);

            Comune c = new Comune();

            // Leggo l'ID.
            c.ID = br.ReadInt32();

            // Leggo il codice del comune.
            c.CodiceCastale = br.ReadString();

            // Leggo il nome del comune.
            c.NomeComune = br.ReadString();

            Add(c);

            c = new Comune();
            c.ID = br.ReadInt32();
            c.CodiceCastale = br.ReadString();
            c.NomeComune = br.ReadString();
            Add(c);
        }
    }
}