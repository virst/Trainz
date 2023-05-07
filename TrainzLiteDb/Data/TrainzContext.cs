using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Intrinsics.Arm;
using System.Text;
using System.Threading.Tasks;
using TrainzLib.Models;

namespace TrainzLiteDb.Data
{
    public class TrainzContext : DbContext
    {
        public const string Fn = "trainz.db";

        public DbSet<Way> Ways { get; set; }
        public DbSet<VagonType> VagonTypes { get; set; }
        public DbSet<VagonInfo> VagonInfos { get; set; }
        public DbSet<Station> Stations { get; set; }
        public DbSet<GruzType> GruzTypes { get; set; }
        public DbSet<Vagon> Vagons { get; set; }

        public bool InitRun { get; }


        protected override void OnConfiguring(DbContextOptionsBuilder options)
           => options
           .UseSqlite("Data Source=" + Fn);

        public TrainzContext()
        {
            Console.WriteLine("Data Source=" + Fn);
            InitRun = Database.EnsureCreated();

            if (InitRun)
            {
                foreach (var g in GruzType.GetBaseList())
                    GruzTypes.Add(g);

                foreach (var v in VagonType.GetBaseList())
                    VagonTypes.Add(v);

                SaveChanges();


                var st = new Station() { Name = "Город Пассажирская", Description = "Ну просто станция" };
                st.Ways = new List<Way>()
                  {
                    new Way() {Num = 1, Description = "Путь 1"},
                    new Way() {Num = 2, Description = "Путь 2"},
                    new Way() {Num = 3, Description = "Путь 3"},
                  };
                Stations.Add(st);     
                foreach (var w in st.Ways)
                {
                    w.StationId = st.Id;                    
                }         
                st = new Station() { Name = "Город Грузовая", Description = "Не просто станция, а другая танция" };
                st.Ways = new List<Way>()
                   {
                    new Way() { Num = 1, Description = "Way 1"},
                    new Way() { Num = 2, Description = "Way 2"},
                    new Way() { Num = 3, Description = "Way 3"},
                    new Way() { Num = 4, Description = "Way 4"},
                    new Way() { Num = 5, Description = "Way 5"},
                  };
                Stations.Add(st);
               
                foreach (var w in st.Ways)
                {
                    w.StationId = st.Id;                   
                }
                SaveChanges();
            }
        }
    }
}
