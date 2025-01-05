using Logic;
using Models;
using Models.PumpTypes;
using Moq;
using NUnit.Framework;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Test
{
    [TestFixture]
    public class PumpLogicTester
    {
        PumpLogic logic;
        Mock<IRepository<Pump>> mockPumpRepo;
        List<Pump> pumpList; //Ahhoz hogy frissüljön a tesztelés során a repository listát használunk
        [SetUp]
        public void Init()
        {
            mockPumpRepo = new Mock<IRepository<Pump>>();
            pumpList = new List<Pump>()
            {
                new CompactPump("1111", Room.Room202, Models.Type.Volumetric),
                new SpacePlusPump("1114", Room.Room201, Models.Type.Volumetric)
            };
            //ReadAll
            mockPumpRepo.Setup(repo => repo.ReadAll()).Returns(() => pumpList.AsQueryable());
            //Read
            mockPumpRepo.Setup(repo => repo.Read(It.IsAny<string>())).Returns((string id) => pumpList.FirstOrDefault(t => t.SerialID == id));
            //Create
            mockPumpRepo.Setup(repo => repo.Create(It.IsAny<Pump>())).Callback<Pump>(t => pumpList.Add(t));
            //Delete
            mockPumpRepo.Setup(repo => repo.Delete(It.IsAny<string>())).Callback<string>(id => pumpList.RemoveAll(p => p.SerialID == id));
            logic = new PumpLogic(mockPumpRepo.Object);
        }
        [Test]
        public void PumpCreateSuccessTest()
        {
            var newPump = new CompactPlusPump("1234", Room.Room203, Models.Type.Volumetric);
            logic.Create(newPump);
            mockPumpRepo.Verify(t => t.Create(newPump), Times.Once);
        }
        [Test]
        public void PumpCreateFailTest()
        {
            var newPump = new SpacePump("1114", Room.Room203, Models.Type.Syringe);
            Assert.Throws<ArgumentException>(() => logic.Create(newPump));
            mockPumpRepo.Verify(t => t.Create(newPump), Times.Never);
        }
        [Test]
        public void PumpRemoveSuccessTest()
        {
            string idToRemove = "1114";
            logic.Delete(idToRemove);
            mockPumpRepo.Verify(t => t.Delete(idToRemove), Times.Once);
        }
        [Test]
        public void PumpRemoveFailTest()
        {
            string idToRemove = "9999";
            Assert.Throws<ArgumentException>(() => logic.Delete(idToRemove));
            mockPumpRepo.Verify(t => t.Delete(idToRemove), Times.Never);
        }
        [Test]
        public void PumpChangeLocationSuccessTest()
        {
            string serialIdToUpdate = "1111";
            Room newRoom = Room.Room203;
            logic.ChangePumpLocation(serialIdToUpdate, newRoom);
            var updatedPump = logic.Read(serialIdToUpdate);
            Assert.That(newRoom, Is.EqualTo(updatedPump.Location));
            mockPumpRepo.Verify(repo => repo.Update(It.IsAny<Pump>()), Times.Once);
        }
        [Test]
        public void PumpChangeLocationFailTest()
        {
            string serialIdToUpdate = "9999";
            Room newRoom = Room.Room203;
            Assert.Throws<ArgumentException>(() => logic.ChangePumpLocation(serialIdToUpdate, newRoom));
            mockPumpRepo.Verify(repo => repo.Update(It.IsAny<Pump>()), Times.Never);
        }
    }
}
