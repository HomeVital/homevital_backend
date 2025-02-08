using System.Threading.Tasks;
using HomeVital.Models.Entities;
using Microsoft.EntityFrameworkCore;
namespace HomeVital.Services.Interfaces
{
    public interface IPatientService
    {
        Task<Patient[]> GetPatientsAsync();
        Task<Patient?> GetPatientByIdAsync(int id);
        Task<Patient> CreatePatientAsync(Patient patient);
        Task<bool> DeletePatientAsync(int id);
        Task<Patient?> UpdatePatientAsync(int id, Patient updatedPatient);
    }
}