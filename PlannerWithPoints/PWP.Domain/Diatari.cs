using System;
using NetTopologySuite.Geometries;

namespace PWP.Domain
{
    public class Diatari
    {
        public Guid Id { get; set; }
        public string Agenda { get; set; }
        public DateTime DataInici { get; set; }
        public TimeSpan HoraInici { get; set; }
        public int Minuts { get; set; }
        public Point PuntInici { get; set; }
        public Point PuntFi { get; set; }

       
    }
}
