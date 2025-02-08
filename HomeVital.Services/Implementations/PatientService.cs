// service for patient

using System.Threading.Tasks;
using HomeVital.Services.Interfaces;
using HomeVital.Models.Entities;
using HomeVital.Repositories.dbContext; // Add this using directive
using Microsoft.EntityFrameworkCore;
namespace HomeVital.Services
{
    public class PatientService : IPatientService
    {
        private readonly HomeVitalDbContext _context;

        public PatientService(HomeVitalDbContext context)
        {
            _context = context;
        }
        public async Task<Patient[]> GetPatientsAsync()
        {
            return await _context.Patients.ToArrayAsync();
        }
        public async Task<Patient?> GetPatientByIdAsync(int id)
        {
            return await _context.Patients.FindAsync(id)!;
        }
        public async Task<Patient> CreatePatientAsync(Patient patient)
        {
            _context.Patients.Add(patient);
            await _context.SaveChangesAsync();
            return patient;
        }
        public async Task<bool> DeletePatientAsync(int id)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return false;
            }
            _context.Patients.Remove(patient);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<Patient?> UpdatePatientAsync(int id, Patient updatedPatient)
        {
            var patient = await _context.Patients.FindAsync(id);
            if (patient == null)
            {
                return null;
            }
            patient.Name = updatedPatient.Name;
            patient.Phone = updatedPatient.Phone;
            patient.TeamID = updatedPatient.TeamID;
            patient.Status = updatedPatient.Status;
            await _context.SaveChangesAsync();
            return patient;
        }
    }
}
