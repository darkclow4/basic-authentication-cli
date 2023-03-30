namespace Authentication
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                Console.Clear();
                int inputInstruction = MainMenu.Home();
                switch (inputInstruction)
                {
                    case 1:
                        MainMenu.Menu1();
                        break;
                    case 2:
                        MainMenu.Menu2();
                        break;
                    case 3:
                        MainMenu.Menu3();
                        break;
                    case 4:
                        MainMenu.Menu4();
                        break;
                    case 5:
                        Environment.Exit(0);
                        break;
                }
            }
        }

        
    }
}