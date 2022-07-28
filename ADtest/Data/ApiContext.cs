using ADtest.Models;
using Microsoft.EntityFrameworkCore;
namespace ADtest.Data;

public class ApiContext:DbContext
{
 public DbSet<Pet>Pets { get; set; }

 public ApiContext(DbContextOptions<ApiContext> options) : base(options)
 {
  
 }
}