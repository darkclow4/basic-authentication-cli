using System.Text.RegularExpressions;

namespace Authentication
{
    internal class MainMenu
    {
        public static int Home()
        {
            int inputInstruction;
            Console.WriteLine("== BASIC AUTHENTICATION ==");
            Console.WriteLine();
            Console.WriteLine("1. Create User");
            Console.WriteLine("2. Show User");
            Console.WriteLine("3. Search User");
            Console.WriteLine("4. Login User");
            Console.WriteLine("5. Exit");
            Console.WriteLine();
            Console.Write("Input : ");
            string inputNumber = Console.ReadLine();
            inputInstruction = ValidateMenu(inputNumber, 5);
            while (inputInstruction == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input. Only accept number 1 - 5");
                Console.WriteLine();
                Console.Write("Input : ");
                inputNumber = Console.ReadLine();
                inputInstruction = ValidateMenu(inputNumber, 5);
            }
            return inputInstruction;
        }
        public static void Menu1()
        {
            Console.Clear();
            Console.WriteLine("== ADD USER ==");
            Console.WriteLine();
            Console.Write("First Name: ");
            string firstName = Console.ReadLine();
            Console.Write("Last Name: ");
            string lastName = Console.ReadLine();
            Console.Write("Password: ");
            string password = Console.ReadLine();

            bool validPassword = ValidatePassword(password);
            while (!validPassword) {
                Console.WriteLine("Invalid password format. Password must have 8-15 characters, \nat least 1 uppercase letter, \n\t1 lowercase letter, \n\tand 1 number\n") ;
                Console.Write("Password: ");
                password = Console.ReadLine();
                validPassword = ValidatePassword(password);
            }

            User user = new User(firstName, lastName, password);
            DB.AddUser(user);

            Console.WriteLine();
            Console.WriteLine("User data successfully created");
            Console.ReadKey();
        }
        public static void Menu2()
        {
            Console.Clear();
            Console.WriteLine("== SHOW USER ==");
            Console.WriteLine();
            Console.WriteLine("|| ID || Name || Username || Password ||");
            foreach (var user in DB.GetUsers())
            {
                Console.WriteLine($"|| {user.Id} || {user.Name} || {user.UserName} || {user.Password} ||");
            }
            Console.WriteLine();
            Console.WriteLine("Menu");
            Console.WriteLine("1. Edit User");
            Console.WriteLine("2. Delete User");
            Console.WriteLine("3. Back");
            Console.WriteLine();
            Console.Write("Input: ");
            string inputNumber = Console.ReadLine();
            int inputInstruction = ValidateMenu(inputNumber, 3);
            while (inputInstruction == 0)
            {
                Console.WriteLine();
                Console.WriteLine("Invalid input. Only accept number 1 - 3");
                Console.WriteLine();
                Console.Write("Input : ");
                inputNumber = Console.ReadLine();
                inputInstruction = ValidateMenu(inputNumber, 3);
            }
            switch(inputInstruction)
            {
                case 1:
                    Console.Write("Input ID to edit : ");
                    User user;
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
                        user = DB.FindId(id);
                        if (user == null) throw new Exception();
                        Console.WriteLine("\nNote : Leave empty to not change old value");
                        Console.Write("First Name : ");
                        string firstName = Console.ReadLine();
                        Console.Write("Last Name : ");
                        string lastName = Console.ReadLine();
                        Console.Write("Password : ");
                        string password = Console.ReadLine();

                        string[] oldNames = user.Name.Split(' ');
                        firstName = firstName == ""? oldNames[0] : firstName;
                        lastName = lastName == ""? oldNames[1] : lastName;
                        if(password == "")
                        {
                            password = user.Password;
                        } else
                        {
                            bool validPassword = ValidatePassword(password);
                            while (!validPassword)
                            {
                                Console.WriteLine("Invalid password format. Password must have 8-15 characters, \nat least 1 uppercase letter, \n\t1 lowercase letter, \n\tand 1 number\n");
                                Console.Write("Password: ");
                                password = Console.ReadLine();
                                validPassword = ValidatePassword(password);
                            }
                        }
                        DB.EditUser(id, firstName, lastName, password);
                        Console.WriteLine("Data was changed successfully") ;
                    } catch (Exception) { Console.WriteLine("Invalid input or data not found"); }

                    Console.ReadKey();
                    break;
                case 2: 
                    Console.Write("Input ID to delete : ");
                    try
                    {
                        int id = int.Parse(Console.ReadLine());
                        DB.RemoveUser(id);
                        Console.WriteLine("User data was deleted");
                    } catch (Exception)
                    {
                        Console.WriteLine("Failed to delete. Invalid input or ID not found.");
                    }
                    Console.ReadKey(); break;
                case 3: 
                    break;
            }
        }
        public static void Menu3()
        {
            Console.Clear();
            Console.WriteLine("== SEARCH USER ==");
            Console.WriteLine("* Note : Case Sensitive Search");
            Console.WriteLine();
            Console.Write("Input Name: ");
            string name = Console.ReadLine();
            Console.WriteLine();
            List<User> users = DB.FindName(name);
            if(users.Count == 0)
            {
                Console.WriteLine("==============================");
                Console.WriteLine("Users not found");
            } else
            {
                foreach (User user in users)
                {
                    Console.WriteLine("==============================");
                    Console.WriteLine($"ID        : {user.Id}");
                    Console.WriteLine($"Name      : {user.Name}");
                    Console.WriteLine($"Username  : {user.UserName}");
                    Console.WriteLine($"Password  : {user.Password}");
                }
            }
            Console.WriteLine("==============================");
            Console.ReadKey();
        }
        public static void Menu4()
        {
            Console.Clear();
            Console.WriteLine("== LOGIN ==");
            Console.Write("\nUSERNAME: ");
            string inputUsername = Console.ReadLine();
            Console.Write("\nPASSWORD: ");
            string inputPassword = Console.ReadLine();
            Console.WriteLine();

            try
            {
                User user = DB.FindUsername(inputUsername);
                if (user.UserName == inputUsername && user.Password == inputPassword)
                {
                    Console.WriteLine($"Login success. Welcome {user.Name}!");
                }
                else { Console.WriteLine("Wrong username and password combination"); }
            } catch (Exception)
            {
                Console.WriteLine("User not found");
            }
            Console.ReadKey();
        }

        static int ValidateMenu(string inputNumber, int maxNumber)
        {
            bool valid;
            int outNumber;
            valid = int.TryParse(inputNumber, out outNumber) && outNumber <= maxNumber && outNumber > 0;
            return valid ? outNumber : 0;
        }

        static bool ValidatePassword(string password)
        {
            Regex rgx = new Regex(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,15}$");
            return rgx.IsMatch(password);
        }
    }
}
