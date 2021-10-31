using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite.Geometries;
using PWP.Domain;
using PWP.Infra;

namespace PWPConsole
{
    class Program
    {
        static int div = 10000000;
        static int minutdistance = 60;

        static string AGLABO = "LABO";
        static string AGELEC = "ELEC";
        static string AGANES = "ANES";
        
        static void Main(string[] args)
        {
            //SomeThings();
            //PopulateDb();

            //Test1_FirstPlanner();
            //Test2_SecondPlanner_NotOrdened();
            Test3_ThirdPlanner_MoreThanOne();

            Console.WriteLine("Fi");
            

        }

        private static void Test1_FirstPlanner()
        {
            Console.WriteLine("*** First Planner ***");
            using (var ctx = CreateDbContext())
            {
                var queryLAB = ctx.Diatari.Where(w => w.Agenda.Equals(AGLABO));
                var queryELEC = ctx.Diatari.Where(w => w.Agenda.Equals(AGELEC));
                var queryANES = ctx.Diatari.Where(w => w.Agenda.Equals(AGANES));

                var query = from lab in queryLAB
                            from elec in queryELEC
                            from anes in queryANES
                            where
                            lab.PuntFi.Distance(elec.PuntInici) > (minutdistance * 10)
                            &&
                            lab.PuntFi.Distance(elec.PuntInici) < (minutdistance * 30)
                            &&
                            elec.PuntFi.Distance(anes.PuntInici) > (minutdistance * 10)
                            &&
                            elec.PuntFi.Distance(anes.PuntInici) < (minutdistance * 60)
                            &&
                            lab.PuntFi.Y < anes.PuntInici.Y
                            &&
                            elec.PuntFi.Y < anes.PuntInici.Y

                            select new
                            {
                                age1Code = lab.Agenda,
                                age1Data = lab.DataInici,
                                age1Hora = lab.HoraInici,
                                age1Minuts = lab.Minuts,
                                age1Start = lab.PuntInici,
                                age1End = lab.PuntFi,
                                age2Code = elec.Agenda,
                                age2Data = elec.DataInici,
                                age2Hora = elec.HoraInici,
                                age2Minuts = elec.Minuts,
                                age2Start = elec.PuntInici,
                                age2End = elec.PuntFi,
                                age3Code = anes.Agenda,
                                age3Data = anes.DataInici,
                                age3Hora = anes.HoraInici,
                                age3Minuts = anes.Minuts,
                                age3Start = anes.PuntInici,
                                age3End = anes.PuntFi,


                            };

                var result = query.FirstOrDefault();

                if (result != null)
                {

                    Paint(result.age1Code, result.age1Data, result.age1Hora, result.age1Minuts, result.age1Start, result.age1End);
                    Paint(result.age2Code, result.age2Data, result.age2Hora, result.age2Minuts, result.age2Start, result.age2End);
                    Paint(result.age3Code, result.age3Data, result.age3Hora, result.age3Minuts, result.age3Start, result.age3End);


                }





            }
            Console.WriteLine("****************************************");
        }

  
   

 
        private static void Test2_SecondPlanner_NotOrdened()
        {
            Console.WriteLine("*** Second Planner ***");
            using (var ctx = CreateDbContext())
            {
                var query1 = ctx.Diatari.OrderBy(o => o.DataInici);
                var query2 = ctx.Diatari;
                var query3 = ctx.Diatari;

                var query = from q1 in query1
                            from q2 in query2
                            from q3 in query3
                            where
                            q1.PuntFi.Distance(q2.PuntInici) < (minutdistance * 30)
                            &&
                            q2.PuntFi.Distance(q3.PuntInici) < (minutdistance * 60)
                            &&
                            q1.PuntFi.Y < q2.PuntInici.Y
                            &&
                            q2.PuntFi.Y < q3.PuntInici.Y

                            && !q2.Agenda.Equals(q1.Agenda)
                            && !q3.Agenda.Equals(q2.Agenda)
                            && !q3.Agenda.Equals(q1.Agenda)
                            select new
                            {
                                age1Code = q1.Agenda,
                                age1Data = q1.DataInici,
                                age1Hora = q1.HoraInici,
                                age1Minuts = q1.Minuts,
                                age1Start = q1.PuntInici,
                                age1End =q1.PuntFi,
                                age2Code = q2.Agenda,
                                age2Data = q2.DataInici,
                                age2Hora = q2.HoraInici,
                                age2Minuts = q2.Minuts,
                                age2Start = q2.PuntInici,
                                age2End = q2.PuntFi,
                                age3Code = q3.Agenda,
                                age3Data = q3.DataInici,
                                age3Hora = q3.HoraInici,
                                age3Minuts=q3.Minuts,
                                age3Start = q3.PuntInici,
                                age3End = q3.PuntFi,


                            };

                var result = query.FirstOrDefault();

                if (result != null)
                {

                    Paint(result.age1Code, result.age1Data, result.age1Hora, result.age1Minuts, result.age1Start, result.age1End);
                    Paint(result.age2Code, result.age2Data, result.age2Hora, result.age2Minuts, result.age2Start, result.age2End);
                    Paint(result.age3Code, result.age3Data, result.age3Hora, result.age3Minuts, result.age3Start, result.age3End);

                }




            }
            Console.WriteLine("****************************************");
        }

        private static void Test3_ThirdPlanner_MoreThanOne()
        {
            Console.WriteLine("*** Thrid Planner ***");
            using (var ctx = CreateDbContext())
            {
                var queryLAB = ctx.Diatari.Where(w => w.Agenda.Equals(AGLABO)).OrderBy(o => o.DataInici);
                var queryELEC = ctx.Diatari.Where(w => w.Agenda.Equals(AGELEC)).OrderBy(o => o.DataInici);
                var queryANES = ctx.Diatari.Where(w => w.Agenda.Equals(AGANES)).OrderBy(o => o.DataInici);

                var query = from lab in queryLAB
                            from elec in queryELEC
                            from anes in queryANES
                            where
                            lab.PuntFi.Distance(elec.PuntInici) > (minutdistance * 10)
                            &&
                            lab.PuntFi.Distance(elec.PuntInici) < (minutdistance * 30)
                            &&
                            elec.PuntFi.Distance(anes.PuntInici) > (minutdistance * 10)
                            &&
                            elec.PuntFi.Distance(anes.PuntInici) < (minutdistance * 60)
                            &&
                            lab.PuntFi.Y < anes.PuntInici.Y
                            &&
                            elec.PuntFi.Y < anes.PuntInici.Y
                            select new
                            {
                                age1Code = lab.Agenda,
                                age1Data = lab.DataInici,
                                age1Hora = lab.HoraInici,
                                age1Minuts = lab.Minuts,
                                age1Start = lab.PuntInici,
                                age1End = lab.PuntFi,
                                age2Code = elec.Agenda,
                                age2Data = elec.DataInici,
                                age2Hora = elec.HoraInici,
                                age2Minuts = elec.Minuts,
                                age2Start = elec.PuntInici,
                                age2End = elec.PuntFi,
                                age3Code = anes.Agenda,
                                age3Data = anes.DataInici,
                                age3Hora = anes.HoraInici,
                                age3Minuts = anes.Minuts,
                                age3Start = anes.PuntInici,
                                age3End = anes.PuntFi,

                            };



                var result = query.Take(20).ToArray();

                if (result != null)
                {
                    foreach (var res in result)
                    {
                        Console.WriteLine("---------------------------------------------");
                        Paint(res.age1Code, res.age1Data, res.age1Hora, res.age1Minuts, res.age1Start, res.age1End);
                        Paint(res.age2Code, res.age2Data, res.age2Hora, res.age2Minuts, res.age2Start, res.age2End);
                        Paint(res.age3Code, res.age3Data, res.age3Hora, res.age3Minuts, res.age3Start, res.age3End);
                        Console.WriteLine("---------------------------------------------");

                    }
                }



            }
            Console.WriteLine("****************************************");
        }


        private static DbCtx CreateDbContext()
        {
            //var connectionstring = "Server=MINOS.althaia.cat\\APPs;Database=PWP;User Id=pwp;Password=@pocpwp;";
            var connectionstring = "Server=localhost;Database=PWP;User Id=sa;Password=@thisIsAp0c";


            var optionsBuilder = new DbContextOptionsBuilder<DbCtx>();
            optionsBuilder.UseSqlServer(connectionstring, x => x.UseNetTopologySuite());

            return new DbCtx(optionsBuilder.Options);
        }

        private static void PopulateDb()
        {
 
            var startdate = new DateTime(2021, 11, 1, 0, 0, 0);

            var LABO = new
            {
                Agenda = AGLABO,
                starthour = new TimeSpan(8, 0, 0),
                endhour = new TimeSpan(10, 0, 0),
                dofweek = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }.ToList(),
                minutes = 10
            };

            var ELEC = new
            {
                Agenda = AGELEC,
                starthour = new TimeSpan(9, 0, 0),
                endhour = new TimeSpan(12, 0, 0),
                dofweek = new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Friday }.ToList(),
                minutes=30
            };

            var ANES = new
            {
                Agenda = AGANES,
                starthour = new TimeSpan(9, 0, 0),
                endhour = new TimeSpan(14, 0, 0),
                dofweek = new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Friday }.ToList(),
                minutes=20
            };

            
            using (var ctx = CreateDbContext())
            {
                var _date = startdate;

                for (var i = 0; i < 60; i++)
                {
                    // LAB
                    if (LABO.dofweek.Contains(_date.DayOfWeek))
                    {
                        var _hour = LABO.starthour;

                        do
                        {
                            long startX = _date.Ticks / div;
                            long startY = (long)(60 * minutdistance * _hour.TotalHours);
                            long endX = startX;
                            long endY = startY + (LABO.minutes * minutdistance);

                            Coordinate start = new Coordinate(startX, startY);
                            Coordinate end = new Coordinate(endX, endY);

                            var diatari = new Diatari()
                            {
                                Id = Guid.NewGuid(),
                                Agenda = LABO.Agenda,
                                DataInici = _date,
                                HoraInici = _hour,
                                HoraFi = _hour.Add(new TimeSpan(0,LABO.minutes,0)),
                                Minuts = LABO.minutes,
                                PuntInici = new Point(startX, startY),
                                PuntFi = new Point(endX, endY),
                               

                            };

                            ctx.Diatari.Add(diatari);


                            _hour += new TimeSpan(0, LABO.minutes, 0);

                        } while (_hour <= LABO.endhour);



                    }


                    // ELEC
                    if (ELEC.dofweek.Contains(_date.DayOfWeek))
                    {
                        var _hour = ELEC.starthour;

                        do
                        {
                            long startX = _date.Ticks / div;
                            long startY = (long)(60 * minutdistance * _hour.TotalHours);
                            long endX = startX;
                            long endY = startY + (ELEC.minutes * minutdistance);

                            Coordinate start = new Coordinate(startX, startY);
                            Coordinate end = new Coordinate(endX, endY);

                            var diatari = new Diatari()
                            {
                                Id = Guid.NewGuid(),
                                Agenda = ELEC.Agenda,
                                DataInici = _date,
                                HoraInici = _hour,
                                HoraFi= _hour.Add(new TimeSpan(0, ELEC.minutes, 0)),
                                Minuts = ELEC.minutes,
                                PuntInici = new Point(startX, startY),
                                PuntFi = new Point(endX, endY),
                                

                            };

                            ctx.Diatari.Add(diatari);


                            _hour += new TimeSpan(0, ELEC.minutes, 0);

                        } while (_hour <= ELEC.endhour);



                    }

                    // ANES
                    if (ANES.dofweek.Contains(_date.DayOfWeek))
                    {
                        var _hour = ANES.starthour;

                        do
                        {
                            long startX = _date.Ticks / div;
                            long startY = (long)(60 * minutdistance * _hour.TotalHours);
                            long endX = startX;
                            long endY = startY + (ANES.minutes * minutdistance);

                            Coordinate start = new Coordinate(startX, startY);
                            Coordinate end = new Coordinate(endX, endY);

                            var diatari = new Diatari()
                            {
                                Id = Guid.NewGuid(),
                                Agenda = ANES.Agenda,
                                DataInici = _date,
                                HoraInici = _hour,
                                HoraFi = _hour.Add(new TimeSpan(0, ANES.minutes, 0)),
                                Minuts = ANES.minutes,
                                PuntInici = new Point(startX, startY),
                                PuntFi =new Point(endX, endY),
                                

                            };

                            ctx.Diatari.Add(diatari);


                            _hour += new TimeSpan(0, ANES.minutes, 0);

                        } while (_hour <= ELEC.endhour);



                    }









                    _date = _date.AddDays(1);

                }


                int total=ctx.SaveChanges();
            }


        }

        private static void SomeThings()
        {
            var dtnow = DateTime.Now;

            Console.WriteLine($"Now: {dtnow.Ticks / div}");
            Console.WriteLine($"Tomorrow: {dtnow.AddDays(1).Ticks / div}");

            Console.WriteLine($"Max day: {new DateTime(2100, 12, 31, 23, 0, 0).Ticks / div}");


            var oneminute = (dtnow.AddMinutes(1) - dtnow).Ticks;

            Console.WriteLine($"One minute: {oneminute / div}");

            Console.WriteLine($"Minuts day: {(oneminute * 60 * 24) / div}");


            var diataridatahora = new DateTime(2021, 10, 10, 8, 0, 0);
            var minuts = 20;
            var startX = new DateTime(2021, 10, 10, 0, 0, 0).Ticks / div;
            var startY = (60 * oneminute * 8);
            var endX = startX;
            var endY = startY + (minuts * oneminute) ;

            var dts = diataridatahora.ToString("yyyy/MM/dd HH:mm");
            Console.WriteLine($"Diatari data hora: {dts}");

            Console.WriteLine($"Start Point: {startX},{startY}");
            Console.WriteLine($"End Point: {endX},{endY}");
            Console.ReadLine();
        }
        private static void Paint(string ageCode, DateTime ageData, TimeSpan ageHora, int ageMinuts, Point start, Point end)
        {
            string msg = $"{ageCode} {ageData.ToString("dd-MM-yyyy")} {ageHora} {ageHora.Add(new TimeSpan(0, ageMinuts, 0))}";
            msg += $" Start[{start.X},{start.Y}] End[{end.X},{end.Y}]";
            Console.WriteLine(msg);

        }

    }
}
