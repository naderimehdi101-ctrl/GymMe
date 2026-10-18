using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<GymPlan> GymPlans { get; set; } = null!;
    public DbSet<Member> GymMembers { get; set; } = null!;
    public DbSet<MemberIDDTO> MemberIds { get; set; } = null!;
	public DbSet<Membership> Memberships { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options): base(options)
    {
    }


protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Membership>()
            .HasOne(m => m.Member)
            .WithMany()
            .HasForeignKey(m => m.MemberId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Membership>()
            .HasOne(m => m.GymPlan)
            .WithMany()
            .HasForeignKey(m => m.GymPlanId)
            .OnDelete(DeleteBehavior.Restrict);
	}
}