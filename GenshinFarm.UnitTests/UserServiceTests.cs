using GenshinFarm.Core.Entities;
using GenshinFarm.Core.Interfaces;
using GenshinFarm.Core.Services;
using Moq;
using NUnit.Framework;
using System;

namespace GenshinFarm.UnitTests
{
    public class UserServiceTests
    {
        [Test]
        [TestCase("<>", typeof(ArgumentOutOfRangeException))]
        [TestCase("*fj*gbdiufbg", typeof(ArgumentOutOfRangeException))]
        [TestCase("adg~bsd", typeof(ArgumentOutOfRangeException))]
        public void Test1AddWithInvalidCharactersError(string name, Type ex =null)
        {
            var repo = new Mock<IUnitOfWork>();
            UserService service = new UserService(repo.Object);
            User user = new User
            {
                Username = name
            };
            Assert.Throws(ex, () => service.Add(user).GetAwaiter().GetResult());
        }
    }
}
