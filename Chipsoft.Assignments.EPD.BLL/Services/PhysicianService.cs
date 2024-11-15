using Chipsoft.Assignments.EPD.DAL.Data;
using Chipsoft.Assignments.EPD.BLL.Exceptions;
using Chipsoft.Assignments.EPD.DAL.Models;
using Chipsoft.Assignments.EPD.BLL.Validation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Chipsoft.Assignments.EPD.BLL.Services
{
    public class PhysicianService
    {
        private readonly EPDDbContext _context;

        public PhysicianService(EPDDbContext context)
        {
            _context = context;
        }

        public void AddPhysician(Physician physician)
        {
            try
            {
                PhysicianValidator.ValidatePhysician(physician);
                _context.Physicians.Add(physician);
                _context.SaveChanges();
            }
            catch (ValidationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Foutmelding: Toevoegen van Arts naar de database.", ex);
            }
        }

        public void DeletePhysician(int id)
        {
            var physician = _context.Physicians.Find(id);
            if (physician == null)
                throw new ValidationException("Arts niet gevonden.");

            _context.Physicians.Remove(physician);
            _context.SaveChanges();
        }

        public List<Physician> GetAllPhysicians()
        {
            return _context.Physicians.ToList();
        }
    }
}
