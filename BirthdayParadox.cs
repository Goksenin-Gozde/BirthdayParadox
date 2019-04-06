using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BirthdayParadoxConsole
{
    class Program
    {
        static Random rand = new Random();

        static int uretilenSayilar = 0; // methodlarımızda parametre olarak almadan kullanabilmek için class değişkeni olarak atadığımız sayaç.

        static void Main(string[] args)
        {
            int[,] istatistikarr = new int[11, 4];
            int n;
            int[] kisiSayilari;
            int[][] birthdayArray = Program.birthdayArray();
            Console.WriteLine("Doğum günü paradoxunu testine hoşgeldiniz.");
            Console.WriteLine("Dilerseniz çakışmaları öneyerek listeleyip toplam çakışma sayısını gösterebilir," +
                "\n dilerseniz de çakışmaları önlemeden listeleyip" +
                " yılın herbir günü için çakışma sayılarını size gösterebilirim");
            Console.WriteLine("Lütfen Çakışmaları önleyip toplam çakışmayı göstermemi istiyorsanız 1'i tuşlayınız.");
            Console.WriteLine("Eğer çakışmaları önlemeden listeleyip gün gün çakışmaları göstermemi isterseniz 2'yi ");
            Console.WriteLine("Eğer bütün deneylerin sonucunu bir tabloda görmek istiyorsanız 3'ü tuşlayınız.");
           
             int cevap = Convert.ToInt32(Console.ReadLine());
            if (cevap == 1)
            {
                //verilen cevaba göre hangi method ile çalışacağımıza karar verip buna göre dizilerimizi oluşturuyoruz.

                kisiSayilari = new int[3];
                kisiSayilari[0] = 50;
                kisiSayilari[1] = 100;
                kisiSayilari[2] = 250;
            }
            else if (cevap == 2 || cevap == 3)
            {
                kisiSayilari = new int[4];
                kisiSayilari[0] = 50;
                kisiSayilari[1] = 100;
                kisiSayilari[2] = 500;
                kisiSayilari[3] = 1000;
            }
            else {
                kisiSayilari = new int[0];
                Console.WriteLine("Hatalı bir tuşlama yaptınız.");
                Console.ReadKey();
                System.Environment.Exit(0);               
            }

            for (int k = 1; k <= 10; k++)
            {
                if (cevap == 1 || cevap == 2) {
                    Console.WriteLine(" ");
                    Console.WriteLine(" ");
                    Console.WriteLine("Deney " + k);//yalnızca ilk 2 deneyde sıra sıra yazdıracağız.
                    

                }//Deneyimizi 10 kere tekrarlayacağımız döngü
                
                for (int j = 0; j < kisiSayilari.Length; j++)

                {
                    Program.uretilenSayilar = 0;
                    // kişi sayılarına göre deneyleri yapacağımız döngü
                    n = kisiSayilari[j];
                    for (int i = 0; i < n; i++)
                    {
                        
                        // asıl işlemin yapıldığı ve ana methodları çalıştırdığımız döngümüz.
                        int[] sayilar = randomSayiUret(birthdayArray, rand);//Üretilen random sayılar 2 elemanlı bir diziye atılıyor.
                        //methodlarımızda sayilar[0] = ay sayilar[1] = gün olarak kullanılacak.

                        /*
                         Doğrudan bir jagged array içine bütün doğum günlerini atıp işlemleri yaparken elemanları 1 eksiltmeyi seçtik.
                         Böylece hem kolamamız çok kolaylaştı hem de çakışmaları toplarken yalnızca listenin içindeki elemanlarını 1 azaltıp toplamamız yeterli oldu.
                         */
                        if (cevap == 1) cakismalariOnleyerekEkle(birthdayArray, sayilar,rand);//Kullanıcının girdiği değere göre method seçiliyor.
                        else cakismalariEkle(birthdayArray, sayilar);
                        
                    }
                    if (cevap == 2)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                        Console.WriteLine(kisiSayilari[j] + " Kişi için toplam Çakışma Sayısı : " + toplamCakismaSayisiniBul(birthdayArray));
                        Console.WriteLine(kisiSayilari[j] + " Kişi için oluşan çakışmaları içeren takvim.");
                        arrayYazdir(birthdayArray);
                    }
                    else if (cevap == 1) 
                    {

                       
                        Console.WriteLine( "{0} kişi için {1} sayı üretilmiştir.", kisiSayilari[j], Program.uretilenSayilar);

                    }
                    else if (cevap == 3) {
                        
                        istatistikarr[k-1,j] = toplamCakismaSayisiniBul(birthdayArray); // k 1'den başladığı için.
                        sutunlarıTopla(istatistikarr);


                    }
                    

                    birthdayArray = Program.birthdayArray();
                }
                if (k == 10)
                    if (cevap == 3) {

                        arrayYazdir(istatistikarr);

                    }
                    else
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                        Console.WriteLine("Bütün deneyler bitti çıkış yapmak için bir tuşa basınız.");
                    }
                else
                if(cevap == 1 || cevap ==2)
                    {
                        Console.WriteLine(" ");
                        Console.WriteLine(" ");
                        Console.WriteLine((k + 1) + ". deney için herhangi bir tuşa basınız.");
                        Console.ReadKey();
                }

                
            }
            Console.ReadKey();
        }


        

        public static int[][] birthdayArray()
        {

            int[][] birthdayArray = new int[12][];

            /* şubat dışında bütün aylarda çiftler 31 tekler 30 gün olmalıdır.
            ocak(0) = 31,nisan(3) = 30 gibi
            0 ile 6 arasında çiftler 31 tekler 30 gün
            7 ile 11 arasında çiftler 30 tekler 31
            */

          

            for (int i = 0; i < birthdayArray.Length; i++)
            {
                if (i == 0) birthdayArray[i] = new int[31];

                else if (i == 1) birthdayArray[i] = new int[29];

                else
                {
                    if (i < 7)
                    {
                        if (i % 2 == 1) birthdayArray[i] = new int[30];


                        else birthdayArray[i] = new int[31];
                    }

                    else
                    {
                        if (i % 2 == 1) birthdayArray[i] = new int[31];


                        else birthdayArray[i] = new int[30];

                    }
                }
            }

            return birthdayArray;
        }

        public static void arrayYazdir(int[][] array)
        {

            Console.WriteLine(" ");
            Console.WriteLine(" ");
            Console.WriteLine("********************************************************************");
            
            for (int i = 0; i < array.Length; i++)
            {


                for (int j = 0; j < array[i].Length; j++)
                {
                    if (array[i][j] == 0)
                        Console.Write(array[i][j] + " ");
                else{
                        Console.Write(array[i][j] - 1 + " ");
                    }
                }
                Console.WriteLine(" ");
            }
            
            Console.WriteLine("********************************************************************");
            Console.WriteLine(" ");
            Console.WriteLine(" ");

        }
        public static void sutunlarıTopla(int[,] arr) {
            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);
            int toplam = 0;
            for (int j = 0; j < 4; j++)
            {
                for (int i = 0; i < rowLength-1; i++)
                {
                    toplam += arr[i,j];

                }
                arr[10, j] = toplam;
            }
        }
        public static void arrayYazdir(int[,] arr)
        {

            int rowLength = arr.GetLength(0);
            int colLength = arr.GetLength(1);
            Console.WriteLine("                  50   100   500   1000");
            Console.WriteLine("                kişi  kişi  kişi   kişi");
            Console.WriteLine("");
            for (int i = 0; i < rowLength; i++)
            {
                if (i == 10)
                {
                    Console.Write("Toplam çakışmalar =>");

                }
                else {
                    Console.Write("    {0}'ıncı deney =>", i + 1);
                }
                for (int j = 0; j < colLength; j++)
                {

                        Console.Write(string.Format("{0}   ", arr[i, j]));
                    
                }
                Console.Write(Environment.NewLine + Environment.NewLine);
            }

        }

        public static int[] randomSayiUret(int[][] arr,Random rand)
        {
            /*
             2 adet sayı üretiyor ve bunları 2 elemanlı bir dizide tutuyoruz. ilk eleman ayı 2. eleman günü veriyor
             */

            int[] sayilar = new int[2];

           
            int sayi1 = rand.Next(12); // 12 ay arasından rastgele sayı
            int sayi2 = rand.Next(arr[sayi1].Length);//ayın günleri arasından rastgele biri
            Program.uretilenSayilar++; // Bunu ilerde yazdıracağız.

            sayilar[0] = sayi1;
            sayilar[1] = sayi2;
            return sayilar;
        }
        public static void cakismalariEkle(int[][] arr, int[] sayilar)
        {

            arr[sayilar[0]][sayilar[1]] += 1; // programın teker teker yazdırdığımız durumu için gerekli olacak takvimi hazırlıyor
        }

        public static void cakismalariOnleyerekEkle(int[][] arr, int[] sayilar, Random rand) {

            /*
             Burada bize en efektif gelen yol özyinelemeli olarak yazmak oldu. Bu sayede program uygun bir tarih üretene kadar durmadan kendini çağırıyor.
             if'den sonra return koyarak da StackOverFlow hatasının önüne geçtik.
             */

            if (arr[sayilar[0]][sayilar[1]] == 0)
            {
                arr[sayilar[0]][sayilar[1]]++;
                return;
            }
            else
                sayilar = randomSayiUret(arr, rand);
            cakismalariOnleyerekEkle(arr, sayilar, rand);//özyinelemeli olarak çakışmayana kadar sayı üretecek.
            //
                
        }
        public static int toplamCakismaSayisiniBul(int[][] arr)
        {
            /*
             Biz diziye bütün doğum günlerini ekleyip, çakışmaları bunlamk için dizinin elemanlarını 1 eksiltmeyi tercih ettik.
             Yazarken bize büyük kolaylık sağladı.
             */

            int sum = 0;
            for (int i = 0; i < arr.Length; i++)
            {

                for (int j = 0; j < arr[i].Length; j++)
                {
                    if (arr[i][j] == 0) sum += arr[i][j];
                    else
                        sum += arr[i][j] - 1;
                }
            }

            return sum;
        }
    }
}


