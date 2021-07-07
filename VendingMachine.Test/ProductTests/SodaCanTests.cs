using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Products;
using Xunit;

namespace VendingMachine.Test
{
    public class SodaCanTests
    {
        [Fact]
        public void SodaCan_MaxSipsThrowsExceptionWhenNegative()
        {
            Assert.Throws<ArgumentException>(() => new SodaCan(1, "", 1, "", maxSips: -1));
        } 
        
        [Fact]
        public void SodaCan_SipsThrowsExceptionWhenNegativeOrExceedingMaxSips()
        {
            SodaCan sc = new SodaCan(1, "", 1, "");

            Assert.Throws<ArgumentException>(() => sc.Sips = 4);
            Assert.Throws<ArgumentException>(() => sc.Sips = -1);
        } 
        
        [Fact]
        public void SodaCan_PantThrowsExceptionWhenNegative()
        {
            Assert.Throws<ArgumentException>(() => new SodaCan(1, "", 1, "", pant: -1));
        }

        [Fact]
        public void Recycle_ReturnsPant()
        {
            SodaCan sc = new SodaCan(4, "", 4, "", pant: 3);
            Assert.Equal(3, sc.Recycle());
        } 
        
        [Fact]
        public void Recycle_ReturnsZeroIfUnusable()
        {
            SodaCan sc = new SodaCan(4, "", 4, "", pant: 3);
            sc.IsUnusable = true;

            Assert.Equal(0, sc.Recycle());
        } 
        
        [Fact]
        public void Recycle_MakesUnusable()
        {
            SodaCan sc = new SodaCan(4, "", 4, "", pant: 3);
            sc.Recycle();

            Assert.True(sc.IsUnusable);
        }

        [Fact]
        public void Use_IncreasesSips()
        {
            SodaCan sc = new SodaCan(1, "", 1, "");
            int sipsBefore = sc.Sips;
            sc.Use();
            Assert.True(sc.Sips > sipsBefore);
        }

        [Fact]
        public void Use_ReturnFinishedStringIfSipsEqualsToMaxSips()
        {
            SodaCan sc = new SodaCan(1, "", 1, "", maxSips: 2);
            sc.Sips = 2;

            string str = sc.Use();

            Assert.Contains("finished", str, StringComparison.OrdinalIgnoreCase);
            Assert.Contains(sc.Calories.ToString(), str);
        }


        [Fact]
        public void Use_ReturnSipString()
        {
            SodaCan sc = new SodaCan(1, "", 1, "", maxSips: 2);

            string str = sc.Use();

            Assert.Contains("sip", str, StringComparison.OrdinalIgnoreCase);
            Assert.Contains(sc.Type, str);
            Assert.Contains(sc.Description, str);
        }
    }
}
