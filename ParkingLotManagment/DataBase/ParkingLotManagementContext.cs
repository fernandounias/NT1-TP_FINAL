using Microsoft.EntityFrameworkCore;
using ParkingLotManagment.Models;

namespace ParkingLotManagment.DataBase
{
    public class ParkingLotManagementContext : DbContext
    {
        /*
        private readonly string _connectionString;
        public ParkingLotContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(_connectionString);
        }
        */

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            //optionsBuilder.UseSqlServer(@"Server=(localdb)\mssqllocaldb;Database=Test;ConnectRetryCount=0");
        }*/

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(@"Server=DELL-LATITUDE-7;Database=ParkingLotDB;Trusted_Connection=True;");
        }
        */

        public ParkingLotManagementContext(DbContextOptions<ParkingLotManagementContext> options) : base(options)
        {
        }
        public DbSet<Estacionamiento> Estacionamientos { get; set; }
        public DbSet<Plan> Planes { get; set; }
        public DbSet<Vehiculo> Vehiculos { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        public DbSet<Cliente> Clientes { get; set; }
        public DbSet<Administador> Administradores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            // Acá se configura la "precision" y "scale" para la propiedad Tarifa
            //Precision: numeros total de digitos que van a ser guardados en la base de datos (contando tambien los digitos a la derecha de la coma).
            //Scale: numero de digitos a la derecha de la coma que van a ser guardados en la base de datos.
            modelBuilder.Entity<Ticket>(entity =>
            {
                entity.Property(e => e.Tarifa)
                      .HasPrecision(18, 2); // 1.000.000.000.000.000,00
            });

        }
    }
}