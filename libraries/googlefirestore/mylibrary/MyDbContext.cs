using Microsoft.EntityFrameworkCore;

namespace mylibrary
{
	public class DbRecordA
	{
		public string Name { get; set; }
		public int Number { get; set; }
	}
	
	public class DbRecordB
	{
		public string Name { get; set; }
		public int Number { get; set; }
	}
	
	public class MyDbContext : DbContext
    {
        public DbSet<DbRecordA> A { get; set; }
		public DbSet<DbRecordB> B { get; set; }
    }
}