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

    void DisplayInfo()
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
        double total = (DailyRentalPrice * rentalDays) + (ElectricityFee * rentalDays);
        return total;
    }
}
class PremiumStand : Stand {
    protected double _securityFee = 100000;
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

class Program {
    static void Main(string[] args)
    {
        List<Stand> stands = new List<Stand>();

        stands.Add(new OutdoorStand("Outdoor-1", 450000));
        stands.Add(new OutdoorStand("Outdoor-2", 500000));
        stands.Add(new OutdoorStand("Indoor-1", 600000));
        stands.Add(new OutdoorStand("Indoor-2", 700000));
        stands.Add(new OutdoorStand("Premium-1", 1800000));
        stands.Add(new OutdoorStand("Premium-2", 2000000));

        const int width = 40;
        const int columnSize = 12;

        bool running = true;
        while (running)
        {
            Console.WriteLine(
                centeredAlign("=== Starlight Festival ===", width) +
                centeredAlign("Available Stands", width) +
                new string('=', width) +
                $"{centeredAlign("Stand Name", columnSize)}| {centeredAlign("Price/Day", columnSize)}| {centeredAlign("Status", columnSize)}"
                );
            for (int i = 0; stands.Count > i; i++)
            {
                if (stands[i].IsAvailable)
                {
                    Console.WriteLine($"{stands[i].StandName,columnSize}| {stands[i].DailyRentalPrice,columnSize}| Available");
                }
            }
            Console.Write(
                "\n1. Rent a Stand" +
                "\n2. End Renting a Stand" +
                "\n3. Exit" +
                "\n\nSelect a Menu: ");
            string choice = Console.ReadLine();

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
                    Console.WriteLine("Invalid choice. Please select a valid menu option.\n\n");
                    break;
            }
        }
    }

    static void RentStand(List<Stand> stands)
    {
        Console.Write("Enter the name of the stand you want to rent: ");
        string standName = Console.ReadLine();
        Stand ? selectedStand = stands.FirstOrDefault(s => s.StandName.ToLower() == standName.ToLower());
        if (selectedStand != null)
        {
            if (selectedStand.IsAvailable)
            {
                Console.WriteLine($"Stand found: {selectedStand.StandName} | Rp.{selectedStand.DailyRentalPrice} / Day");
                Console.Write("Enter the number of rental days: ");
                int rentalDays;
                while (!int.TryParse(Console.ReadLine(), out rentalDays) || rentalDays <= 0)
                {
                    Console.Write("Please enter a valid number of days: ");
                }
                double totalCost = selectedStand.CalculateTotal(rentalDays);
                Console.WriteLine($"{selectedStand.StandName} has been succesfully rented for {rentalDays}\nTotal: Rp.{totalCost}"); 
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
    }

    static void EndRentingStand(List<Stand> stands)
    {
        Console.Write("Enter the name of the stand you want to rent: ");
        string standName = Console.ReadLine();
        Stand? selectedStand = stands.FirstOrDefault(s => s.StandName.ToLower() == standName.ToLower());
        if (selectedStand != null)
        {
            if (!(selectedStand.IsAvailable))
            {
                Console.WriteLine($"Stand found: {selectedStand.StandName} | Rp.{selectedStand.DailyRentalPrice} / Day");
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
    }

    // Centers the text within a specified width by adding spaces on both sides
    static string centeredAlign(string text, int width)
    {
        int spaces = (width - text.Length) / 2;
        return new string(' ', spaces) + text + new string(' ', spaces);
    }
}