using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using GenshinFarm.Infrastructure.Mappings;
using Moq;
using NUnit.Framework;

namespace GenshinFarm.UnitTests
{
    public class MappingTests
    {

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1MappingUsesEntityConstructor()
        {
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<User, UserDto>().ReverseMap();
            });
            config.AssertConfigurationIsValid();
            IMapper _mapper = config.CreateMapper();
            UserDto userDto = new UserDto 
            {
                Username = "TestUser",
                Email = "test@test.com",
            };
            User user = _mapper.Map<User>(userDto);
            Assert.That(user.Id, Is.EqualTo(null));
        }
    }
}