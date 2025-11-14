using System.Collections.Concurrent;
using System.Diagnostics;
using System.Reflection.Metadata;
using System.Runtime.InteropServices;
using System.Security.Cryptography.X509Certificates;

namespace aplikacija
{
    internal class Program
    {
        static Dictionary<int, Tuple<string, string, DateTime, List<int>>> users = new Dictionary<int, Tuple<string, string, DateTime, List<int>>>
            {
                {1, new Tuple<string, string, DateTime, List<int>>("Ana","Anic",new DateTime(2000,12,15),new List<int>{1,2,3})},
                {2, new Tuple<string, string, DateTime, List<int>>("Mate","Matic",new DateTime(1992,1,20),new List<int>{5})},
                {3, new Tuple<string, string, DateTime, List<int>>("Iva","Ivic",new DateTime(2012,9,1),new List<int>{3,4})}
            };

        static Dictionary<int, Tuple<DateTime, double, double, double, double>> trips = new Dictionary<int, Tuple<DateTime, double, double, double, double>>
            {
                {1, new Tuple<DateTime, double, double, double, double>(new DateTime(2023,1,15), 320.5, 24.5, 2.0, 24.5*2.0) },
                {2, new Tuple<DateTime, double, double, double, double>(new DateTime(2024,2,2), 580.0, 39.0, 1.57, 39.0*1.57) },
                {3, new Tuple<DateTime, double, double, double, double>(new DateTime(2022,4,13), 145.25, 10.2, 0.99,10.2*0.99) },
                {4, new Tuple<DateTime, double, double, double, double>(new DateTime(2024,5,30), 760.128, 51.0, 1.64, 51.0*1.64) },
                {5, new Tuple<DateTime, double, double, double, double>(new DateTime(2024,7,22), 250.74, 17.5, 1.13, 17.5*1.13) }
            };
        static void Main(string[] args)
        {

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
            while (menu != "0");



            static void UserSelector()
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
                            var newUser = UserEntry();
                            int id = users.Count == 0 ? 1 : users.Keys.Max() + 1;
                            users.Add(id, newUser);
                            Console.WriteLine("Korisnik uspješno dodan.");
                            break;

                        case "2":

                            if (!Confirm("Želiš li izbrisati korisnika?"))
                                break;

                            string type = "";
                            do
                            {
                                Console.Write("Unesi 1 za brisanje po ID, 2 za brisanje po punom imenu: ");
                                type = Console.ReadLine();
                            }
                            while (type != "1" && type != "2");

                            if (DeleteUser(type))
                                Console.WriteLine("Korisnik izbrisan");
                            else
                                Console.WriteLine("Korisnik nije pronađen");

                            break;

                        case "3":
                            if (!Confirm("Želiš li urediti korisnika?"))
                                break;

                            UpdateUser();
                            break;

                        case "4":
                            UserPrint();
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

            static void TripSelector()
            {
                string tripMenu = "";
                do
                {
                    Console.Clear();
                    Console.WriteLine("1 - Unos novog putovanja\r\n2 - Brisanje putovanja\r\n3 - Uređivanje postojećeg putovanja\r\n4 - Pregled svih putovanja\r\n5 - Izvještaji i analize\r\n0 - Povratak na glavni izbornik\r\n");

                    Console.Write("Odabir: ");
                    tripMenu = Console.ReadLine();

                    switch (tripMenu)
                    {
                        case "1":
                            var newTrip = TripEntry();
                            int tripId = trips.Count == 0 ? 1 : trips.Keys.Max() + 1;
                            trips.Add(tripId, newTrip);
                            int userId;
                            do
                            {
                                userId = GetId("korisnika");
                            }
                            while (!users.ContainsKey(userId));
                            users[userId].Item4.Add(tripId);
                            Console.WriteLine("Putovanje uspješno dodano.");
                            break;

                        case "2":

                            if (!Confirm("Želiš li izbrisati putovanje?"))
                                break;

                            string type = "";
                            do
                            {
                                Console.Write("1-brisanje po ID, 2-brisanje skupljih troškova od unesenog iznoa, 3-brisanje jeftinijih troškova: ");
                                type = Console.ReadLine();
                            }
                            while (type != "1" && type != "2" && type != "3");

                            if (DeleteTrip(type))
                                Console.WriteLine("Putovanje izbrisano");
                            else
                                Console.WriteLine("Putovanje nije pronađeno");

                            break;

                        case "3":

                            if (!Confirm("Želiš li urediti putovanje?"))
                                break;

                            UpdateTrip();

                            break;

                        case "4":
                            Console.WriteLine("\na) Ispis svih putovanja\nb) Ispis putovanja po trošku uzlazno\nc) Ispis putovanja po trošku silazno\nd) Ispis putovanja po kilometraži uzlazno\ne) Ispis putovanja po kilometraži silazno\nf) Ispis putovanja po datumu uzlazno\ng) Ispis putovanja po datumu silazno");
                            Console.Write("Odabir: ");
                            string pickPrint = Console.ReadLine();
                            switch (pickPrint)
                            {
                                case "a":
                                    TripPrint(trips);
                                    break;
                                case "b":
                                    TripPrint(trips.OrderBy(trip => trip.Value.Item5).ToDictionary());
                                    break;
                                case "c":
                                    TripPrint(trips.OrderByDescending(trip => trip.Value.Item5).ToDictionary());
                                    break;
                                case "d":
                                    TripPrint(trips.OrderBy(trip => trip.Value.Item2).ToDictionary());
                                    break;
                                case "e":
                                    TripPrint(trips.OrderByDescending(trip => trip.Value.Item2).ToDictionary());
                                    break;
                                case "f":
                                    TripPrint(trips.OrderBy(trip => trip.Value.Item1).ToDictionary());
                                    break;
                                case "g":
                                    TripPrint(trips.OrderByDescending(trip => trip.Value.Item1).ToDictionary());
                                    break;
                                default:
                                    Console.WriteLine("Krivi unos");
                                    break;
                            }
                            break;

                        case "5":
                            TripAnalysis();
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
                while (tripMenu != "0");
                Console.Clear();
            }

            static Tuple<string, string, DateTime, List<int>> UserEntry()
            {
                string firstName = "";
                do
                {
                    Console.Write("Unesi ime: ");
                    firstName = Console.ReadLine();
                }
                while (firstName == "");


                string lastName = "";
                do
                {
                    Console.Write("Unesi prezime: ");
                    lastName = Console.ReadLine();
                }
                while (lastName == "");


                DateTime birthDay;
                bool validDate = false;
                do
                {
                    Console.Write("Unesi datum rođenja (YYYY/MM/DD): ");
                    validDate = DateTime.TryParse(Console.ReadLine(), out birthDay);
                }
                while (!validDate || (birthDay >= DateTime.Now || birthDay.Year < 1930));

                List<int> tripIDs = new List<int>();

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
                        break;

                }

                var newUser = new Tuple<string, string, DateTime, List<int>>(firstName, lastName, birthDay, tripIDs);

                return newUser;

            }

            static void UserPrint()
            {
                var usersInOrder = users.OrderBy(user => user.Value.Item2).ToDictionary();
                Console.WriteLine("\na) Ispis svih korisnika po abecedi");
                foreach (var user in usersInOrder)
                {
                    Console.WriteLine($"{user.Key} - {user.Value.Item1} - {user.Value.Item2} - {user.Value.Item3.ToString("dd/MM/yyyy")}");
                }

                Console.WriteLine("\nb) Ispis korisnika starijih od 20 godina");
                foreach (var user in users)
                {
                    int age = DateTime.Now.Year - user.Value.Item3.Year;
                    if (age >= 20)
                        Console.WriteLine($"{user.Key} - {user.Value.Item1} - {user.Value.Item2} - {user.Value.Item3.ToString("dd/MM/yyyy")}");
                }

                Console.WriteLine("\nc) Ispis korisnika s barem dva putovanja");
                foreach (var user in users)
                {
                    if (user.Value.Item4.Count > 1)
                        Console.WriteLine($"{user.Key} - {user.Value.Item1} - {user.Value.Item2} - {user.Value.Item3.ToString("dd/MM/yyyy")}");
                }
            }

            static bool DeleteUser(string type)
            {
                int id = 0;
                if (type == "1")
                {
                    id=GetId("korisnika");
                }

                else if (type == "2")
                {
                    Console.Write("Unesi ime: ");
                    string firstName = Console.ReadLine();

                    Console.Write("Unesi prezime: ");
                    string lastName = Console.ReadLine();

                    foreach (var user in users)
                    {
                        if (user.Value.Item1 == firstName && user.Value.Item2 == lastName)
                        {
                            id=user.Key;
                            break;
                        }
                    }
                }

                if (users.ContainsKey(id))
                {
                    users.Remove(id);
                    return true;
                }

                return false;
            }

            static void UpdateUser()
            {
                int id=GetId("korisnika");

                if (users.ContainsKey(id))
                {
                    var tuple = UserEntry();
                    users[id] = tuple;
                    Console.WriteLine("Korisnik uspješno uređen.");
                    return;
                }

                Console.WriteLine("Korisnik nije pronađen.");
            }

            static void TripPrint(Dictionary<int, Tuple<DateTime, double, double, double, double>> tripParameter)
            {
                foreach (var trip in tripParameter)
                {
                    Console.WriteLine($"Putovanje #{trip.Key}\nDatum: {trip.Value.Item1.ToString("dd/MM/yyyy")}\nKilometri: {trip.Value.Item2}\nGorivo: {trip.Value.Item3}L\nCijena po litri: {trip.Value.Item4} EUR\nUkupno: {trip.Value.Item5} EUR\n");
                }
            }

            static Tuple<DateTime, double, double, double, double> TripEntry()
            {
                DateTime travelDate;
                bool validDate = false;
                do
                {
                    Console.Write("Unesi datum putovanja (YYYY/MM/DD): ");
                    validDate = DateTime.TryParse(Console.ReadLine(), out travelDate);
                }
                while (!validDate || travelDate.Year > 2025 || travelDate.Year < 1930);


                double kilometers;
                double fuel;
                double price;

                while (true)
                {
                    Console.Write("Unesi prijeđene kilometre: ");
                    if (double.TryParse(Console.ReadLine(), out kilometers) && kilometers > 0)
                        break;
                }

                while (true)
                {
                    Console.Write("Unesi količinu potrošenog goriva: ");
                    if (double.TryParse(Console.ReadLine(), out fuel) && fuel > 0)
                        break;
                }

                while (true)
                {
                    Console.Write("Unesi cijenu goriva po litri: ");
                    if (double.TryParse(Console.ReadLine(), out price) && price > 0)
                        break;
                }

                var newTrip = new Tuple<DateTime, double, double, double, double>(travelDate, kilometers, fuel, price, TotalPrice(fuel, price));
                return newTrip;
            }

            static double TotalPrice(double fuel, double price)
            {
                return fuel * price;
            }

            static bool DeleteTrip(string type)
            {
                List<int> toDelete = new List<int>();
                if (type == "1")
                {
                    int id=GetId("putovanja");

                    if (trips.ContainsKey(id))
                    {
                        toDelete.Add(id);
                    }
                }
                else
                {
                    double budget;

                    while (true)
                    {
                        Console.Write("Unesi iznos troška: ");
                        if (double.TryParse(Console.ReadLine(), out budget))
                            break;
                    }

                    foreach (var trip in trips)
                    {
                        var total = trip.Value.Item5;
                        if ((total > budget && type == "2") || (total < budget && type == "3"))
                        {
                            toDelete.Add(trip.Key);
                        }
                    }
                }

                foreach (var IDs in toDelete)
                {
                    trips.Remove(IDs);
                    foreach (var user in users)
                    {
                        user.Value.Item4.Remove(IDs);
                    }
                }

                return toDelete.Count > 0;
            }

            static void UpdateTrip()
            {
                int id=GetId("putovanja");

                if (trips.ContainsKey(id))
                {
                    var tuple = TripEntry();
                    trips[id] = tuple;
                    Console.WriteLine("Putovanje uspješno uređeno.");
                    return;
                }

                Console.WriteLine("Putovanje nije pronađeno.");
            }

            static void TripAnalysis()
            {
                int userId;
                do
                {
                    userId = GetId("korisnika");
                }
                while (!users.ContainsKey(userId));


                List<int> userTrips = users[userId].Item4;

                if (userTrips.Count == 0)
                {
                    Console.WriteLine("Korisnik nema evidentiranih putovanja.");
                    return;
                }

                double totalFuelConsumed = 0;
                double totalPrice = 0;
                double totalKilometers = 0;
                double maxFuel = double.MinValue;
                int longestTripID = 0;

                foreach (var tripID in userTrips)
                {
                    var fuel = trips[tripID].Item3;
                    var price = trips[tripID].Item5;
                    var kilometers = trips[tripID].Item2;

                    totalFuelConsumed += fuel;
                    totalPrice += price;
                    totalKilometers += kilometers;

                    if (maxFuel < fuel)
                    {
                        maxFuel = fuel;
                        longestTripID = tripID;
                    }
                }

                var averageConsumption = (totalFuelConsumed / totalKilometers) * 100;

                Console.WriteLine("Ukupna potrošnja goriva - {0} L", totalFuelConsumed);
                Console.WriteLine("Ukupni trošak goriva - {0} EUR", totalPrice);
                Console.WriteLine("Prosječna potrošnja goriva {0:F2} L/100km", averageConsumption);
                Console.WriteLine("Putovanje #{0} ima najveću potrošnju goriva", longestTripID);

                var sameDateTrips = SameDateTrips();
                if (sameDateTrips.Count < 1)
                {
                    Console.WriteLine("Nema putovanja na unesenom datum.");
                    return;
                }

                Console.WriteLine("Putovanja koja su se dogodila na uneseni datum:");
                TripPrint(sameDateTrips);


            }

            static Dictionary<int, Tuple<DateTime, double, double, double, double>> SameDateTrips()
            {
                var foundTrips = new List<int>();

                DateTime travelDate;
                bool validDate = false;
                do
                {
                    Console.Write("Unesi datum putovanja (YYYY/MM/DD): ");
                    validDate = DateTime.TryParse(Console.ReadLine(), out travelDate);
                } while (!validDate);

                foreach (var trip in trips.Keys)
                {
                    var date = trips[trip].Item1;
                    if (date == travelDate)
                        foundTrips.Add(trip);
                }

                var dictTrips = new Dictionary<int, Tuple<DateTime, double, double, double, double>>();
                foreach (var trip in foundTrips)
                {
                    dictTrips.Add(trip, trips[trip]);
                }

                return dictTrips;
            }


            static bool Confirm(string question)
            {
                string confirm = "";
                do
                {
                    Console.Write($"{question} (Y/N): ");
                    confirm = Console.ReadLine().ToUpper();
                } while (confirm != "Y" && confirm != "N");
                return confirm == "Y";
            }

            static int GetId(string type)
            {
                int id;
                while (true)
                {
                    Console.Write("Unesi id {0}: ",type);
                    if (int.TryParse(Console.ReadLine(), out id))
                        break;
                }
                return id;
            }



        }
    }
}
