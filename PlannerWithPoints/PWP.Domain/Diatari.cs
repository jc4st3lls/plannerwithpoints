using System;
using NetTopologySuite.Geometries;

namespace PWP.Domain
{
    public class Diatari
    {
        // Identificador entitat
        public Guid Id { get; set; }
        // Codi Agenda
        public string Agenda { get; set; }
        // Data
        public DateTime DataInici { get; set; }
        // Hora Inici
        public TimeSpan HoraInici { get; set; }
        // Hora Fi
        public TimeSpan HoraFi { get; set; }
        // Minuts 
        public int Minuts { get; set; }
        // Punt Inicial
        public Point PuntInici { get; set; }
        // Punt Final££
        public Point PuntFi { get; set; }
       
    }
}
