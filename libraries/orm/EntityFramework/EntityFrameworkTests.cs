using System.Linq;
using types;
using Xunit;

namespace EntityFramework
{
    public class EntityFrameworkTests
    {
        [Fact]
        public void Store_records()
        {
            var arms = Generate.Arms(5);
            var database = new Database();
            database.Arms.AddRange(arms);
            database.SaveChanges();
            var armIds = arms.Select(arm => arm.Id).ToArray();
            var retrieved = database.Arms.Where(arm => armIds.Contains(arm.Id)).ToArray();
            Assert.Equal(arms.Count, retrieved.Length);
            Assert.Equal(arms.Sum(arm => arm.Hands.Count), retrieved.Sum(arm => arm.Hands.Count));
            Assert.Equal(arms.Sum(arm => arm.Hands.Sum(hand => hand.Fingers.Count)), retrieved.Sum(arm => arm.Hands.Sum(hand => hand.Fingers.Count)));
        }
    }
}