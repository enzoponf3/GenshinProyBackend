using AutoMapper;
using GenshinFarm.Core.DTOs;
using GenshinFarm.Core.Entities;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GenshinFarm.UnitTests
{
    public class UserElementTests
    {
        [TestCase(3, 0)]
        [TestCase(16, 0)]
        [TestCase(20, 0)]
        [TestCase(28, 1)]
        [TestCase(30, 1)]
        [TestCase(38, 1)]
        [TestCase(40, 1)]
        [TestCase(42, 2)]
        [TestCase(45, 2)]
        [TestCase(56, 3)]
        [TestCase(60, 3)]
        [TestCase(61, 4)]
        [TestCase(70, 4)]
        [TestCase(79, 5)]
        [TestCase(80, 5)]
        [TestCase(81, 6)]
        [TestCase(90, 6)]
        [Test]
        public void Test1PowerSettedByLvl(int lvl, int power)
        {
            UserElement uElement = new UserElement(lvl);
            Assert.That(power, Is.EqualTo(uElement.PowerLvl));
        }

        [TestCase(-1, typeof(ArgumentOutOfRangeException))]
        [TestCase(91, typeof(ArgumentOutOfRangeException))]
        [TestCase(102, typeof(ArgumentOutOfRangeException))]
        [TestCase(348756, typeof(ArgumentOutOfRangeException))]
        [Test]
        public void Test2UserElementOutOfRangeException(int lvl, Type ex = null)
        {
            Assert.Throws(ex, ()=> new UserElement(lvl) );
        }


        [TestCase(40, 1)]
        [TestCase(42, 2)]
        [TestCase(45, 2)]
        [TestCase(56, 3)]
        [TestCase(60, 3)]
        [TestCase(-5423, 0, typeof(ArgumentOutOfRangeException))]
        [TestCase(78325, 0, typeof(ArgumentOutOfRangeException))]
        [Test]
        public void Test3SetPowerRange(int lvl, int power, Type ex = null)
        {
            UserElementDto dElement = new UserElementDto()
            {
                Id = Guid.NewGuid().ToString(),
                Lvl = lvl,
            };
            var config = new MapperConfiguration(c =>
            {
                c.CreateMap<UserElement, UserElementDto>().ReverseMap();
            });
            config.AssertConfigurationIsValid();
            IMapper mapper = config.CreateMapper();
            try
            {
                UserElement uElement = mapper.Map<UserElement>(dElement);
                uElement.setPowerLvl();
                Assert.That(power, Is.EqualTo(uElement.PowerLvl));
            }catch(Exception e)
            {
                Assert.That(e, Is.TypeOf(ex)) ;
            }
        }
    }
}
