using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace ROExpress_GUI.Andrei
{
    internal class EntityController
    {

        private ROExpressEntities _DataContext;

        private string ExtractNumberFromID(string input)
        {
            string pattern = @"[^\d]*(\d+)";
            Match match = Regex.Match(input, pattern);

            if (match.Success)
            {
                return match.Groups[1].Value;
            }
            return input;
        }

        private double CalculateDistance(double lat1, double lon1, double lat2, double lon2)
        {
            const double R = 6371;
            var dLat = ToRadians(lat2 - lat1);
            var dLon = ToRadians(lon2 - lon1);
            var a = Math.Sin(dLat / 2) * Math.Sin(dLat / 2) + Math.Cos(ToRadians(lat1)) * Math.Cos(ToRadians(lat2)) * Math.Sin(dLon / 2) * Math.Sin(dLon / 2);
            var c = 2 * Math.Atan2(Math.Sqrt(a), Math.Sqrt(1 - a));
            var distance = R * c;
            return distance;
        }

        private double ToRadians(double angle)
        {
            return Math.PI * angle / 180.0;
        }
        public double GetTravelDistanceEntity(string departures, string arrival)
        {
            using (var dbContext = new ROExpressEntities())
            {
                var resultDepartures = dbContext.Statiis
                    .Where(station => station.Nume_Oras == departures)
                    .Select(station => new
                    {
                        Name = station.Nume_Oras,
                        Lat = station.Latitudine,
                        Long = station.Longitudine
                    })
                    .FirstOrDefault();

                var resultArrival = dbContext.Statiis
                    .Where(station => station.Nume_Oras == arrival)
                    .Select(station => new
                    {
                        Name = station.Nume_Oras,
                        Lat = station.Latitudine,
                        Long = station.Longitudine
                    })
                    .FirstOrDefault();

                if (resultDepartures != null && resultArrival != null)
                {
                    double distance = CalculateDistance(resultDepartures.Lat, resultDepartures.Long, resultArrival.Lat, resultArrival.Long);
                    return distance;
                }

                
                return 0.0;
            }
        }
        public double GetTicketPriceEntity(string departures, string arrival, string idTren, int clasa)
        {
            double price = 0;

            using (var dbContext = new ROExpressEntities())
            {
                var resultDepartures = dbContext.Statiis
                    .Where(station => station.Nume_Oras == departures)
                    .Select(station => new
                    {
                        Name = station.Nume_Oras,
                        Lat = station.Latitudine,
                        Long = station.Longitudine
                    })
                    .FirstOrDefault();

                var resultArrival = dbContext.Statiis
                    .Where(station => station.Nume_Oras == arrival)
                    .Select(station => new
                    {
                        Name = station.Nume_Oras,
                        Lat = station.Latitudine,
                        Long = station.Longitudine
                    })
                    .FirstOrDefault();

                if (resultDepartures != null && resultArrival != null)
                {
                    double distance = CalculateDistance(resultDepartures.Lat, resultDepartures.Long, resultArrival.Lat, resultArrival.Long);

                    // Acum poți utiliza distanța pentru a calcula prețul biletului pe baza propriilor criterii.
                    // Adaugă logica ta de calcul a prețului aici.

                    // Exemplu simplu de calcul:
                    price = distance * clasa; // Just un exemplu. Trebuie să ajustezi acest calcul în funcție de nevoile tale.
                }
            }

            return price;
        }
        public void BuyTicketEntity(string idTren, string email, int clasa, DateTime date, string orasPlecare, string orasSosire, int nrSeats)
        {
            using (var dbContext = new ROExpressEntities())
            {
                

                int numberTrain = int.Parse(ExtractNumberFromID(idTren));
                
                List<Tickets> ticketsList = new List<Tickets>();

                int idBiletGenerat;

               
               int nrVagoane = (
                        from tren in dbContext.Trenuris
                        where tren.ID_Tren == numberTrain
                        join vagonTren in dbContext.Vagoane_Tren on tren.ID_Tren equals vagonTren.ID_Tren
                        join tipVagon in dbContext.Tipuri_Vagoane on vagonTren.ID_Vagon equals tipVagon.ID_Vagon
                        where tipVagon.Clasa_Vagon == clasa
                        select vagonTren.Nr_Vagon
               ).FirstOrDefault();

                int topLocVagon = (
                 from bilets in dbContext.Bilet_Tren
                 where bilets.ID_Tren == numberTrain && bilets.DataTren == date
                 orderby bilets.LocVagon descending
                 select bilets.LocVagon
                     ).DefaultIfEmpty(0).FirstOrDefault();

                for (int i = 0; i < nrSeats; i++)
                {

                    topLocVagon += 1;
                    Bilet_Tren bilet = new Bilet_Tren
                    {
                        ID_Tren = numberTrain,
                        Email = email,
                        NumarVagon = nrVagoane,
                        LocVagon = topLocVagon ,
                        Clasa = clasa,
                        DataTren = date
                    };

                    try
                    {
                        dbContext.Bilet_Tren.Add(bilet);
                        dbContext.SaveChanges();
                        idBiletGenerat = bilet.ID_Bilet_Tren;
                        Tickets ticket = new Tickets();

                        ticket.ID_Bilet = idBiletGenerat;
                        ticket.ID_Tren = idTren;
                        ticket.Oras_Plecare = orasPlecare;
                        ticket.Oras_Sosire = orasSosire;
                        ticket.Data = date;
                        ticket.Email = email;
                        ticket.NumarVagon = bilet.NumarVagon;
                        ticket.LocVagon = bilet.LocVagon;
                        ticket.Clasa = clasa;

                        ticketsList.Add(ticket);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }

                SmtpController smtp = new SmtpController();
                smtp.SendMessage(ticketsList);
            }
        }

        public TrainSeatAvailability GetTrainSeatAvailability(string trainID, DateTime date)
        {
            TrainSeatAvailability trainSeatAvailability = new TrainSeatAvailability();

            int numberTrain = int.Parse(ExtractNumberFromID(trainID));

            using (var dbContext = new ROExpressEntities())
            {
                var result = from tren in dbContext.Trenuris
                             join vagonTren in dbContext.Vagoane_Tren on tren.ID_Tren equals vagonTren.ID_Tren
                             join tipVagon in dbContext.Tipuri_Vagoane on vagonTren.ID_Vagon equals tipVagon.ID_Vagon
                             where string.Concat(tren.Tip_Tren, tren.ID_Tren) == trainID
                             group tipVagon by tipVagon.Clasa_Vagon into grup
                             select new
                             {
                                 Clasa_Vagon = grup.Key,
                                 Suma_Nr_Locuri = grup.Sum(tv => tv.Nr_Locuri)
                             };

                foreach (var item in result)
                {
                    if (item.Clasa_Vagon == 1)
                    {
                        trainSeatAvailability.SeatsFirstClass = item.Suma_Nr_Locuri;
                    }
                    if (item.Clasa_Vagon == 2)
                    {
                        trainSeatAvailability.SeatsSecondClass = item.Suma_Nr_Locuri;
                    }
                }

                var resultExists = from bilet in dbContext.Bilet_Tren
                                   where bilet.ID_Tren == numberTrain && bilet.DataTren == date
                                   group bilet by bilet.Clasa into grup
                                   select new
                                   {
                                       Clasa = grup.Key,
                                       Count = grup.Count()
                                   };

                foreach (var item in resultExists)
                {
                    if (item.Clasa == 1)
                    {
                        trainSeatAvailability.SeatsFirstClass -= item.Count;
                    }
                    if (item.Clasa == 2)
                    {
                        trainSeatAvailability.SeatsSecondClass -= item.Count;
                    }
                }
            }

            return trainSeatAvailability;
        }


        public List<RailwayStations> GetRailwayStationsEntity()
        {
            using (var dbContext = new ROExpressEntities())
            {
                var railwayStations = (
                    from station in dbContext.Statiis
                    select new RailwayStations
                    {
                        City_Name = station.Nume_Oras,
                        City_Latitude = (float)station.Latitudine,
                        City_Longitude = (float)station.Longitudine
                    }
                ).ToList();

                return railwayStations;
            }
        }

        public List<TrainDepartures> GetTrainDeparturesEntity(string station)
        {
            using (var dbContext = new ROExpressEntities())
            {
                var listTrains = (
                    from statieTren in dbContext.Statii_Tren
                    join statie in dbContext.Statiis on statieTren.ID_Statie equals statie.ID_Statie
                    where statie.Nume_Oras == station
                    select new
                    {
                        ID_Tren = statieTren.ID_Tren,
                        Ora_Plecare = statieTren.Ora_Plecare
                    }
                ).ToList();

                var result = (
                    from train in listTrains
                    let query = (
                        from tren in dbContext.Trenuris
                        join statieTren in dbContext.Statii_Tren on tren.ID_Tren equals statieTren.ID_Tren
                        join statie in dbContext.Statiis on statieTren.ID_Statie equals statie.ID_Statie
                        where tren.ID_Tren == train.ID_Tren
                        orderby statieTren.Ora_Sosire descending
                        select new
                        {
                            TrenID = tren.Tip_Tren + tren.ID_Tren,
                            NumeOras = statie.Nume_Oras,
                            OraSosire = statieTren.Ora_Sosire
                        }
                    ).Take(1).SingleOrDefault()
                    orderby train.Ora_Plecare
                    select new TrainDepartures
                    {
                        HourDepartures = train.Ora_Plecare.ToString(),
                        Train = query.TrenID.ToString(),
                        FinalyStation = query.NumeOras.ToString()
                    }
                ).ToList();

                return result;
            }
        }

        public List<TrainArrivals> GetTrainArrivalsEntity(string station)
        {
            using (var dbContext = new ROExpressEntities())
            {
                var trainsArrival = (
                    from statieTren in dbContext.Statii_Tren
                    join statie in dbContext.Statiis on statieTren.ID_Statie equals statie.ID_Statie
                    where statie.Nume_Oras == station
                    select new
                    {
                        ID_Tren = statieTren.ID_Tren,
                        OraSosire = statieTren.Ora_Sosire
                    }
                ).ToList();

                var result = (
                    from item in trainsArrival
                    let query = (
                        from tren in dbContext.Trenuris
                        join statieTren in dbContext.Statii_Tren on tren.ID_Tren equals statieTren.ID_Tren
                        join statie in dbContext.Statiis on statieTren.ID_Statie equals statie.ID_Statie
                        where tren.ID_Tren == item.ID_Tren
                        orderby statieTren.Ora_Sosire ascending
                        select new
                        {
                            TrenID = tren.Tip_Tren + tren.ID_Tren,
                            NumeOras = statie.Nume_Oras,
                            OraSosire = statieTren.Ora_Sosire
                        }
                    ).Take(1).SingleOrDefault()
                    orderby query.OraSosire
                    select new TrainArrivals
                    {
                        HourArrival = item.OraSosire.ToString(),
                        Train = query.TrenID.ToString(),
                        StartStation = query.NumeOras.ToString()
                    }
                ).ToList();

                return result;
            }
        }
        public List<GetCityHops_Result> GetCityHopsEntity(string IDTren)
        {
            _DataContext = new ROExpressEntities();

            var query = from cityHop in _DataContext.GetCityHops(IDTren)
                        select cityHop;

            return query.ToList();

        }

        public void PrintCityHops(List<GetCityHops_Result> list)
        {
            foreach (var item in list)
            {
                Console.Write(item.Nume_Oras);
                Console.Write("\t");
                Console.Write(item.Latitudine);
                Console.Write("\t");
                Console.Write(item.Longitudine);
                Console.Write("\n");
            }

        }

        public List<TrainJourney> GetTrainJourneysEntity(string departuresStation, string arrivalStation)
        {

            _DataContext = new ROExpressEntities();

            var query = from journey in _DataContext.GetJourney(departuresStation, arrivalStation)
                        select new TrainJourney
                        {
                            IDTren = $"{journey.TipTren}{journey.ID_Tren}",
                            Departures = journey.Statie_Plecare.ToString(),
                            DepartureTime = journey.Ora_Plecare.ToString(),
                            Arrival = journey.Nume_Oras.ToString(),
                            ArrivalTime = journey.Ora_Sosire.ToString()
                        };

            return query.ToList();

        }
        public void PrintJourneyTrainEntity(List<TrainJourney> journey)
        {
            foreach (TrainJourney train in journey)
            {
                Console.Write(train.IDTren);
                Console.Write('\t');
                Console.Write(train.Departures);
                Console.Write('\t');
                Console.Write(train.DepartureTime);
                Console.Write('\t');
                Console.Write(train.Arrival);
                Console.Write('\t');
                Console.Write(train.ArrivalTime);
                Console.Write('\t');
                Console.Write('\n');

            }
        }

    }
}
