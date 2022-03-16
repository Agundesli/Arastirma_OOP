using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
     class Program
    {
        static void Main(string[] args)
        {
            //Object
            //string
            Product product = new Product(1, "Abdullah", "Gündeşli");
            Product product1 = new Product(1, "Abdullah", "Gündeşli");
            Console.WriteLine("Equels ile kıyasla");//kendi oluşturduğumuz reference_type Product sınıfı için Virtual Equals metodunu bastırmamış olmamızdır.
                                                    //Object sınıfının Static Equals metodu referans tipli eşitlik modeline göre bir karşılaştırma yapmıştır,
                                                    //yani içeriğe bakmamıştır. Tıpkı ReferenceEquals metodu gibi
            if (Object.Equals(product,product1))
            {
                Console.WriteLine("Eşittir");
            }
            else
            {
                Console.WriteLine("Eşit Değildir");
            }
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Referans ile kıyasla");//Burada ReferenceEquals metodu karşılaştırdığı iki değişkenin referanslarına bakar ve
                                                      //bellekte aynı yeri işaret etmiyorsa false döner.
            if (Object.ReferenceEquals(product1,product))
            {
                Console.WriteLine("Eşittir");
            }
            else
            {
                Console.WriteLine("Eşit Değildir");
            }
            Console.WriteLine("------------------------------------------------------------");
            Console.WriteLine("Referans tipi biz oluşturmadık(Equals)");
            var Name = "Usame";
            var Name1 = "Usame";
            if (Object.Equals(Name, Name1))//bizim yazdığımız sınıfta bu eşitliği yakalayamayıp stringlerde yakalama sebebimiz,
                                           //String sınıfının kendi içerisinde Equals metodunu yazmış olması ve kullanmasıdır. Yani Equals metodunu bastırmış olmasıdır. 
            {
                Console.WriteLine("Eşittir");
            }
            else
            {
                Console.WriteLine("Eşit Değildir");
            }
            Console.WriteLine("----------------------------------------------------------------");
            Console.WriteLine("Referans tipi biz oluşturmadık(ReferenceEquals)");
            if (Object.ReferenceEquals(Name, Name1))
            {
                Console.WriteLine("Eşittir");
            }
            else
            {
                Console.WriteLine("Eşit Değildir");
            }
            Console.WriteLine("--Şimdi hem kendi classımız hemde String classı için HashCode inceleyelim--");
            //Peki neden bu uyarıyı alıyoruz ?
            Console.WriteLine("Name için Hash Değeri: "+Name.GetHashCode().ToString());
            Console.WriteLine("Name1 için Hash Değeri: "+Name1.GetHashCode().ToString());
            //Yukarıda gördüğünüz üzere iki eşit string için HashCode’ ları birbirlerine eşit.
            //Bu durum beklediğimiz gibi yani konunun en başında bahsettiğimiz gibi eşit iki nesnenin HashCode’ ları birbirlerine eşittir.
            Console.WriteLine("--------------------------------------------------------");
            Console.WriteLine("Product için Hash Değeri: "+product.GetHashCode().ToString());
            Console.WriteLine("Product1 için Hash Değeri: "+product1.GetHashCode().ToString());
            //İlginç olan Product sınıfına ait nesne örneklerinin içerikleri aynı olmasına rağmen üretilen HashCode’ larının farklı oluşudur.
            //Oysaki iki nesne örneği eğer değer tipi modeline göre eşit iseler aynı HashCode’ lardan bahsediyor olmamız gerekmektedir.

            //İşte bu durumu düzeltmek için Equals metodu bastırılmış bir class’ın GetHashCode metodu bastırılmalıdır.
        }
    }
    public class Product//'Product' overrides Object.Equals(object to) but does not override Object.GetHasCode()
    {//Tam bu noktada Equals metodunu kendi tipimiz için bastırdığımızda derleme zamanında bir uyarı aldığımızı görürüz.
     //Uyarıda, sınıfımızın Object’ ten gelen GetHashCode metodunu bastırması(override) önerilmektedir.
        public Product(int _Id, string _Name, string _LastName)
        {
            Id = _Id;
            Name = _Name;
            LastName = _LastName;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

        //kendi sınıflarımızı yazdığımız takdirde içeriklere göre eşitlikleri kontrol etmek istiyorsak(yani değer tabanlı eşitlik modelini uygulamak istiyorsak)
        //object sınıfının sanal olan equals metodunu bastırmamız(override) gerekecektir.


        public override bool Equals(object obj)
        {
            Product product = (Product)obj;
            if ((product.Id == this.Id) && (product.Name == this.Name) && (product.LastName == this.LastName))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        //İşte Equals metodunu bastırır bastırmaz istediğimiz sonucu aldık ve eşitliği kendi amacımıza yönelik kurguladık.


        //İşte bu durumu düzeltmek için Equals metodu bastırılmış bir class’ın GetHashCode metoduda bastırılmalıdır.
        public override string ToString()
        {
            return Id.ToString() + " " + Name.ToString() + " " + LastName.ToString();
        }
        public override int GetHashCode()
        {
            return this.ToString().GetHashCode();
        }
    }
}
