﻿using Microsoft.EntityFrameworkCore;
using FYPBackend.Models;

namespace FYPBackend.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
            public DbSet<Patient> Patients { get; set; }
            public DbSet<Doctor> Doctors { get; set; }
            public DbSet<Appointment> Appointments { get; set; }

    }
}
