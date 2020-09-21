using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.CodeDom.Compiler;
using Microsoft.CSharp;

namespace FCS
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                List<string> argümanlar = new List<string>();

                foreach (string arg in args)
                {
                    argümanlar.Add(arg);
                }

                if (argümanlar.Contains("-o"))
                {
                    string anadosya = argümanlar[0];

                    if (argümanlar.Contains("-r"))
                    {
                        int idx2 = argümanlar.IndexOf("-r");
                        int sonraidx1 = idx2 + 1;
                        string reference = argümanlar[sonraidx1];

                        int idx = argümanlar.IndexOf("-o");
                        int sonraidx = idx + 1;
                        string çıktıdosyası = argümanlar[sonraidx];

                        string[] referencearray = {reference};
                        derle(dönüştür(dosyaoku(anadosya)), çıktıdosyası, referencearray, true);
                    }
                    else
                    {
                        if (argümanlar.Contains("-rl"))
                        {
                            int idx2 = argümanlar.IndexOf("-rl");
                            int sonraidx1 = idx2 + 1;
                            string referencedosyası = argümanlar[sonraidx1];

                            int idx = argümanlar.IndexOf("-o");
                            int sonraidx = idx + 1;
                            string çıktıdosyası = argümanlar[sonraidx];

                            string[] lines = File.ReadAllLines(referencedosyası);

                            derle(dönüştür(dosyaoku(anadosya)), çıktıdosyası, lines, true);
                        }
                        else
                        {
                            string[] bosarray = {"", ""};
                            int idx = argümanlar.IndexOf("-o");
                            int sonraidx = idx + 1;
                            string çıktıdosyası = argümanlar[sonraidx];
                            derle(dönüştür(dosyaoku(anadosya)), çıktıdosyası, bosarray, false);
                        }
                    }
                }
                else
                {
                    hata();
                }
            }
            catch
            {
                hata();
            }
        }

        public static string dosyaoku(string dosya)
        {
            string dosyaiçerik;
            using (StreamReader reader = new StreamReader(dosya))
            {
                dosyaiçerik = reader.ReadToEnd();
            }
            return dosyaiçerik;
        }

        #region dönüştürmeler
        public static string dönüştür(string eskikod)
        {
            string anakod = eskikod;
            anakod = anakod.Replace("kullan", "using");
            anakod = anakod.Replace("alan", "namespace");
            anakod = anakod.Replace("sınıf", "class");
            anakod = anakod.Replace("statik", "static");
            anakod = anakod.Replace("boşluk", "void");
            anakod = anakod.Replace("Konsol.SatıraYaz(", "Console.WriteLine(");
            anakod = anakod.Replace("Konsol.Yaz(", "Console.Write(");
            anakod = anakod.Replace("Konsol.Oku(", "Console.Read(");
            anakod = anakod.Replace("Konsol.SatırdanOku(", "Console.ReadLine(");
            anakod = anakod.Replace("Konsol.TuşOku(", "Console.ReadKey(");
            anakod = anakod.Replace("eğer(", "if(");
            anakod = anakod.Replace("eğer (", "if (");
            anakod = anakod.Replace("değilse", "else");
            anakod = anakod.Replace("iken(", "while(");
            anakod = anakod.Replace("iken (", "while (");
            anakod = anakod.Replace("genel ", "public ");
            anakod = anakod.Replace("özel", "private");
            anakod = anakod.Replace("obje", "object");
            anakod = anakod.Replace("MesajKutusu.Göster", "MessageBox.Show");
            anakod = anakod.Replace("YayınYazıcı", "StreamWriter");
            anakod = anakod.Replace("YayınOkuyucu", "StreamReader");
            anakod = anakod.Replace("YazıYazıcı", "TextWriter");
            anakod = anakod.Replace("= yeni", "= new");
            anakod = anakod.Replace("=yeni", "=new");
            anakod = anakod.Replace("dene", "try");
            anakod = anakod.Replace("yakala", "catch");
            anakod = anakod.Replace("sonunda", "finally");
            anakod = anakod.Replace("parçalı", "partial");
            anakod = anakod.Replace("için(", "for(");
            anakod = anakod.Replace("için (", "for (");
            anakod = anakod.Replace("bool", "dydeğer");
            anakod = anakod.Replace("Görev.Başlat(", "Process.Start(");
            anakod = anakod.Replace(".Değiştir(", ".Replace(");
            anakod = anakod.Replace(".DiyalogGöster(", ".ShowDialog(");
            anakod = anakod.Replace(".Yazı", ".Text");
            anakod = anakod.Replace(".Kontroller", ".Controls");
            anakod = anakod.Replace(".Ekle(", ".Add(");
            anakod = anakod.Replace("Etiket", "Label");
            anakod = anakod.Replace(".Konum", ".Location");
            anakod = anakod.Replace("Nokta(", "Point(");
            anakod = anakod.Replace(".ClientBüyüklüğü.Yatay", ".ClientSize.Width");
            anakod = anakod.Replace(".ClientBüyüklüğü.Dikey", ".ClientSize.Height");
            anakod = anakod.Replace(".Büyüklük.Yatay", ".Size.Width");
            anakod = anakod.Replace(".Büyüklük.Dikey", ".Size.Height");
            //Console.WriteLine(anakod);
            return anakod;
        }
        #endregion

        public static void derle(string normalkod, string çıktıdosyası, string[] referenceler, bool ifreference)
        {
            CSharpCodeProvider codeProvider = new CSharpCodeProvider();
            ICodeCompiler icc = codeProvider.CreateCompiler();
            string Output = çıktıdosyası;
            CompilerParameters parameters = new CompilerParameters();
            parameters.GenerateExecutable = true;
            parameters.OutputAssembly = Output;
            try
            {
                if (ifreference)
                {
                    foreach (string line in referenceler)
                    {
                        parameters.ReferencedAssemblies.Add(line);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }

            CompilerResults results = icc.CompileAssemblyFromSource(parameters, normalkod);

            if (results.Errors.Count > 0)
            {
                foreach (CompilerError CompErr in results.Errors)
                {
                    Console.WriteLine(
                                "Satır " + CompErr.Line +
                                ", Hata Sayısı: " + CompErr.ErrorNumber +
                                ", '" + CompErr.ErrorText + ";");
                }
            }
            else
            {
                Console.WriteLine("Dosyanız Başarıyla Oluşturuldu.");
            }
        }

        public static void hata()
        {
            Console.WriteLine("FCS Derleyici        |         Türkçe Programlama Dili");
            Console.WriteLine("------------------------------------------------------");
            Console.WriteLine();
            Console.WriteLine("Kullanım: fcs.exe <fcs dosyası>");
            Console.WriteLine("             -o <çıktı dosyası> (zorunlu)");
            Console.WriteLine("             -r <reference adı> (isteğe bağlı)");
            Console.WriteLine("             -rl <referenceleri içeren txt dosyası> (isteğe bağlı)");
            Console.WriteLine();
            Console.WriteLine("                    github: wgex                      ");
            Console.ReadKey();
        }
    }
}
