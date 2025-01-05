using ConsoleTools;
using Logic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure.Internal;
using Models;
using Models.PumpTypes;
using Repository;
using Repository.Interfaces;
using Repository.ModelRepositories;
using System.Net.Http.Headers;
using System.Text.RegularExpressions;

namespace infuzios_pumpa
{
    public class Program
    {
        static PumpDbContext dbContext;
        static IRepository<Pump> repository;
        static PumpLogic logic;
        static void Create()
        {
            //SerialID bekérés
            string serialId = "";
            while (serialId == "")
            {
                Console.WriteLine("Enter Pump's SerialID: ");
                serialId = Console.ReadLine();
            }

            //Pump ProductLine bekérés

            string rawPumpProductLine;
            Regex pl_validpattern = new Regex(@"^(C|CP|S|SP)$");
            while (true)
            {
                Console.WriteLine("Enter Pump's ProductLine (Compact: [C], CompactPlus: [CP], Space: [S], SpacePlus: [SP]): ");
                rawPumpProductLine = Console.ReadLine().ToUpper();
                if (pl_validpattern.IsMatch(rawPumpProductLine))
                {
                    break;
                }
                Console.WriteLine("Invalid input!");
            }

            //Pump Location bekérés
            Room room;
            int rawroomNumber = 0;
            while (true)
            {
                Console.WriteLine("Enter Pump's Location (Room201: [201], Room202: [202], Room203: [203]): ");
                rawroomNumber = int.Parse(Console.ReadLine());
                switch (rawroomNumber)
                {
                    case 201:
                        room = Room.Room201;
                        break;
                    case 202:
                        room = Room.Room202; 
                        break;
                    case 203:
                        room = Room.Room203;
                        break;
                    default:
                        Console.WriteLine("Invalid input!");
                        continue;
                }
                break;
            }

            //Pump Type bekérés

            string rawPumpType;
            Regex pt_validpattern = new Regex(@"^(V|S)$");
            while (true)
            {
                Console.WriteLine("Enter Pump's Type (Volumetric: [V], Syringe: [S]): ");
                rawPumpType = Console.ReadLine().ToUpper();
                if (pt_validpattern.IsMatch(rawPumpType))
                {
                    break;
                }
                Console.WriteLine("Invalid input!");
            }
            
            //PumpType Convertálás
            
            Models.Type pumpType = rawPumpType switch
            {
                "V" => Models.Type.Volumetric,
                "S" => Models.Type.Syringe,
                _ => throw new ArgumentException("Invalid Pump type!")
            };

            //Pumpa létrehozása

            Pump newPump = rawPumpProductLine switch
            {
                "C" => new CompactPump(serialId, room, pumpType),
                "CP" => new CompactPlusPump(serialId, room, pumpType),
                "S" => new SpacePump(serialId, room, pumpType),
                "SP" => new SpacePlusPump(serialId, room, pumpType),
                _ => throw new ArgumentException("Couldn't create Pump!")
            };
            logic.Create(newPump);
            Console.WriteLine($"Pump {serialId} created successfully!");
            Console.ReadLine();
        }
        static void Remove()
        {
            Console.WriteLine("Enter Pump's SerialID to remove: ");
            string serialIdToRemove = Console.ReadLine();
            logic.Delete(serialIdToRemove);
            Console.WriteLine($"Pump {serialIdToRemove} removed successfully!");
            Console.ReadLine();
        }
        static void ListPumps()
        {
            var pumps = logic.ReadAll().ToList();
            foreach (var pump in pumps)
            {
                Console.WriteLine(pump);
            }
            Console.ReadLine();
        }
        static void ChangePumpLocation()
        {
            Console.WriteLine("Enter Pump's SerialID to update location: ");
            string serialIdToUpdate = Console.ReadLine();
            Console.WriteLine("Enter new Location (Room201: [201], Room202: [202], Room203: [203]): ");
            int newRoomNumber = int.Parse(Console.ReadLine());
            Room newRoom = (Room)newRoomNumber;
            logic.ChangePumpLocation(serialIdToUpdate, newRoom);
            Console.WriteLine($"Pump {serialIdToUpdate} moved to {newRoom} successfully!");
            Console.ReadLine();
        }
        static void Main(string[] args)
        {
            var options = new DbContextOptionsBuilder<PumpDbContext>()
                .UseInMemoryDatabase(databaseName: "myDB")
                .Options;
            dbContext = new PumpDbContext(options);
            repository = new PumpRepository(dbContext);
            logic = new PumpLogic(repository);

            var menu = new ConsoleMenu()
                .Add("Add Pump", () => Create())
                .Add("Remove Pump", () => Remove())
                .Add("List Pumps", () => ListPumps())
                .Add("Change Pump Location", () => ChangePumpLocation())
                .Add("Exit", ConsoleMenu.Close);

            menu.Show();
        }
    }
}
