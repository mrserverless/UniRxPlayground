using NUnit.Framework;
using ZeroFormatter;

namespace Models
{
    public class ZeroFormatterTest
    {
        [SetUp]
        public void RegisterZeroFormatter()
        {
            ZeroFormatterInitializer.Register();
        }

        [Test]
        public void TestSerializeNamespaceClass()
        {
            // given
            var player = new Player
            {
                Age = 100,
                FirstName = "Ancient",
                LastName = "Being"
            };

            // when
            var buffer = ZeroFormatterSerializer.Serialize(player);

            // then
            Assert.NotNull(buffer);
            Assert.IsNotEmpty(buffer);
        }
    }


}
