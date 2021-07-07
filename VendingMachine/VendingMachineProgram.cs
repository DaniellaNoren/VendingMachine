using System;
using System.Collections.Generic;
using VendingMachine.Exceptions;
using VendingMachine.Products;
using VendingMachine.Products.Food;
using VendingMachine.Products.Things;

namespace VendingMachine
{
    class VendingMachineProgram
    {
        private List<Product> boughtProducts = new List<Product>();

        private static VendingMachine vendingMachine = new VendingMachine(new ProductStock[] {
        new ProductStock(2, new SodaCan(75, "Coke", 11, "Fizzy", pant: 2)),
        new ProductStock(2, new SodaCan(72, "Pepsi", 11, "Tasty", maxSips: 4)),
        new ProductStock(2, new SodaCan(0, "Water", 15, "Satisfying", isCarbonated: false, pant: 2)),
        new ProductStock(1, new Sandwich(155, "Avocado Sandwich", 23, "Crunchy")),
        new ProductStock(5, new Headphones("Apple", 255, "In-ear")),
        new ProductStock(3, new Sandwich(255, "BLT", 16, "Heavy", hasMayo: true)),
        });


        static void Main(string[] args)
        {
            VendingMachineProgram program = new VendingMachineProgram();
            program.StartProgram();
        }

        public void StartProgram()
        {
            int choice;

            do
            {
                Console.Clear();

                Console.WriteLine($"Total amount of money: {vendingMachine.MoneyPool}\nTotal cost: {vendingMachine.TotalCost}");
                PrintMainMenu();

                choice = GetNumber();

                ChooseMainMenu(choice);

            } while (choice != 3);

            EndProgram();
        }

        public void EndProgram()
        {
            Console.Clear();
            Dictionary<int, int> change = vendingMachine.EndTransaction();
            PrintChange(change);
            int choice;

            Console.WriteLine("--------------------------------\nBought products: ");

            do
            {
                PrintProducts(boughtProducts);

                choice = GetNumber("Enter number of product: (-1 to end program)");

                if (choice == -1)
                    break;

                Product product = ChooseProduct(choice);

                if (product == null)
                {
                    Console.WriteLine("Product does not exist! Try again!");
                    continue;
                }

                PrintProductMenu(product);

                choice = GetNumber();

                ChooseProductMenu(choice, product);

                if (product.IsUnusable)
                {
                    boughtProducts.Remove(product);
                };


            } while (true);
        }
        public int GetNumber(string text = "Enter your choice: ")
        {
            ChangeConsoleColor();
            Console.WriteLine(text);

            do
            {
                try
                {
                    return InputHandler.GetValidNumber();
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Enter a valid number: ");
                }
            } while (true);

        }

        public void ChooseMainMenu(int choice)
        {
            switch (choice)
            {
                case 1:
                    {
                        BuyProduct();
                    }
                    break;
                case 2:
                    EnterMoney();
                    break;
                default: break;
            }
        }
        public void EnterMoney()
        {

            do
            {
                int money = GetNumber("Enter the amount of money to insert: (-1 to exit)");

                if (money == -1)
                    return;

                try
                {
                    vendingMachine.InsertMoney(money);
                    Console.WriteLine($"Total amount of money: {vendingMachine.MoneyPool}");
                }
                catch (ArgumentException)
                {
                    Console.WriteLine("Not a valid amount of money!");
                }

            } while (true);

        }

        public void BuyProduct()
        {

            do
            {
                ChangeConsoleColor(ConsoleColor.Blue);
                PrintProducts(vendingMachine.ShowAll());
                ChangeConsoleColor();

                try
                {
                    int choice = GetNumber("Enter your choice of product to buy: (-1 to exit)");

                    if (choice == -1)
                        return;

                    Product product = vendingMachine.Purchase(choice - 1);
                    boughtProducts.Add(product);
                    Console.Clear();
                    Console.WriteLine($"You bought a {product.Type}");
                }
                catch (NotEnoughMoneyException)
                {
                    Console.Clear();
                    Console.WriteLine("You do not have enough money to buy this product");
                    

                }
                catch (OutOfStockException)
                {
                    Console.Clear();
                    Console.WriteLine("The product you chose is out of stock.");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.Clear();
                    Console.WriteLine("Incorrect number. Try again.");
                }

            } while (true);
        }

        public void ChooseProductMenu(int choice, Product product)
        {
            switch (choice)
            {
                case 1: UseProduct(product); break;
                case 2: ExamineProduct(product); break;
                case 3:
                    {

                        if (product is SodaCan)
                            RecycleSodaCan(product as SodaCan);
                        else if (product is Sandwich)
                        {
                            Console.WriteLine((product as Sandwich).Smell());
                        }
                        else
                        {
                            return;
                        }
                    } break;
                case 4:
                    {
                        if(!(product is SodaCan || product is Sandwich))
                            Console.WriteLine("Invalid choice.");
                        else
                        {
                            return;
                        }
                    } break;
                default: Console.WriteLine("Invalid choice"); break;
            }
        }

       

        public Product ChooseProduct(int choice)
        {

            if (choice <= 0 || choice > boughtProducts.Count)
            {
                return null;
            }

            return boughtProducts[choice - 1];
        }

        public void UseProduct(Product product)
        {
            Console.WriteLine(product.Use());
        }

        public void RecycleSodaCan(SodaCan sodaCan)
        {
            int pant = sodaCan.Recycle();

            Console.WriteLine($"You recycled the can and got {pant} kr");
        }

        public void ExamineProduct(Product product)
        {
            Console.WriteLine(product.Examine());
        }

        public void PrintProducts(ProductStock[] products)
        {
            int index = 1;

            foreach (ProductStock product in products)
            {
                Console.WriteLine($"{index++}. {product.Product.Examine()}. Amount: {product.Stock}");
            }
        }
        public void PrintProducts(List<Product> products)
        {
            int index = 1;

            foreach (Product product in products)
            {
                Console.WriteLine($"{index++}. {product.Type}.");
            }
        }

        public void PrintChange(Dictionary<int, int> change)
        {

            Console.WriteLine("Returned change: ");
            foreach (KeyValuePair<int, int> value in change)
            {
                Console.WriteLine($"{value.Value}x{value.Key}");
            }
        }

        public void PrintMainMenu()
        {
            ChangeConsoleColor(ConsoleColor.Green);
            Console.WriteLine("1. Show products\n2. Insert money\n3. End transaction");
        }

        public void PrintProductMenu(Product product)
        {
            ChangeConsoleColor(ConsoleColor.Green);
            Console.WriteLine("------------------\n1. Use\n2. Examine");

            if(product is SodaCan)
            {
                Console.WriteLine("3. Recycle\n4. Go back");
            }
            else if(product is Sandwich)
            {
                Console.WriteLine("3. Smell\n4. Go back");
            }
            else
            {
                Console.WriteLine("3. Go back");
            }
        }

        public void ChangeConsoleColor(ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
        }
    }
}
