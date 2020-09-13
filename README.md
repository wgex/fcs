# fcs
yeni nesil türkçe programlama dili

## Syntax:

- C# syntax ının neredeyse aynısı
- çünkü bana göre en kolay syntax
- C# ta kullanılan referencelerin aynısı kullanıldı.
- derleyici programı `.net` e dönüştürür

## Örnek Program:
```
kullan System;
kullan System.Text;
kullan System.Windows.Forms;
kullan System.Drawing;
kullan System.Data;
kullan System.Collections.Generic;
kullan System.Linq;
kullan System.Windows;

alan FCS
{
    sınıf Program
    {
        statik boşluk Main(string[] args)
        {
            kullan (Form form = yeni Form())
            {
                Etiket etiket = yeni Etiket();
                etiket.Yazı = "Merhaba!";
                etiket.Font = yeni Font("Courier", 13);
                //etiket i ortaladım
                etiket.Konum = yeni Nokta(form.ClientBüyüklüğü.Yatay / 2 - etiket.Büyüklük.Yatay   / 2,   form.ClientBüyüklüğü.Dikey / 2 - etiket.Büyüklük.Dikey / 2);
                form.Yazı = "Başlangıç Formu";
                //etiket i form a ekledim
                form.Kontroller.Ekle(etiket);
                form.DiyalogGöster();
            }
        }
    }
}
```

### Discord: wgex#7561
