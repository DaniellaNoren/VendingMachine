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
        new ProductStock(2, new Drink(75, "Coke", 11, "Fizzy")),
        new ProductStock(1, new Food(101, "Smarties", 15, "Crunchy")),
        new ProductStock(5, new Item("Stick", 120, "Sticky")),
        new ProductStock(3, new Drink(0, "Water", 16, "Satisfying")),
        });


        static void Main(string[] args)
        {
            VendingMachineProgram program = new VendingMachineProgram();
            program.StartProgram();
            program.EndProgram();
        }

        public void StartProgram()
        {
            int choice;

            do
            {
                PrintMainMenu();

                choice = GetNumber();

                ChooseMainMenu(choice);

            } while (choice != 3);

        }

        public void EndProgram()
        {
            Dictionary<int, int> change = vendingMachine.EndTransaction();
            PrintChange(change);
            int choice;

            do
            {
                PrintProducts(boughtProducts);

                choice = GetNumber("Enter number of product: (-1 to end program)");

                if (choice == -1)
                    break;

                Console.Clear();

                Product product = ChooseProduct(choice);

                if (product == null)
                {
                    Console.WriteLine("Product does not exist! Try again!");
                    continue;
                }

                PrintProductMenu();

                choice = GetNumber();

                ChooseProductMenu(choice, product);

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

        public void ChooseProductMenu(int choice, Product product)
        {
            switch (choice)
            {
                case 1: UseProduct(product); break;
                case 2: ExamineProduct(product); break;
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
                    int choice = GetNumber("Enter your choice: (-1 to exit)");

                    if (choice == -1)
                        return;

                    Product product = vendingMachine.Purchase(choice - 1);
                    boughtProducts.Add(product);
                    Console.WriteLine($"You bought a {product.Name}");
                }
                catch (NotEnoughMoneyException)
                {
                    Console.WriteLine("You do not have enough money to buy this product");

                }
                catch (OutOfStockException)
                {
                    Console.WriteLine("The product you chose is out of stock.");
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Incorrect number. Try again.");
                }

            } while (true);
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

            if (product.IsUnusable)
            {
                boughtProducts.Remove(product);
            };
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
                Console.WriteLine($"{index++}. {product.Product.Examine()}. Amount: {product.Amount}");
            }
        }
        public void PrintProducts(List<Product> products)
        {
            int index = 1;

            foreach (Product product in products)
            {
                Console.WriteLine($"{index++}. {product.Name}.");
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

        public void PrintProductMenu()
        {
            ChangeConsoleColor(ConsoleColor.Green);
            Console.WriteLine("------------------\n1. Use\n2. Examine\n3. Go back");
        }

        public void ChangeConsoleColor(ConsoleColor color = ConsoleColor.Gray)
        {
            Console.ForegroundColor = color;
        }
    }
}
