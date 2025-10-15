using System;
using Microsoft.EntityFrameworkCore;
using UserManagement.Data.Entities;
using UserManagement.Models;

namespace UserManagement.Data;

public class DataContext : DbContext
{
    public DataContext() { }

    public DataContext(DbContextOptions<DataContext> options) : base(options)
    {

    }

    protected override void OnModelCreating(ModelBuilder model)
    {
        base.OnModelCreating(model);


        model.Entity<User>().HasData(new[]
        {
            new User { Id = 1, Forename = "Peter", Surname = "Loew", Email = "ploew@example.com", DateOfBirth = DateTime.Parse("14/11/1960"), IsActive = true },
            new User { Id = 2, Forename = "Benjamin Franklin", Surname = "Gates", Email = "bfgates@example.com", DateOfBirth = DateTime.Parse("14/01/1940"), IsActive = true },
            new User { Id = 3, Forename = "Castor", Surname = "Troy", Email = "ctroy@example.com", DateOfBirth = DateTime.Parse("02/07/1999"), IsActive = false },
            new User { Id = 4, Forename = "Memphis", Surname = "Raines", Email = "mraines@example.com", DateOfBirth = DateTime.Parse("06/08/1978"), IsActive = true },
            new User { Id = 5, Forename = "Stanley", Surname = "Goodspeed", Email = "sgodspeed@example.com", DateOfBirth = DateTime.Parse("23/06/2005"), IsActive = true },
            new User { Id = 6, Forename = "H.I.", Surname = "McDunnough", Email = "himcdunnough@example.com", DateOfBirth = DateTime.Parse("26/05/1940"), IsActive = true },
            new User { Id = 7, Forename = "Cameron", Surname = "Poe", Email = "cpoe@example.com", DateOfBirth = DateTime.Parse("01/05/1986"), IsActive = false },
            new User { Id = 8, Forename = "Edward", Surname = "Malus", Email = "emalus@example.com", DateOfBirth = DateTime.Parse("14/07/1960"), IsActive = false },
            new User { Id = 9, Forename = "Damon", Surname = "Macready", Email = "dmacready@example.com", DateOfBirth = DateTime.Parse("21/01/1986"), IsActive = false },
            new User { Id = 10, Forename = "Johnny", Surname = "Blaze", Email = "jblaze@example.com", DateOfBirth = DateTime.Parse("04/06/2001"), IsActive = true },
            new User { Id = 11, Forename = "Robin", Surname = "Feld", Email = "rfeld@example.com", DateOfBirth = DateTime.Parse("05/02/1989"), IsActive = true },
        });

        model.Entity<UserActionLog>().Navigation(x => x.User).AutoInclude();
    }



    public virtual DbSet<User>? Users { get; set; }
    public virtual DbSet<UserActionLog> UserActionLogs { get; set; }
}
