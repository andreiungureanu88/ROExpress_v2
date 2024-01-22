using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROExpress_GUI.Andrei
{
    public class TrainJourney
    { 
        public string IDTren {  get; set; } 
        public string Departures { get; set; } 
        public string DepartureTime {  get; set; } 
         
        public string Arrival {  get; set; }
        public string ArrivalTime {  get; set; } 
         
        public DateTime Date { get; set; }
        public float PriceTrain {  get; set; } 
        

    }
}
