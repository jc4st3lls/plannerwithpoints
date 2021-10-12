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
        
        static void Main(string[] args)
        {
            //SomeThings();
            //PopulateDb();

            Test1();


            Console.WriteLine("Fi");
            

        }

        private static void Test1()
        {
            using (var ctx = CreateDbContext())
            {
                var queryLAB = ctx.Diatari.Where(w => w.Agenda.Equals("LAB"));
                var queryELEC = ctx.Diatari.Where(w => w.Agenda.Equals("ELEC"));
                var queryANES = ctx.Diatari.Where(w => w.Agenda.Equals("ANES"));

                var query = from lab in queryLAB
                            from elec in queryELEC
                            from anes in queryANES
                            where lab.PuntFi.Distance(elec.PuntInici) < (minutdistance * 30)
                            && elec.PuntFi.Distance(anes.PuntInici) < (minutdistance * 60)
                            select new
                            {
                                age1Code = lab.Agenda,
                                age1Data = lab.DataInici,
                                age1Hora = lab.HoraInici,
                                age2Code = elec.Agenda,
                                age2Data = elec.DataInici,
                                age2Hora = elec.HoraInici,
                                age3Code = anes.Agenda,
                                age3Data = anes.DataInici,
                                age3Hora = anes.HoraInici

                            };

                var result = query.FirstOrDefault();

                if (result != null)
                {
                    Console.WriteLine($"{result.age1Code} {result.age1Data.ToString("dd-MM-yyyy")} {result.age1Hora}");
                    Console.WriteLine($"{result.age2Code} {result.age2Data.ToString("dd-MM-yyyy")} {result.age2Hora}");
                    Console.WriteLine($"{result.age3Code} {result.age3Data.ToString("dd-MM-yyyy")} {result.age3Hora}");

                }




            }
        }

        private static DbCtx CreateDbContext()
        {
            var connectionstring = "Server=MINOS.althaia.cat\\APPs;Database=PWP;User Id=pwp;Password=@pocpwp;";

            var optionsBuilder = new DbContextOptionsBuilder<DbCtx>();
            optionsBuilder.UseSqlServer(connectionstring, x => x.UseNetTopologySuite());

            return new DbCtx(optionsBuilder.Options);
        }

        private static void PopulateDb()
        {
 
            var startdate = new DateTime(2021, 11, 1, 0, 0, 0);

            var LABO = new
            {
                Agenda = "LAB",
                starthour = new TimeSpan(8, 0, 0),
                endhour = new TimeSpan(10, 0, 0),
                dofweek = new[] { DayOfWeek.Monday, DayOfWeek.Tuesday, DayOfWeek.Wednesday, DayOfWeek.Thursday, DayOfWeek.Friday }.ToList(),
                minutes = 10
            };

            var ELEC = new
            {
                Agenda = "ELEC",
                starthour = new TimeSpan(9, 0, 0),
                endhour = new TimeSpan(12, 0, 0),
                dofweek = new[] { DayOfWeek.Tuesday, DayOfWeek.Thursday, DayOfWeek.Friday }.ToList(),
                minutes=30
            };

            var ANES = new
            {
                Agenda = "ANES",
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
                            long startY = (long)((60 * minutdistance * _hour.TotalHours) / div);
                            long endX = startX;
                            long endY = startY + ((LABO.minutes * minutdistance) / div);



                            var diatari = new Diatari()
                            {
                                Id = Guid.NewGuid(),
                                Agenda = LABO.Agenda,
                                DataInici = _date,
                                HoraInici = _hour,
                                Minuts = LABO.minutes,
                                PuntInici = new Point(startX, startY),
                                PuntFi = new Point(endX, endY)

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
                            long startY = (long)((60 * minutdistance * _hour.TotalHours) / div);
                            long endX = startX;
                            long endY = startY + ((ELEC.minutes * minutdistance) / div);



                            var diatari = new Diatari()
                            {
                                Id = Guid.NewGuid(),
                                Agenda = ELEC.Agenda,
                                DataInici = _date,
                                HoraInici = _hour,
                                Minuts = ELEC.minutes,
                                PuntInici = new Point(startX, startY),
                                PuntFi = new Point(endX, endY)

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
                            long startY = (long)((60 * minutdistance * _hour.TotalHours) / div);
                            long endX = startX;
                            long endY = startY + ((ANES.minutes * minutdistance) / div);



                            var diatari = new Diatari()
                            {
                                Id = Guid.NewGuid(),
                                Agenda = ANES.Agenda,
                                DataInici = _date,
                                HoraInici = _hour,
                                Minuts = ANES.minutes,
                                PuntInici = new Point(startX, startY),
                                PuntFi = new Point(endX, endY)

                            };

                            ctx.Diatari.Add(diatari);


                            _hour += new TimeSpan(0, ANES.minutes, 0);

                        } while (_hour <= ELEC.endhour);



                    }









                    _date = _date.AddDays(1);

                }


                ctx.SaveChanges();
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
            var startY = (60 * oneminute * 8) / div;
            var endX = startX;
            var endY = startY + ((minuts * oneminute) / div);

            var dts = diataridatahora.ToString("yyyy/MM/dd HH:mm");
            Console.WriteLine($"Diatari data hora: {dts}");

            Console.WriteLine($"Start Point: {startX},{startY}");
            Console.WriteLine($"End Point: {endX},{endY}");
            Console.ReadLine();
        }
    }
}
