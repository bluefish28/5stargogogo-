using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Model
    {
        public string MODELID { get; set; }
        public string MODELNAME { get; set; }
        public string MODELSIZE { get; set; }
        public string MODELADDRESS { get; set; }
        public string MODELFORMAT { get; set; }
        public string MODELUPDATETIME { get; set; }
        public string UPDATERID { get; set; }
        public int MODELTYPE { get; set; }
        public double POSITIONX { get; set; }
        public double POSITIONY { get; set; }
        public double POSITIONZ { get; set; }
        public string PROJECT { get; set; }
    }
}
