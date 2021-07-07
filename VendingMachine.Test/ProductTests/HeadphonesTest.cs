using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Products.Things;
using Xunit;

namespace VendingMachine.Test
{
    public class HeadphonesTest
    {

        [Fact]
        public void Use_ReturnCorrectStringIfUnusable()
        {
            Headphones hp = new Headphones("Apple", 150, "Cool");
            hp.IsUnusable = true;

            string str = hp.Use();

            Assert.Contains("trash", str);
        }

        private Mock<Headphones> hpMock = new Mock<Headphones>("Apple", 150, "Description");

        [Fact]
        public void Use_ReturnCorrectStringIfUsedWrong()
        {
            hpMock.CallBase = true;

            hpMock.Setup(hp => hp.CheckIfUsedWrong()).Returns(true);
            string str = hpMock.Object.Use();

            hpMock.Verify(hp => hp.CheckIfUsedWrong());
            Assert.Contains("consequences", str);
        } 
        
        [Fact]
        public void Use_ReturnCorrectStringIfUsedCorrectly()
        {
            hpMock.CallBase = true;

            hpMock.Setup(hp => hp.CheckIfUsedWrong()).Returns(false);
            string str = hpMock.Object.Use();

            hpMock.Verify(hp => hp.CheckIfUsedWrong());
            Assert.Contains(hpMock.Object.Type, str);
            Assert.Contains(hpMock.Object.Description, str);
            Assert.Contains("nice music", str);
        }
    }
}
