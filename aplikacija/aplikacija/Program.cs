using System.Collections.Concurrent;
using System.Runtime.InteropServices;

namespace aplikacija
{
    internal class Program
    {
        static void Main(string[] args)
        {

            DateTime now=new DateTime(2024,6,15);
            Console.WriteLine(now.ToString("dd/MM/yyyy"));

            var users= new Dictionary<int, Tuple<string,string,DateTime,List<int>>>
            {
                {1, new Tuple<string, string, DateTime, List<int>>("Ana","Anić",new DateTime(2000,12,15),new List<int>{1,2,3})},
                {2, new Tuple<string, string, DateTime, List<int>>("Mate","Matić",new DateTime(1992,1,20),new List<int>{5})},
                {3, new Tuple<string, string, DateTime, List<int>>("Iva","Ivić",new DateTime(2012,9,1),new List<int>{3,4})}
            };

            var trips = new Dictionary<int, Tuple<DateTime,double,double,double,double>>
            {
                {1, new Tuple<DateTime, double, double, double, double>(new DateTime(2026,1,15), 320.5, 24.5, 2.0, 24.5*2.0) },
                {2, new Tuple<DateTime, double, double, double, double>(new DateTime(2026,2,2), 580.0, 39.0, 1.57, 39.0*1.57) },
                {3, new Tuple<DateTime, double, double, double, double>(new DateTime(2026,4,13), 145.25, 10.2, 0.99,10.2*0.99) },
                {4, new Tuple<DateTime, double, double, double, double>(new DateTime(2026,5,30), 760.128, 51.0, 1.64, 51.0*1.64) },
                {5, new Tuple<DateTime, double, double, double, double>(new DateTime(2027,7,22), 250.74, 17.5, 1.13, 17.5*1.13) }
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



            void UserSelector()
            {
                string userMenu = "";
                do
                {
                    Console.Clear();
                    Console.WriteLine("1 - Unos novog korisnika\r\n2 - Brisanje korisnika\r\n3 - Uređivanje korisnika\r\n4 - Pregled svih korisnika\r\n0 - Povratak na glavni izbornik\r\n");

                    Console.Write("Odabir: ");
                    userMenu = Console.ReadLine();

                    switch (userMenu)
                    {
                        case "1":
                            var newUser=UserEntry();
                            users.Add(users.Keys.Last()+1,newUser);
                            Console.WriteLine("Korisnik uspješno dodan.");
                            break;

                        case "2":
                            string type = "";
                            do
                            {
                                Console.Write("Unesi 1 za brisanje po ID, 2 za brisanje po punom imenu: ");
                                type = Console.ReadLine();
                            }
                            while (type!="1" && type!="2");
                            
                            if (DeleteUser(type, users))
                                Console.WriteLine("Korisnik izbrisan");
                            else
                                Console.WriteLine("Korisnik nije pronađen");
                            
                            break;

                        case "3":
                            break;

                        case "4":
                            UserPrint(users);
                            break;

                        case "0":
                            Console.WriteLine("Vraćanje na glavni izbornik");
                            break;

                        default:
                            Console.WriteLine("Krivi unos");
                            break;
                    }

                    Console.WriteLine("\nKlikni tipku za nastavak");
                    Console.ReadKey();
                }
                while (userMenu != "0");
                Console.Clear();
            }

            void TripSelector()
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

            Tuple<string, string, DateTime, List<int>> UserEntry()
            {
                Console.Write("Unesi ime: ");
                string firstName=Console.ReadLine();

                Console.Write("Unesi prezime: ");
                string lastName=Console.ReadLine();

                DateTime birthDay;
                while (true)
                {
                    Console.Write("Unesi datum rođenja (YYYY/MM/DD): ");
                    if (DateTime.TryParse(Console.ReadLine(), out birthDay))
                    {
                        break;
                    }
                }

                List<int> tripIDs=new List<int>();

                while (tripIDs.Count != trips.Count)
                {
                    Console.Write("Unesi id putovanja ili nemoj upisati broj za prekid unosa: ");
                    if (int.TryParse(Console.ReadLine(), out int id))
                    {
                        if (trips.ContainsKey(id) && !tripIDs.Contains(id))
                        {
                            tripIDs.Add(id);
                        }
                    }
                    else
                    {
                        break;
                    }
                }

                var newUser= new Tuple<string,string,DateTime,List<int>>(firstName,lastName,birthDay,tripIDs);

                return newUser;

            }

            static void UserPrint(Dictionary<int, Tuple<string, string, DateTime, List<int>>> users)
            {
                Console.WriteLine("\na) Ispis svih korisnika");
                foreach (var user in users)
                {
                    Console.WriteLine($"{user.Key} - {user.Value.Item1} - {user.Value.Item2} - {user.Value.Item3.ToString("dd/MM/yyyy")}");
                }

                Console.WriteLine("\nb) Ispis korisnika starijih od 20 godina");
                foreach (var user in users)
                {
                    int age = DateTime.Now.Year - user.Value.Item3.Year;
                    if(age>=20)
                        Console.WriteLine($"{user.Key} - {user.Value.Item1} - {user.Value.Item2} - {user.Value.Item3.ToString("dd/MM/yyyy")}");
                }

                Console.WriteLine("\nc) Ispis korisnika s barem dva putovanja");
                foreach (var user in users)
                {
                    if (user.Value.Item4.Count>1)
                        Console.WriteLine($"{user.Key} - {user.Value.Item1} - {user.Value.Item2} - {user.Value.Item3.ToString("dd/MM/yyyy")}");
                }
            }

            static bool DeleteUser(string type, Dictionary<int, Tuple<string, string, DateTime, List<int>>> users)
            {
                if (type=="1")
                {
                    int id;
                    while (true)
                    {
                        Console.WriteLine("Unesi id korisnika: ");
                        if (int.TryParse(Console.ReadLine(), out id))
                            break;
                    }

                    foreach (var user in users.Keys)
                    {
                        if (user==id)
                        {
                            users.Remove(id);
                            return true;
                        }
                    }
                }

                else if(type=="2")
                {
                    Console.Write("Unesi ime: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Unesi prezime: ");
                    string lastName = Console.ReadLine();

                    foreach (var user in users)
                    {
                        if (user.Value.Item1==firstName && user.Value.Item2==lastName)
                        {
                            users.Remove(user.Key);
                            return true;
                        }
                    }
                }
                
                return false;
            }

        }
    }
}
