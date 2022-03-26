using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class HandlePhase
    {
        public int HandlePhaseId { get; set; }
        public string PhaseName { get; set; }
        public bool IsCancelAvaliable { get; set; }
    }
}
