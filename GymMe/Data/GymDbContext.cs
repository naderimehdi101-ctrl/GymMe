using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<GymPlan> Plans { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<Membership> Memberships { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
        
        modelBuilder.Entity<Member>()
            .HasOne(x => x.GymPlan)
            .WithMany()
            .HasForeignKey(x => x.GymPlanId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Membership>()
            .HasOne(x => x.Member)
            .WithMany()
            .HasForeignKey(x => x.MemberId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Membership>()
            .HasOne(x => x.GymPlan)
            .WithMany()
            .HasForeignKey(x => x.GymPlanId)
            .OnDelete(DeleteBehavior.Restrict);    
    } 
}