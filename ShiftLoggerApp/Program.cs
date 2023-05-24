using Newtonsoft.Json;
using ShiftLogger;
using ShiftLogger.Controllers;
using ShiftLoggerAPI;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;


HttpClient client = new HttpClient();
client.BaseAddress = new Uri("https://localhost:7102");

async Task Menu()
{
    Console.WriteLine("");
    Console.WriteLine("");
    Console.WriteLine("*********Log Shifts**********");
    Console.WriteLine("1. Log New Shift");
    Console.WriteLine("2. View Past Shifts");
    Console.WriteLine("3. View Past Shifts for Employee");
    Console.WriteLine("4. Update Past Shift");
    Console.WriteLine("5. Delete Past Shift");
    Console.WriteLine("0. Exit");

    string input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        await Menu();

    switch (input)
    {
        case "1":
            await AddShift();
            break;
        case "2":
            await ViewShifts();
            break;
        case "3":
            await ViewEmpShifts();
            break;
        case "4":
            await UpdateShift();
            break;
        case "5":
            await DeleteShift();
            break;
        case "0":
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Invalid input! Try again!");
            await Menu();
            break;

    }
}

async Task AddShift()
{
    Shift newShift = new Shift();
    Console.WriteLine("Enter your employee ID number: ");
    string input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        await Menu();
    else if (int.TryParse(input, out int result))
        newShift.employeeId = result;
    else
    {
        Console.WriteLine("Enter a valid ID!");
        await AddShift();
    }

    Console.WriteLine("Enter the start time of your shift (8:00AM,5/2/23 9:00AM, etc) : ");
    input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        await Menu();
    else if (DateTime.TryParse(input, out DateTime result))
        newShift.Start = result;
    else
    {
        Console.WriteLine("Invalid format! Try again!");
        await AddShift();
    }

    Console.WriteLine("Enter the end time of your shift (5:00PM,5/2/23 4:00PM, etc): ");
    input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        await Menu();
    else if(DateTime.TryParse(input,out DateTime result))
        newShift.End = result;
    else
    {
        Console.WriteLine("Invalid format! Try again!");
        await AddShift();
    }

    var json = JsonConvert.SerializeObject(newShift);
    var content = new StringContent(json,Encoding.UTF8,"application/json");

    var response = client.PostAsync("/api/ShiftLogger", content).Result;

    if (response.IsSuccessStatusCode)
    {
        Console.WriteLine($"A shift has been logged for Employee {newShift.employeeId}");
        Console.WriteLine("Returning to Main Menu...");
    }
    else
    {
        Console.WriteLine("Error: " + response.StatusCode);
    }
    await Menu();
}
async Task ViewShifts()
{
    string response = await client.GetStringAsync("/api/ShiftLogger");
    Shifts shifts = JsonConvert.DeserializeObject<Shifts>(response);
    foreach(Shift shift in shifts)
    {
        Console.WriteLine($"({shift.shiftId}) Employee: {shift.employeeId}: {shift.Start} - {shift.End} Shift Time: {shift.Duration} ");
    }
    await Menu();
}
async Task ViewEmpShifts()
{
    Console.WriteLine("Enter employee ID of the past shifts you want to view: ");
    string input = Console.ReadLine();
    int empID=0;
    if (string.IsNullOrWhiteSpace(input))
        await Menu();
    else if (int.TryParse(input, out int result))
        empID = result;
    else
    {
        Console.WriteLine("Enter a valid ID!");
        await Menu();
    }

    string response = await client.GetStringAsync($"/api/ShiftLogger/{empID}");
    Shifts shifts = JsonConvert.DeserializeObject<Shifts>(response);
    foreach (Shift shift in shifts)
    {
        Console.WriteLine($"({shift.shiftId}) Employee: {shift.employeeId}: {shift.Start} - {shift.End} Shift Time: {shift.Duration} ");
    }
    await Menu();
}
async Task UpdateShift()
{
    Shift updatedShift = new Shift();
    string response = await client.GetStringAsync("/api/ShiftLogger");
    Shifts shifts = JsonConvert.DeserializeObject<Shifts>(response);
    foreach (Shift shift in shifts)
    {
        Console.WriteLine($"({shift.shiftId}) Employee: {shift.employeeId}: {shift.Start} - {shift.End} Shift Time: {shift.Duration} ");
    }
    Console.WriteLine("");
    Console.WriteLine("Enter the (ID) of the shift you want to update: ");
    string input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        await Menu();
    updatedShift.shiftId = int.Parse(input);
    response = await client.GetStringAsync($"/api/ShiftLogger/{updatedShift.shiftId}");
    Shift oldShift = JsonConvert.DeserializeObject<Shift>(response);
    

    Console.WriteLine("Enter an updated Employee ID or press enter to keep it unchanged: ");
    input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        updatedShift.employeeId = oldShift.employeeId;
    else if (int.TryParse(input, out int result))
        updatedShift.employeeId = result;
    else
    {
        Console.WriteLine("Invalid input! Try Again!");
        await Menu();
    }
        
        

    Console.WriteLine("Enter an updated Start Time or press enter to keep it unchanged: ");
    input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        updatedShift.Start = oldShift.Start;
    else if (DateTime.TryParse(input, out DateTime result))
        updatedShift.Start = result;
    else
    {
        Console.WriteLine("Invalid input! Try Again!");
        await Menu();
    }

    Console.WriteLine("Enter an updated End Time or press enter to keep it unchanged: ");
    input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        updatedShift.End = oldShift.End;
    else if (DateTime.TryParse(input, out DateTime result))
        updatedShift.End = result;
    else
    {
        Console.WriteLine("Invalid input! Try Again!");
        await Menu();
    }

    var json = JsonConvert.SerializeObject(updatedShift);
    var content = new StringContent(json, Encoding.UTF8, "application/json");

    var response1 = client.PutAsync($"/api/ShiftLogger/{updatedShift.shiftId}", content).Result;

    if (response1.IsSuccessStatusCode)
    {
        Console.WriteLine($"The shift has been updated!");
        Console.WriteLine("Returning to Main Menu...");
    }
    else
    {
        Console.WriteLine("Error: " + response1.StatusCode);
    }
    await Menu();
}
async Task DeleteShift()
{
    string response = await client.GetStringAsync("/api/ShiftLogger");
    Shifts shifts = JsonConvert.DeserializeObject<Shifts>(response);
    foreach (Shift shift in shifts)
    {
        Console.WriteLine($"({shift.shiftId}) Employee: {shift.employeeId}: {shift.Start} - {shift.End} Shift Time: {shift.Duration} ");
    }


    Console.WriteLine("Enter the (ID) of the Shift you would like to delete: ");
    int shiftID = 0;
    string input = Console.ReadLine();
    if (string.IsNullOrWhiteSpace(input))
        await Menu();
    else if (int.TryParse(input, out int result))
        shiftID = result;
    else {
        Console.WriteLine("Not a valid input! Try again!");
        await Menu();
    }
    Console.WriteLine("Are you sure you want to delete this shift? Enter Yes or No:");
    input = Console.ReadLine();
    switch (input.ToLower())
    {
        case "yes":
            var response1 = client.DeleteAsync($"/api/ShiftLogger/{shiftID}").Result;

            if (response1.IsSuccessStatusCode)
            {
                Console.WriteLine($"The shift has been deleted!");
                Console.WriteLine("Returning to Main Menu...");
            }
            else
            {
                Console.WriteLine("Error: " + response1.StatusCode);
            }
            await Menu();
            break;
        case "no":
            Console.WriteLine("Returning to Main Menu...");
            await Menu();
            break;
        default:
            Console.WriteLine("Invalid input!");
            Console.WriteLine("Returning to Main Menu...");
            await Menu();
            break;
    }
   
}

await Menu();