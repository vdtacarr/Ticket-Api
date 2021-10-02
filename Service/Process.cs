using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace AdaYazılım.Controllers
{
    public class Process : IProcess
    {
        public List<Response> DoEverything(Base sayi)
        {
            Dictionary<int, string> emptySeat = new Dictionary<int, string>();
            var rezervasyonSayi = sayi.RezervasyonYapilacakKisiSayisi;
            //Vagonlardaki kullanılabilecek koltuklar teker teker hesaplanıyor.

            sayi.Tren.Vagonlar.ForEach(
                vagon => {
                    
                    if((vagon.DoluKoltukAdet / vagon.Kapasite)*100 < 70)
                    {
                        emptySeat.Add(70 - (vagon.DoluKoltukAdet / vagon.Kapasite) * 100, vagon.Ad);
                    }
                });

            if (sayi.KisilerFarkliVagonlaraYerlestirilebilir == false)
            {
                int count = 0;
                List<Yerlesim> lst = new List<Yerlesim>();
                List<Response> rspns = new List<Response>();
                foreach (KeyValuePair<int, string> rezervasyon in emptySeat)
                {
                    if (rezervasyon.Key >= rezervasyonSayi)
                    {
                        lst.Add(new Yerlesim { VagonAdi = rezervasyon.Value, KisiSayisi = rezervasyon.Key });
                        rspns.Add(new Response { RezervasyonYapilabilir = true, YerlesimAyrinti = lst });
                        return rspns;
                        count++;
                        break;
                    }
                   
                       
                   
                }
                // Eğer hiçbir vagonda yeterli boş yer yoksa burası çalışacak.

                if(count == emptySeat.Count)
                {
                    List<Response> rspns2 = new List<Response>();
                    rspns2.Add(new Response{ RezervasyonYapilabilir = false, YerlesimAyrinti = new List<Yerlesim>() });
                    return rspns2;
                }
            }

            else if(sayi.KisilerFarkliVagonlaraYerlestirilebilir == true)
            {
                List<Yerlesim> lst = new List<Yerlesim>();
                List<Response> rspns = new List<Response>();
                foreach (KeyValuePair<int, string> rezervasyon in emptySeat)
                {
                    do
                    {
                        rezervasyonSayi = rezervasyonSayi - rezervasyon.Key;
                        lst.Add(new Yerlesim { VagonAdi = rezervasyon.Value, KisiSayisi = rezervasyon.Key });
                    } while (rezervasyonSayi >= 0);


                }
                rspns.Add(new Response { RezervasyonYapilabilir = true, YerlesimAyrinti = lst });
                return rspns;
            }
            
        }
    }
}
