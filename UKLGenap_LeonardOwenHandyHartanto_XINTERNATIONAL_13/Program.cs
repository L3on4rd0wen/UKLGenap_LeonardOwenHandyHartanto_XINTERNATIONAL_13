class Stand  // Base class (parent) 
{
    protected string standName;
    protected double dailyRentalPrice;
    protected bool isAvailable;
    
    public Stand(string name, double price)
    {
        standName = name;
        dailyRentalPrice = price;
        isAvailable = true; // Default to available
    }

    public string StandName {
        get { return standName; }
        set {
            if (!(string.IsNullOrEmpty(standName) || string.IsNullOrWhiteSpace(standName)))
            {
                standName = value;
            } 
        }
    }
    public double DailyRentalPrice {
        get { return dailyRentalPrice; }
        set {
            if (dailyRentalPrice > 0)
            {
                dailyRentalPrice = value;
            }
        }
    }
    public bool IsAvailable {
        get { return isAvailable; }
    }

    void DisplayInfo() // Method to display stand information
    {
        Console.WriteLine(string.Format(
            $"Stand Name    : {standName} \n" +
            $"Price/Day     : {dailyRentalPrice} \n" +
            $"Status        : {(isAvailable ? "Available" : "Rented")}"));
    }

    public void ChangeStatus()
    {
        isAvailable = !isAvailable;
    }

    public virtual double CalculateTotal(int rentalDays) {
        return DailyRentalPrice * rentalDays;
    }
}

class OutdoorStand : Stand
{
    protected double _tentFee = 75000; 
    public OutdoorStand(string name, double price) : base(name, price)
    {
        StandName = name;
        DailyRentalPrice = price;
    }
    public double TentFee {
        get { return _tentFee; }
    }
    public override double CalculateTotal(int rentalDays) {
        double total = (DailyRentalPrice * rentalDays) + (TentFee * rentalDays);
        return total;
    }
}

class IndoorStand : Stand
{
    protected double _electricityFee = 100000;
    public IndoorStand(string name, double price) : base(name, price)
    {
        StandName = name;
        DailyRentalPrice = price;
    }
    public double ElectricityFee
    {
        get { return _electricityFee; }
    }
    public override double CalculateTotal(int rentalDays)
    {
        double total = base.CalculateTotal(rentalDays) + (ElectricityFee * rentalDays);
        return total;
    }
}
class PremiumStand : Stand {
    protected double _securityFee = 300000;
    public PremiumStand(string name, double price) : base(name, price)
    {
        StandName = name;
        DailyRentalPrice = price;
    }
    public double SecurityFee
    {
        get { return _securityFee; }
    }
    public override double CalculateTotal(int rentalDays)
    {
        double total = (DailyRentalPrice * rentalDays) + SecurityFee;
        return total;
    }
}

class StandVIP : PremiumStand {
    public StandVIP(string name, double price) : base(name, price)
    {
        
    }
}

class Program {

    const int width = 49;
    const int columnSize = 15;
    static void Main(string[] args)
    {
        List<Stand> stands = new List<Stand>();

        stands.Add(new OutdoorStand("Outdoor-1", 450000));
        stands.Add(new OutdoorStand("Outdoor-2", 500000));
        stands.Add(new IndoorStand("Indoor-1", 600000));
        stands.Add(new IndoorStand("Indoor-2", 700000));
        stands.Add(new PremiumStand("Premium-1", 1800000));
        stands.Add(new PremiumStand("Premium-2", 2000000));

        bool running = true;
        while (running)
        {
            Console.WriteLine(
                new string('=', width - 1) + "\n" +
                centeredAlign("<<< Starlight Festival >>>", width) + "\n\n" +
                centeredAlign("< Available Stands >", width) + "\n" +
                new string('=', width - 1) + "\n" +

                // Table Header
                $"{centeredAlign("Stand Name", columnSize)}|" +
                $"{centeredAlign("Price/Day", columnSize)}|" +
                $"{centeredAlign("Status", columnSize)}|" + "\n" +
                
                new string('=', width - 1)
                );
            for (int i = 0; stands.Count > i; i++)
            {
                if (stands[i].IsAvailable)
                {
                    string ParsedPrice = currencyFormat(stands[i].DailyRentalPrice);
                    Console.WriteLine(
                        $"{centeredAlign(stands[i].StandName,columnSize)}|" +
                        $"{centeredAlign(ParsedPrice,columnSize)}|" +
                        $"{centeredAlign("Available", columnSize)}|");
                }
            }
            Console.WriteLine(new string('=', width - 1));

            Console.Write(
                "\n1. Rent a Stand" +
                "\n2. End Renting a Stand" +
                "\n3. Exit" +
                "\n\nSelect a Menu: ");
            string? choice = Console.ReadLine();

            while (choice != "1" && choice != "2" && choice != "3")
            {
                Console.Write("Invalid choice. Please select a valid menu option: ");
                choice = Console.ReadLine();
            }
            switch (choice)
            {
                case "1":
                    RentStand(stands);
                    break;
                case "2":
                    EndRentingStand(stands);
                    break;
                case "3":
                    Console.WriteLine("Thank you for using the Stand Rental System!");
                    running = false;
                    break;
                default:
                    Console.WriteLine("Invalid choice.\n\n");
                    break;
            }
        }
    }

    static void RentStand(List<Stand> stands)
    {
        Console.Write("\nEnter the name of the stand you want to rent: ");
        string? standName = Console.ReadLine();
        Stand? selectedStand = stands.FirstOrDefault(s => s.StandName.ToLower() == standName?.ToLower());
        if (selectedStand != null)
        {
            if (selectedStand.IsAvailable)
            {
                Console.WriteLine($"Stand found: {selectedStand.StandName} | {currencyFormat(selectedStand.DailyRentalPrice)} / Day");
                Console.Write("Enter the number of rental days: ");
                int rentalDays;
                while (!int.TryParse(Console.ReadLine(), out rentalDays) || rentalDays <= 0)
                {
                    Console.Write("Please enter a valid number of days: ");
                }
                double tCost = selectedStand.CalculateTotal(rentalDays);
                string totalCost = currencyFormat(tCost);
                Console.WriteLine($"{selectedStand.StandName} has been succesfully rented for {rentalDays}\nTotal: {totalCost}"); 
                selectedStand.ChangeStatus(); // Mark as rented
            }
            else
            {
                Console.WriteLine("Stand is currently unavailable.");
            }
        }
        else
        {
            Console.WriteLine("Stand not found.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.WriteLine("\n\n");
    }

    static void EndRentingStand(List<Stand> stands)
    {
        List<Stand> unavailables = new List<Stand>();

        for (int i = 0; stands.Count > i; i++)
        {
            if (!(stands[i].IsAvailable))
            {
                unavailables.Add(stands[i]);
            } 
        }

        if ((unavailables.Count == 0))
        {
            Console.WriteLine("\nNo stands are currently rented.");
            Console.WriteLine("Press any key to continue...");
            Console.ReadKey();
            Console.WriteLine("\n\n");
            return;
        }
            
        Console.WriteLine(
            "\n\n" +
            centeredAlign("Rented Stands", width) + "\n" +
            new string('=', width - 1) + "\n" +

            // Table Header
            $"{centeredAlign("Stand Name", columnSize)}|" +
            $"{centeredAlign("Price/Day", columnSize)}|" +
            $"{centeredAlign("Status", columnSize)}|" + "\n" +

            new string('=', width - 1)
            );

        for (int i = 0; unavailables.Count > i; i++)
        {
        string ParsedPrice = currencyFormat(unavailables[i].DailyRentalPrice);
            Console.WriteLine(
            $"{centeredAlign(unavailables[i].StandName, columnSize)}|" +
            $"{centeredAlign(ParsedPrice, columnSize)}|" +
            $"{centeredAlign("Rented", columnSize)}|");
        }

        Console.Write("\n\nEnter the name of the stand you want to terminate: ");
        string? standName = Console.ReadLine();
        Stand? selectedStand = stands.FirstOrDefault(s => s.StandName.ToLower() == standName?.ToLower());
        if (selectedStand != null)
        {
            if (!(selectedStand.IsAvailable))
            {
                Console.WriteLine($"Stand found: {selectedStand.StandName} | {currencyFormat(selectedStand.DailyRentalPrice)} / Day");
                Console.WriteLine($"Rent session for {selectedStand.StandName} has been succesfully terminated.");
                selectedStand.ChangeStatus(); // Mark as available
            }
            else
            {
                Console.WriteLine("Stand is not currently rented.");
            }
        }
        else
        {
            Console.WriteLine("Stand not found.");
        }

        Console.WriteLine("Press any key to continue...");
        Console.ReadKey();
        Console.WriteLine("\n\n");
    }

    // Centers the text within a specified width by adding spaces on both sides
    static string centeredAlign(string text, int width)
    {
        int spaces = (width - text.Length) / 2;
        return new string(' ', spaces) + text + new string(' ', spaces + ((text.Length + width) % 2));
    }

    static string currencyFormat(double amount)
    {
        return string.Format("Rp.{0:N0}", amount);
    }
}