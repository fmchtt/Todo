using Microsoft.EntityFrameworkCore;
using Todo.Domain.Entities;

namespace Todo.Infra.Data.Contexts;

public class TodoDBContext : DbContext
{
    public DbSet<Board> Boards { get; set; }
    public DbSet<Column> Columns { get; set; }
    public DbSet<TodoItem> Itens { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<RecoverCode> RecoverCodes { get; set; }
    public DbSet<Invite> Invites { get; set; }
    public DbSet<Comment> Comments { get; set; }

    public TodoDBContext(DbContextOptions<TodoDBContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Board>().HasMany(b => b.Participants).WithMany(u => u.Boards);
        modelBuilder.Entity<Board>().HasMany(b => b.Columns).WithOne(c => c.Board).HasForeignKey(c => c.BoardId);
        modelBuilder.Entity<Board>().HasMany(b => b.Itens).WithOne(i => i.Board).HasForeignKey(i => i.BoardId).OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Board>().HasOne(b => b.Owner).WithMany(u => u.OwnedBoards).HasForeignKey(b => b.OwnerId);

        modelBuilder.Entity<Column>().HasMany(c => c.Itens).WithOne(i => i.Column).HasForeignKey(i => i.ColumnId).OnDelete(DeleteBehavior.SetNull);
        modelBuilder.Entity<Column>().HasIndex(c => new { c.Order, c.BoardId }).IsUnique();

        modelBuilder.Entity<TodoItem>().HasOne(t => t.Creator).WithMany(u => u.Itens).HasForeignKey(t => t.CreatorId);
        modelBuilder.Entity<TodoItem>().HasMany(t => t.Comments).WithOne(c => c.Item).HasForeignKey(c => c.ItemId);

        modelBuilder.Entity<RecoverCode>().HasOne(r => r.User).WithOne();
        modelBuilder.Entity<RecoverCode>().HasIndex(r => new { r.UserId, r.Code }).IsUnique();

        modelBuilder.Entity<Invite>().HasOne(i => i.Board).WithMany();
        modelBuilder.Entity<Invite>().HasIndex(i => new { i.BoardId, i.Email }).IsUnique();

        modelBuilder.Entity<Comment>().HasOne(c => c.Author).WithMany();
    }
}
