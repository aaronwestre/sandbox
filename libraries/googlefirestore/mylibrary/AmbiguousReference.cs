namespace mylibrary
{
    using System.Linq;
    
    public static class AmbiguousReference
    {
        public static int CountItems()
        {
            var dbContext = new MyDbContext();
            /* The line below has an ambiguous reference
             * between IAsyncEnumerable, IEnumerable, and IQueryable.
             * Removing the Google.Cloud.Firestore package reference
             * from librarywithfirestorereference.csproj resolves
             * the ambiguity.
             * I can work around this with:
             * var aList = dbContext.A.AsQueryable().Where(a => a.Number > 5);
             *
             * When using the previous version of this package (Google.Cloud.Firestore.V1)
             * I did not experience this issue.
             */
            var aList = dbContext.A.Where(a => a.Number > 5);
            return aList.Count();
        }
    }
}
