using CrossoverSpa.Database;


namespace CrossoverSpa.Helper
{
    public partial class DbHelper: IDbHelper
    {
        private readonly SpaDbContext _context;

        public DbHelper(SpaDbContext context)
        {
            _context = context;
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
