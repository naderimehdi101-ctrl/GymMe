using Microsoft.EntityFrameworkCore;

public class AppDbContext : DbContext
{
    public DbSet<GymPlan> Plans { get; set; } = null!;
    public DbSet<Member> Members { get; set; } = null!;
    public DbSet<MemberIdDto> MemberIds { get; set; } = null!;
    public DbSet<Membership> Memberships { get; set; } = null!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }
}