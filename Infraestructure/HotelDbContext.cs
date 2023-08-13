using Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infraestructure
{
    public class HotelDbContext: DbContext
    {
        public HotelDbContext() { }

        public HotelDbContext(DbContextOptions<HotelDbContext> options) : base(options)
        {
        }

        public DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<Guest> Guests { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Hotel>(entity =>
            {
                entity.HasKey(e => e.HotelId);

                entity.HasMany(h => h.Rooms)
                      .WithOne(r => r.Hotel)
                      .HasForeignKey(r => r.HotelId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasKey(e => e.RoomId);

                entity.HasOne(r => r.Hotel)
                      .WithMany(h => h.Rooms)
                      .HasForeignKey(r => r.HotelId)
                      .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Reservation>(entity =>
            {
                entity.HasKey(e => e.IdReservation);

                entity.HasOne(e => e.Room)
                      .WithMany(r => r.Reservations)
                      .HasForeignKey(e => e.RoomId)
                      .OnDelete(DeleteBehavior.Cascade);

                entity.HasMany(e => e.Guests)
                      .WithOne(g => g.Reservation)
                      .HasForeignKey(g => g.IdReservation)
                      .OnDelete(DeleteBehavior.Cascade);
            });


            modelBuilder.Entity<Guest>(entity =>
            {
                entity.HasKey(e => e.GuestId);
            });

        }
    }
}