using InternTask.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace InternTask.Data.Contexts;
public class AppDbContext : DbContext
{
    public DbSet<User> Users { get; set; }
    public DbSet<Grades> Grades { get; set; }
    public DbSet<Subject> Subjects { get; set; }
    public DbSet<UserSubject> UserSubjects { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }
}
