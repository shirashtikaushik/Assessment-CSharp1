using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;

enum JoiningDateFilter
{
    All,
    Before,
    After,
    On
}

class Developer
{
    public int Id { get; set; }
    public string DeveloperName { get; set; }
    public DateTime JoiningDate { get; set; }
    public string Project_Assigned { get; set; }

    // OnContract properties
    public int Duration { get; set; }
    public decimal Charges_Per_Day { get; set; }

    // OnPayroll properties
    public string Dept { get; set; }
    public string Manager { get; set; }
    public decimal NetSalary { get; set; }
    public int Exp { get; set; }

    // Salary components
    public decimal DA { get; set; }
    public decimal HRA { get; set; }
    public decimal LTA { get; set; }
    public decimal PF { get; set; }

    // Constructor for OnContract Developer
    public Developer(int id, string name, DateTime joinDate, string project, int duration, decimal chargesPerDay)
    {
        Id = id;
        DeveloperName = name;
        JoiningDate = joinDate;
        Project_Assigned = project;
        Duration = duration;
        Charges_Per_Day = chargesPerDay;
    }

    // Constructor for OnPayroll Developer
    public Developer(int id, string name, DateTime joinDate, string project, string dept, string manager, decimal netSalary, int exp)
    {
        Id = id;
        DeveloperName = name;
        JoiningDate = joinDate;
        Project_Assigned = project;
        Dept = dept;
        Manager = manager;
        NetSalary = netSalary;
        Exp = exp;

        // Calculate salary components
        CalculateSalaryComponents();
    }

    // Method to calculate salary components for OnPayroll Developer
    private void CalculateSalaryComponents()
    {
        
        DA = NetSalary * 0.2m;
        HRA = NetSalary * 0.3m;
        LTA = NetSalary * 0.1m;
        PF = NetSalary * 0.12m;
    }
    public decimal CalculateTotalCharges()
    {
        return Duration * Charges_Per_Day;
    }

    
    public void DisplayDetails()
    {
        Console.WriteLine($"Developer Id: {Id}");
        Console.WriteLine($"Developer Name: {DeveloperName}");
        Console.WriteLine($"Joining Date: {JoiningDate.ToShortDateString()}");
        Console.WriteLine($"Project Assigned: {Project_Assigned}");

        if (!string.IsNullOrEmpty(Dept))
        {
           
            Console.WriteLine($"Department: {Dept}");// OnPayroll
            Console.WriteLine($"Manager: {Manager}");
            Console.WriteLine($"Net Salary: {NetSalary:C}");
            Console.WriteLine($"Experience: {Exp} years");
            Console.WriteLine($"DA: {DA:C}, HRA: {HRA:C}, LTA: {LTA:C}, PF: {PF:C}");
        }
        else
        {
           
            Console.WriteLine($"Duration: {Duration} days");// OnContract
            Console.WriteLine($"Charges Per Day: {Charges_Per_Day:C}");
            Console.WriteLine($"Total Charges: {CalculateTotalCharges():C}");
        }

        Console.WriteLine("-----------------------------------------");
    }
}

class Program
{
    static List<Developer> developers = new List<Developer>();

    static void Main()
    {
        int option;
        do
        {
            Console.WriteLine("1. Create Developer");
            Console.WriteLine("2. Display Developer Details");
            Console.WriteLine("3. Exit");
            Console.Write("Enter option (1-3): ");
            if (int.TryParse(Console.ReadLine(), out option))
            {
                switch (option)
                {
                    case 1:
                        CreateDeveloper();
                        break;
                    case 2:
                        DisplayDetails();
                        break;
                    case 3:
                        Console.WriteLine("Exiting program.");
                        break;
                    default:
                        Console.WriteLine("Invalid option. Please enter a valid option.");
                        break;
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid option.");
            }

        } while (option != 3);
    }

    static void CreateDeveloper()
    {
        Console.WriteLine("Select Developer Type:");
        Console.WriteLine("1. OnContract");
        Console.WriteLine("2. OnPayroll");
        Console.Write("Enter option (1-2): ");
        if (int.TryParse(Console.ReadLine(), out int developerType))
        {
            if (developerType == 1)
            {
                CreateOnContractDeveloper();
            }
            else if (developerType == 2)
            {
                CreateOnPayrollDeveloper();
            }
            else
            {
                Console.WriteLine("Invalid option. Please enter a valid option.");
            }
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid option.");
        }
    }

    static void CreateOnContractDeveloper()
    {
        try
        {
            Console.Write("Enter Developer Id: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Developer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Joining Date (MM/dd/yyyy): ");
            DateTime joinDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Project Assigned: ");
            string project = Console.ReadLine();

            Console.Write("Enter Duration (in days): ");
            int duration = int.Parse(Console.ReadLine());

            Console.Write("Enter Charges Per Day: ");
            decimal chargesPerDay = decimal.Parse(Console.ReadLine());

            Developer onContractDeveloper = new Developer(id, name, joinDate, project, duration, chargesPerDay);
            developers.Add(onContractDeveloper);
            Console.WriteLine("OnContract Developer created successfully!");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void CreateOnPayrollDeveloper()
    {
        try
        {
            Console.Write("Enter Developer Id: ");
            int id = int.Parse(Console.ReadLine());

            Console.Write("Enter Developer Name: ");
            string name = Console.ReadLine();

            Console.Write("Enter Joining Date (MM/dd/yyyy): ");
            DateTime joinDate = DateTime.Parse(Console.ReadLine());

            Console.Write("Enter Project Assigned: ");
            string project = Console.ReadLine();

            Console.Write("Enter Department: ");
            string dept = Console.ReadLine();

            Console.Write("Enter Manager: ");
            string manager = Console.ReadLine();

            Console.Write("Enter Net Salary: ");
            decimal netSalary = decimal.Parse(Console.ReadLine());

            Console.Write("Enter Experience (in years): ");
            int exp = int.Parse(Console.ReadLine());

            Developer onPayrollDeveloper = new Developer(id, name, joinDate, project, dept, manager, netSalary, exp);
            developers.Add(onPayrollDeveloper);
            Console.WriteLine("OnPayroll Developer created successfully!");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error: {ex.Message}");
        }
    }

    static void DisplayDetails()
    {
        if (developers.Count == 0)
        {
            Console.WriteLine("No developers to display.");
            return;
        }

        Console.WriteLine("Select Display Option:");
        Console.WriteLine("1. Display All Developers");
        Console.WriteLine("2. Display Developers with Net Salary > 20000");
        Console.WriteLine("3. Display Developers with DeveloperName containing 'D'");
        Console.WriteLine("4. Display Developers joined between 01/01/2020 and 01/01/2022");
        Console.WriteLine("5. Display Developers joined on 12 Jan 2022");
        Console.Write("Enter option (1-5):");

        if (int.TryParse(Console.ReadLine(), out int displayOption))
        {
            switch (displayOption)
            {
                case 1:
                    DisplayAllDevelopers();
                    break;
                case 2:
                    DisplayDevelopersWithHighSalary();
                    break;
                case 3:
                    DisplayDevelopersWithNameContainingD();
                    break;
                case 4:
                    DisplayDevelopersJoinedBetweenDates(new DateTime(2020, 1, 1), new DateTime(2022, 1, 1));
                    break;
                case 5:
                    DisplayDevelopersJoinedOnDate(new DateTime(2022, 1, 12));
                    break;
                default:
                    Console.WriteLine("Invalid option. Please enter a valid option.");
                    break;
            }
          
        }
        else
        {
            Console.WriteLine("Invalid input. Please enter a valid option.");
        }
    }

    static void DisplayAllDevelopers()
    {
        Console.WriteLine("All Developers:");
        foreach (var developer in developers)
        {
            developer.DisplayDetails();
        }
    }

    static void DisplayDevelopersWithHighSalary()
    {
        Console.WriteLine("Developers with Net Salary > 20000:");
        var highSalaryDevelopers = developers.Where(d => d.NetSalary > 20000);
        foreach (var developer in highSalaryDevelopers)
        {
            developer.DisplayDetails();
        }
    }

    static void DisplayDevelopersWithNameContainingD()
    {
        Console.WriteLine("Developers with DeveloperName containing 'D':");
        var nameContainingDDevelopers = developers.Where(d => d.DeveloperName.Contains("D", StringComparison.OrdinalIgnoreCase));
        foreach (var developer in nameContainingDDevelopers)
        {
            developer.DisplayDetails();
        }
    }

    static void DisplayDevelopersJoinedBetweenDates(DateTime startDate, DateTime endDate)
    {
        Console.WriteLine($"Developers joined between {startDate.ToShortDateString()} and {endDate.ToShortDateString()}:");
        var developersBetweenDates = developers.Where(d => d.JoiningDate >= startDate && d.JoiningDate <= endDate);
        foreach (var developer in developersBetweenDates)
        {
            developer.DisplayDetails();
        }
    }

    static void DisplayDevelopersJoinedOnDate(DateTime date)
    {
        Console.WriteLine($"Developers joined on {date.ToShortDateString()}:");
        var developersOnDate = developers.Where(d => d.JoiningDate == date);
        foreach (var developer in developersOnDate)
        {
            developer.DisplayDetails();
        }

    }
}






