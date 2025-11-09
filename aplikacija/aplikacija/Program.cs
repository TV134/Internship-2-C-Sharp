namespace aplikacija
{
    internal class Program
    {
        static void Main(string[] args)
        {

            var users= new Dictionary<int, Tuple<string,string,DateTime,List<int>>>
            {
                {1, new Tuple<string, string, DateTime, List<int>>("Ana","Anić",new DateTime(2000,12,15),new List<int>{1,2,3})},
                {2, new Tuple<string, string, DateTime, List<int>>("Mate","Matić",new DateTime(1992,1,20),new List<int>{5})},
                {3, new Tuple<string, string, DateTime, List<int>>("Iva","Ivić",new DateTime(2000,12,15),new List<int>{3,4})}
            };


            string menu = "";
            do
            {
        
                Console.WriteLine("APLIKACIJA ZA EVIDENCIJU GORIVA\n1 - Korisnici\r\n2 - Putovanja\r\n0 - Izlaz iz aplikacije\r\n");

                Console.Write("Odabir: ");
                menu = Console.ReadLine();

                switch (menu)
                {
                    case "1":
                        UserSelector();
                        break;

                    case "2":
                        TripSelector();
                        break;

                    case "0":
                        Console.WriteLine("Kraj");
                        break;

                    default:
                        Console.WriteLine("Krivi unos");
                        break;
                }
            }
            while (menu!="0");



            static void UserSelector()
            {
                string userMenu = "";
                do
                {
                    Console.WriteLine("1 - Unos novog korisnika\r\n2 - Brisanje korisnika\r\n3 - Uređivanje korisnika\r\n4 - Pregled svih korisnika\r\n0 - Povratak na glavni izbornik\r\n");

                    Console.Write("Odabir: ");
                    userMenu = Console.ReadLine();

                    switch (userMenu)
                    {
                        case "1":
                            break;

                        case "2":
                            break;

                        case "3":
                            break;

                        case "4":
                            break;

                        case "0":
                            Console.WriteLine("Vraćanje na glavni izbornik");
                            break;

                        default:
                            Console.WriteLine("Krivi unos");
                            break;
                    }
                }
                while (userMenu != "0");
                Console.Clear();
            }

            static void TripSelector()
            {
                string tripMenu = "";
                do
                {
                    Console.WriteLine("1 - Unos novog putovanja\r\n2 - Brisanje putovanja\r\n3 - Uređivanje postojećeg putovanja\r\n4 - Pregled svih putovanja\r\n5 - Izvještaji i analize\r\n0 - Povratak na glavni izbornik\r\n");

                    Console.Write("Odabir: ");
                    tripMenu = Console.ReadLine();

                    switch (tripMenu)
                    {
                        case "1":
                            break;

                        case "2":
                            break;

                        case "3":
                            break;

                        case "4":
                            break;

                        case "5":
                            break;

                        case "0":
                            Console.WriteLine("Vraćanje na glavni izbornik");
                            break;

                        default:
                            Console.WriteLine("Krivi unos");
                            break;
                    }
                }
                while (tripMenu != "0");
                Console.Clear();
            }



        }
    }
}
