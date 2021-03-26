using System.Linq;
using Xunit;
using Mighty;
using types;

namespace MightyTests
{
    public class Basics
    {
        [Fact]
        public void Map_objects_to_database()
        {
            var database = new MightyOrm("ProviderName=Microsoft.Data.Sqlite;Data Source=/Users/aaron/Code/sandbox/libraries/orm/orm.sqlite", "Fingers");
            var fingers = Generate.Fingers(5);
            var results = database.Insert(fingers).Select(result => (Finger)result);
            Assert.All(fingers.Zip(results), pair => 
                Assert.Equal(pair.First, pair.Second));
        }
        
        [Fact]
        public void Retrieve_objects_from_database()
        {
            var database = new MightyOrm("ProviderName=Microsoft.Data.Sqlite;Data Source=/Users/aaron/Code/sandbox/libraries/orm/orm.sqlite", "Fingers", "Id");
            var fingers = Generate.Fingers(5);
            _ = database.Insert(fingers);
            var retrieved = database
                .All(fingers.Select(finger => new {Id = finger.Id.ToString()}))
                .Select(result => (Finger)result)
                .ToArray();
            Assert.All(fingers.Zip(retrieved), pair => 
                Assert.Equal(pair.First, pair.Second));
        }
    }
}