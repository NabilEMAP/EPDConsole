using Chipsoft.Assignments.EPD.DAL.Data;

namespace Chipsoft.Assignments.EPDConsole
{
    public class Program
    {
        //Don't create EF migrations, use the reset db option
        //This deletes and recreates the db, this makes sure all tables exist
        private static EPDDbContext _context;
        private static ConsoleUI _ui;

        #region FreeCodeForAssignment
        static void Main(string[] args)
        {
            _context = new EPDDbContext();
            _ui = new ConsoleUI(_context);
            while (ShowMenu())
            {
                //Continue
            }
        }

        public static bool ShowMenu()
        {
            Console.Clear();
            foreach (var line in File.ReadAllLines("logo.txt"))
            {
                Console.WriteLine(line);
            }
            Console.WriteLine("");
            Console.WriteLine("1 - Patient toevoegen");
            Console.WriteLine("2 - Patienten verwijderen");
            Console.WriteLine("3 - Arts toevoegen");
            Console.WriteLine("4 - Arts verwijderen");
            Console.WriteLine("5 - Afspraak toevoegen");
            Console.WriteLine("6 - Afspraken inzien");
            Console.WriteLine("7 - Sluiten");
            Console.WriteLine("8 - Reset db");

            if (int.TryParse(Console.ReadLine(), out int option))
            {
                try
                {
                    switch (option)
                    {
                        case 1:
                            _ui.AddPatient();
                            return true;
                        case 2:
                            _ui.DeletePatient();
                            return true;
                        case 3:
                            _ui.AddPhysician();
                            return true;
                        case 4:
                            _ui.DeletePhysician();
                            return true;
                        case 5:
                            _ui.AddAppointment();
                            return true;
                        case 6:
                            _ui.ShowAppointments();
                            return true;
                        case 7:
                            return false;
                        case 8:
                            _context.Database.EnsureDeleted();
                            _context.Database.EnsureCreated();
                            return true;
                        default:
                            return true;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"\nEr is een fout opgetreden: {ex.Message}");
                    Console.WriteLine("Druk op een knop om door te gaan...");
                    Console.ReadKey();
                    return true;
                }
            }
            return true;
        }

        #endregion
    }
}