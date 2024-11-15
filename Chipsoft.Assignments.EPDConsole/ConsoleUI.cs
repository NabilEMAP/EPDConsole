using Chipsoft.Assignments.EPD.DAL.Data;
using Chipsoft.Assignments.EPD.BLL.Exceptions;
using Chipsoft.Assignments.EPD.DAL.Models;
using Chipsoft.Assignments.EPD.BLL.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPDConsole
{
    public class ConsoleUI
    {
        private readonly PatientService _patientService;
        private readonly PhysicianService _physicianService;
        private readonly AppointmentService _appointmentService;

        public ConsoleUI(EPDDbContext context)
        {
            _patientService = new PatientService(context);
            _physicianService = new PhysicianService(context);
            _appointmentService = new AppointmentService(context);
        }

        public void AddPatient()
        {
            Console.WriteLine("\n=== Nieuwe patiënt toevoegen ===");
            try
            {
                var patient = new Patient();

                Console.Write("Voornaam: ");
                patient.FirstName = Console.ReadLine();

                Console.Write("Achternaam: ");
                patient.LastName = Console.ReadLine();

                bool validDate = false;
                while (!validDate)
                {
                    Console.Write("Geboortedatum: ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dob))
                    {
                        patient.DateOfBirth = dob;
                        validDate = true;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldige datumformaat. Probeer opnieuw.");
                    }
                }

                Console.Write("Email: ");
                patient.Email = Console.ReadLine();

                Console.Write("Telefoonnummer: ");
                patient.PhoneNumber = Console.ReadLine();

                Console.Write("Adres: ");
                patient.Address = Console.ReadLine();

                Console.Write("Rijksregisternummer: ");
                patient.InsuranceNumber = Console.ReadLine();

                // Check for existing patient by Insurance Number
                if (_patientService.DoesPatientExistByInsuranceNumber(patient.InsuranceNumber))
                {
                    Console.WriteLine("\nEen patiënt met deze rijksregisternummer bestaat al. Probeer opnieuw.");
                    WaitForKey();
                    return;
                }

                _patientService.AddPatient(patient);
                Console.WriteLine("\nPatiënt succesvol toegevoegd!");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"\nValidatie foutmelding: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFoutmelding: {ex.Message}");
            }

            WaitForKey();
        }

        public void DeletePatient()
        {
            Console.WriteLine("\n=== Patiënt verwijderen ===");
            try
            {
                var patients = _patientService.GetAllPatients();
                if (!patients.Any())
                {
                    Console.WriteLine("Geen patiënten gevonden.");
                    WaitForKey();
                    return;
                }

                DisplayPatientList(patients);

                Console.Write("\nVoer de patiënt-ID in die u wilt verwijderen: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    _patientService.DeletePatient(id);
                    Console.WriteLine("Patiënt succesvol verwijderd!");
                }
                else
                {
                    Console.WriteLine("Ongeldig ID formaat.");
                }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"\nValidatie foutmelding: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFoutmelding: {ex.Message}");
            }

            WaitForKey();
        }

        public void AddPhysician()
        {
            Console.WriteLine("\n=== Nieuwe arts toevoegen ===");
            try
            {
                var physician = new Physician();

                Console.Write("Voornaam: ");
                physician.FirstName = Console.ReadLine();

                Console.Write("Achternaam: ");
                physician.LastName = Console.ReadLine();

                Console.Write("Specialisatie: ");
                physician.Specialization = Console.ReadLine();

                _physicianService.AddPhysician(physician);
                Console.WriteLine("\nArts succesvol toegevoegd!");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"\nValidatie foutmelding: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFoutmelding: {ex.Message}");
            }

            WaitForKey();
        }

        public void DeletePhysician()
        {
            Console.WriteLine("\n=== Arts verwijderen ===");
            try
            {
                var physicians = _physicianService.GetAllPhysicians();
                if (!physicians.Any())
                {
                    Console.WriteLine("Geen artsen gevonden.");
                    WaitForKey();
                    return;
                }

                DisplayPhysicianList(physicians);

                Console.Write("\nVoer de arts-ID in die u wilt verwijderen: ");
                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    _physicianService.DeletePhysician(id);
                    Console.WriteLine("Arts succesvol verwijderd!");
                }
                else
                {
                    Console.WriteLine("Ongeldig ID formaat.");
                }
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"\nValidatie foutmelding: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFoutmelding: {ex.Message}");
            }

            WaitForKey();
        }

        public void AddAppointment()
        {
            Console.WriteLine("\n=== Nieuwe afspraak toevoegen ===");
            try
            {
                var patients = _patientService.GetAllPatients();
                if (!patients.Any())
                {
                    Console.WriteLine("Er zijn geen patiënten geregistreerd. Voeg eerst een patiënt toe.");
                    WaitForKey();
                    return;
                }

                var physicians = _physicianService.GetAllPhysicians();
                if (!physicians.Any())
                {
                    Console.WriteLine("Er zijn geen artsen geregistreerd. Voeg eerst een arts toe.");
                    WaitForKey();
                    return;
                }

                DisplayPatientList(patients);
                DisplayPhysicianList(physicians);

                var appointment = new Appointment();

                // Get patient ID
                bool validPatient = false;
                while (!validPatient)
                {
                    Console.Write("\nVoer een patiënt-ID in: ");
                    if (int.TryParse(Console.ReadLine(), out int patientId) &&
                        patients.Any(p => p.Id == patientId))
                    {
                        appointment.PatientId = patientId;
                        validPatient = true;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldig patiënt-ID. Probeer het opnieuw.");
                    }
                }

                // Get physician ID
                bool validPhysician = false;
                while (!validPhysician)
                {
                    Console.Write("Voer een arts-ID in: ");
                    if (int.TryParse(Console.ReadLine(), out int physicianId) &&
                        physicians.Any(p => p.Id == physicianId))
                    {
                        appointment.PhysicianId = physicianId;
                        validPhysician = true;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldig arts-ID. Probeer het opnieuw.");
                    }
                }

                // Get appointment date and time
                bool validDateTime = false;
                while (!validDateTime)
                {
                    Console.Write("Afspraak (dd/mm/yyyy HH:mm): ");
                    if (DateTime.TryParse(Console.ReadLine(), out DateTime dateTime))
                    {
                        appointment.DateTime = dateTime;
                        validDateTime = true;
                    }
                    else
                    {
                        Console.WriteLine("Ongeldig datum en tijd formaat. Probeer het opnieuw.");
                    }
                }

                Console.Write("Beschrijving: ");
                appointment.Description = Console.ReadLine();

                _appointmentService.AddAppointment(appointment);
                Console.WriteLine("\nAfspraak succesvol toegevoegd!");
            }
            catch (ValidationException ex)
            {
                Console.WriteLine($"\nValidatie foutmelding: {ex.Message}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFoutmelding: {ex.Message}");
            }

            WaitForKey();
        }

        public void ShowAppointments()
        {
            Console.WriteLine("\n=== Afsprakenlijst ===");
            try
            {
                var appointments = _appointmentService.GetAllAppointments();

                if (!appointments.Any())
                {
                    Console.WriteLine("Geen afspraken gevonden.");
                }
                else
                {
                    foreach (var apt in appointments)
                    {
                        Console.WriteLine("\n----------------------------------------");
                        Console.WriteLine($"Datum: {apt.DateTime:dd/MM/yyyy HH:mm}");
                        Console.WriteLine($"Patiënt: {apt.Patient.FirstName} {apt.Patient.LastName}");
                        Console.WriteLine($"Arts: Dr. {apt.Physician.FirstName} {apt.Physician.LastName}");
                        Console.WriteLine($"Beschrijving: {apt.Description}");
                        Console.WriteLine("----------------------------------------");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nFoutmelding: {ex.Message}");
            }

            WaitForKey();
        }

        private void DisplayPatientList(List<Patient> patients)
        {
            Console.WriteLine("\nBeschikbare patiënten:");
            foreach (var p in patients)
            {
                Console.WriteLine($"{p.Id} - {p.FirstName} {p.LastName} ({p.DateOfBirth:dd/MM/yyyy})");
            }
        }

        private void DisplayPhysicianList(List<Physician> physicians)
        {
            Console.WriteLine("\nBeschikbare artsen:");
            foreach (var p in physicians)
            {
                Console.WriteLine($"{p.Id} - Dr. {p.FirstName} {p.LastName} ({p.Specialization})");
            }
        }

        private void WaitForKey()
        {
            Console.WriteLine("\nDruk op een knop om door te gaan...");
            Console.ReadKey();
        }
    }
}
