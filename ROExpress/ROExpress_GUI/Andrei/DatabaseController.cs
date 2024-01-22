using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Runtime.CompilerServices;
using System.CodeDom;

namespace ROExpress_GUI.Andrei
{
   public class DatabaseController
    {
        #region private

        private static SqlConnection _conn {  get; set; }

        #endregion

        #region public
        public static void InitializeDataBase()
        {
            string connString = @"Data Source=DESKTOP-18891JU;Initial Catalog=ROExpress;Integrated Security=True";
                       
            _conn = new SqlConnection(connString);

            Console.WriteLine("Openning Connection ... ");
            
            _conn.Open(); 
            
            if(_conn.State == System.Data.ConnectionState.Open) 
            {
                Console.WriteLine("Connection successful..."); 
                _conn.Close();
            }
            else
            {
                Console.WriteLine("Error database");
            }     
         

        }  

        public  List<TrainJourney> GetTrainJourneys(string departuresStation,string arrivalStation)
        {

            List<TrainJourney> trainJourneys = new List<TrainJourney>();
            _conn.Open();

            if (_conn.State != System.Data.ConnectionState.Open)
            {
                Console.WriteLine("Error database");
            }
            else
            {
                string query = $@"
                        WITH StatiePlecare as (
                            SELECT Statii.Nume_Oras as 'Statie Plecare', Tren.Tip_Tren as 'TipTren', Statii_Tren.ID_Tren as 'ID_Tren', Ora_Plecare as 'Ora Plecare'
                            FROM Statii_Tren
                            INNER JOIN Statii
                            ON Statii_Tren.ID_Statie = Statii.ID_Statie
                            INNER JOIN Trenuri Tren
                            ON Tren.ID_Tren = Statii_Tren.ID_Tren
                            WHERE Statii.Nume_Oras = '{departuresStation}'
                        )
                        SELECT STATIEPLECARE.TipTren, STATIEPLECARE.ID_Tren, STATIEPLECARE.[Statie Plecare], STATIEPLECARE.[Ora Plecare], STATIEDESTINATIE.Nume_Oras, STATII_TREN.Ora_Sosire
                        FROM StatiePlecare STATIEPLECARE
                        INNER JOIN Statii_Tren STATII_TREN
                        ON STATIEPLECARE.ID_Tren = STATII_TREN.ID_Tren
                        INNER JOIN Statii STATIEDESTINATIE ON STATIEDESTINATIE.ID_Statie = STATII_TREN.ID_Statie
                        WHERE STATIEDESTINATIE.Nume_Oras = '{arrivalStation}' AND STATIEPLECARE.[Ora Plecare] < STATII_TREN.Ora_Sosire";
                SqlCommand cmd = new SqlCommand(query, _conn); 

                SqlDataReader reader = cmd.ExecuteReader(); 

                while (reader.Read())
                {
                    TrainJourney train = new TrainJourney();
                    
                    train.IDTren = reader["TipTren"].ToString()+reader["ID_Tren"];
                    train.Departures = reader["Statie Plecare"].ToString();
                    train.DepartureTime = reader["Ora Plecare"].ToString();
                    train.Arrival = reader["Nume_Oras"].ToString(); 
                    train.ArrivalTime= reader["Ora_Sosire"].ToString() ; 

                    trainJourneys.Add(train);   
                }

                _conn.Close();
            } 

            return trainJourneys;
        }
        
        public List<RailwayStations> GetRailwayStations()
        {
             
            List<RailwayStations> railwayStations = new List<RailwayStations>(); 
         
            _conn.Open();

            if(_conn.State != System.Data.ConnectionState.Open)  
            {
                Console.WriteLine("Error database"); 
            } 
            else 
            { 
                SqlCommand cmd_Station = new SqlCommand();
                cmd_Station.Connection = _conn;
                cmd_Station.CommandText = "SELECT * FROM dbo.Statii"; 
                SqlDataReader reader = cmd_Station.ExecuteReader(); 

                while(reader.Read()) 
                {
                    RailwayStations railway = new RailwayStations();

                    railway.City_Name = reader["Nume_Oras"].ToString(); 
                   
                    string latitude= reader["Latitudine"].ToString();
                    string longitude= reader["Longitudine"].ToString();


                    if (float.TryParse(latitude, out float lat))
                    {
                        railway.City_Latitude = lat;
                    }
                    else
                    {
                        Console.WriteLine("Error converse string to float");
                    }

                    if (float.TryParse(longitude,out float log))
                    {
                        railway.City_Longitude = log; 
                    }
                    else
                    {
                        Console.WriteLine("Error converse string to float");
                    }

                    railwayStations.Add( railway );
                }
                _conn.Close();
            }
       
            return railwayStations;
        } 

        public void PrintRailwayStations(List<RailwayStations> rails)
        {
            foreach(RailwayStations rail in rails)
            {
                Console.WriteLine(rail.City_Name + '\t' + rail.City_Latitude + '\t' + rail.City_Longitude);
            }
        }

        public void PrintTrainsJourney(List<TrainJourney> trains)
        {
            foreach(TrainJourney train in trains)
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
        #endregion
    }
}
