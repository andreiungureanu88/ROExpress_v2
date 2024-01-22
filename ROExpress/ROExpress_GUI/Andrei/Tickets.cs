using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ROExpress_GUI.Andrei
{
    internal class Tickets
    { 
        public int ID_Bilet {  get; set; } 
        public string ID_Tren {  get; set; } 

        public string Oras_Plecare {  get; set; }

        public string Oras_Sosire { get; set; } 

        public DateTime Data { get; set; }
        public string Email { get; set; } 

        public int NumarVagon { get; set; } 

        public int LocVagon { get; set; } 

        public int Clasa { get; set; } 

    }
}
