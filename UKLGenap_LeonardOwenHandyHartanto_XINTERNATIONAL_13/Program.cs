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
            $"Stand Name    : {standName} \n"
        +   $"Price/Day     : {dailyRentalPrice} \n"
        +   $"Status        : {(isAvailable ? "Available" : "Rented")}"));
    }

    public void ChangeStatus()
    {
        isAvailable = !isAvailable; // Toggle availability
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