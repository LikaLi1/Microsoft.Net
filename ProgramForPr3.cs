using Geametry;
using AAAAAAAAAA;
using mIc.MegaGigaCompany.CompanyTELEGRAM;
using mIc.MegaGigaCompany.CompanyMAX;
using RabTELEGA = mIc.MegaGigaCompany.CompanyTELEGRAM.Rab;
using RabMAX = mIc.MegaGigaCompany.CompanyMAX.Rab;

namespace Consolica
{
    internal class Hipe
    {
        static void Main()
        {
            var cicle = new Circle();
            Console.WriteLine(cicle.CircleArea(25));

            var pu = new PuPuPu();
            Console.WriteLine(pu.MiaMora(" МИАМОРА"));
            Console.ReadKey();

            var radius = new Circle();
            Console.WriteLine(radius.CircleRadius(3));

            var company = new mIc.MegaGigaCompany.CompanyTELEGRAM.KazinoBot();
            Console.WriteLine(company.Dolgi(" выбор очевиден"));
            Console.ReadKey();

            var RabVCompanyTelegram = new RabTELEGA { num = 1 };
            var RabVCompanyMax = new RabMAX { num = 2, Birka = "Дуров" };
            Console.WriteLine(RabVCompanyTelegram.num);
            Console.WriteLine(RabVCompanyMax.Birka);
            Console.WriteLine(Parkovka.LovitDajeNaParkovke());
            Console.ReadKey();
        }
    }
}
