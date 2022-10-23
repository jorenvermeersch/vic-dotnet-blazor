using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public static class Availability
    {
        public enum DayOfWeek
        {
            Maandag,
            Dinsdag,
            Woensdag,
            Donderdag,
            Vrijdag,
            Zaterdag,
            Zondag
        }

        public enum PartOfDay
        {
            Voormiddag,
            Namiddag,
            Hele_dag
        }
    }
}
