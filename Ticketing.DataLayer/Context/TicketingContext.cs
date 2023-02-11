using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ticketing.DataLayer.Context;
using Ticketing.DataLayer.Entities.Admin;
using Ticketing.DataLayer.Entities.Ticket;
using Ticketing.DataLayer.Entities.TicketAnswer;
using Ticketing.DataLayer.Entities.TicketQuestion;
using Ticketing.DataLayer.Entities.User;

namespace Ticketing.DataLayer.Context
{
    public class TicketingContext:DbContext
    {
        public TicketingContext(DbContextOptions<TicketingContext> options):base(options)
        {

        }

        public DbSet<Admin> Admin { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Ticket> Ticket { get; set; }
        public DbSet<TicketAnswer> TicketAnswer { get; set; }
        public DbSet<TicketQuestion> TicketQuestion { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region Seed Data

            modelBuilder.Entity<Admin>()
                .HasData(
                    new Admin()
                    {
                        id = 1,
                        Email = "Admin@gmail.com",
                        Password = "Admin"
                    }
                );
            
            modelBuilder.Entity<User>()
                .HasData(
                    new User()
                    {
                        Id = 1,
                        Name = "User1",
                        Email = "User1@gmail.com",
                        Password = "User1"
                    },
                    new User()
                    {
                        Id = 2,
                        Name = "User2",
                        Email = "User2@gmail.com",
                        Password = "User2"
                    }
                );
            
            modelBuilder.Entity<Ticket>()
                .HasData(
                    new Ticket()
                    {
                        Id = 1,
                        Title = "رفع باگ",
                        IsFinished = false,
                        userId = 1,
                        DateTime = DateTime.Now
                    },
                    new Ticket()
                    {
                        Id = 2,
                        Title = "مدیریت خطا ها",
                        IsFinished = true,
                        userId = 1,
                        DateTime = DateTime.Now
                    }
                    
                );

            modelBuilder.Entity<TicketQuestion>()
                .HasData(
                    new TicketQuestion()
                    {
                        id = 1,
                        Body = "لطفا باگ صفحه ورود را برطرف کنید",
                        ticketId = 1,
                        IsResponded = true,
                        DateTime = DateTime.Now
                    }
                    );

            modelBuilder.Entity<TicketAnswer>()
                .HasData(
                    new TicketAnswer()
                    {
                        id = 1,
                        adminId = 1,
                        Body = "در حال بررسی میباشد لطفا صبور باشید",
                        ticketId = 1,
                        DateTime = DateTime.Now,
                        questionId = 1
                    }
                    );
            
            #endregion
            
            base.OnModelCreating(modelBuilder);
            
        }
    }
}
