using System;
using System.Collections.Generic;
using System.Text;
using VendingMachine.Products.Food;
using Xunit;

namespace VendingMachine.Test
{
    public class SandwichTests
    {
        [Fact]
        public void Smell_ReturnsMayonnaiseStringIfHasMayo()
        {
            Sandwich s = new Sandwich(1, "", 1, "", hasMayo: true);
            string str = s.Smell();

            Assert.Contains("mayonnaise", str, StringComparison.OrdinalIgnoreCase);
        } 
        
        [Fact]
        public void Smell_ReturnsNormalStringIfNotHasMayo()
        {
            Sandwich s = new Sandwich(1, "", 1, "");
            string str = s.Smell();

            Assert.Contains("sandwich", str, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Use_ReturnCorrectStringIfUnusable()
        {
            Sandwich s = new Sandwich(1, "", 1, "");
            s.IsUnusable = true;

            string str = s.Use();
            Assert.Contains("already eaten", str, StringComparison.OrdinalIgnoreCase);
        }

        [Fact]
        public void Use_MakesSandwichUnusable()
        {
            Sandwich s = new Sandwich(1, "", 1, "");
            s.Use();
            Assert.True(s.IsUnusable);
        }
        
        [Fact]
        public void Use_ReturnsMayonnaiseStringIfHasMayo()
        {
            Sandwich s = new Sandwich(1, "", 1, "", hasMayo: true);
            string str = s.Use();
            Assert.Contains("mayonnaise", str);
            Assert.Contains(s.Calories.ToString(), str);
            Assert.Contains(s.Type, str);

        }

        [Fact]
        public void Use_ReturnsNormalStringIfNotHasMayo()
        {
            Sandwich s = new Sandwich(1, "", 1, "");
            string str = s.Use();
            Assert.Contains("feel amazing", str);
            Assert.Contains(s.Calories.ToString(), str);
            Assert.Contains(s.Type, str);
        }
    }
}
