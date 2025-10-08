// 代码生成时间: 2025-10-08 19:10:50
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

/// <summary>
/// Represents the Electronic Medical Record System
/// </summary>
public class ElectronicMedicalRecordSystem
{
    private readonly DbContext _context;

    /// <summary>
    /// Initializes a new instance of the ElectronicMedicalRecordSystem class.
    /// </summary>
    /// <param name="context">The database context.</param>
    public ElectronicMedicalRecordSystem(DbContext context)
    {
        _context = context;
    }

    /// <summary>
    /// Adds a new patient record to the database.
    /// </summary>
    /// <param name="patientRecord">The patient record to add.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task AddPatientRecordAsync(PatientRecord patientRecord)
    {
        if (patientRecord == null)
            throw new ArgumentNullException(nameof(patientRecord));

        try
        {
            await _context.AddAsync(patientRecord);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Handle any database errors here
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Retrieves a patient record by patient ID.
    /// </summary>
    /// <param name="patientId">The ID of the patient.</param>
    /// <returns>The patient record if found; otherwise, null.</returns>
    public async Task<PatientRecord> GetPatientRecordByIdAsync(int patientId)
    {
        try
        {
            return await _context.Set<PatientRecord>().FindAsync(patientId);
        }
        catch (Exception ex)
        {
            // Handle any database errors here
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Updates an existing patient record in the database.
    /// </summary>
    /// <param name="patientRecord">The patient record to update.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task UpdatePatientRecordAsync(PatientRecord patientRecord)
    {
        if (patientRecord == null)
            throw new ArgumentNullException(nameof(patientRecord));

        try
        {
            _context.Update(patientRecord);
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Handle any database errors here
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }

    /// <summary>
    /// Deletes a patient record from the database.
    /// </summary>
    /// <param name="patientId">The ID of the patient to delete.</param>
    /// <returns>A task representing the asynchronous operation.</returns>
    public async Task DeletePatientRecordAsync(int patientId)
    {
        try
        {
            var patientRecord = await _context.Set<PatientRecord>().FindAsync(patientId);
            if (patientRecord != null)
            {
                _context.Set<PatientRecord>().Remove(patientRecord);
                await _context.SaveChangesAsync();
            }
        }
        catch (Exception ex)
        {
            // Handle any database errors here
            Console.WriteLine($"An error occurred: {ex.Message}");
            throw;
        }
    }
}

/// <summary>
/// Represents a patient record in the medical record system.
/// </summary>
public class PatientRecord
{
    public int Id { get; set; }
    public string Name { get; set; }
    public DateTime DateOfBirth { get; set; }
    public string MedicalHistory { get; set; }
    public List<TreatmentRecord> Treatments { get; set; } = new List<TreatmentRecord>();
}

/// <summary>
/// Represents a treatment record associated with a patient.
/// </summary>
public class TreatmentRecord
{
    public int Id { get; set; }
    public int PatientRecordId { get; set; }
    public string Description { get; set; }
    public DateTime DateOfTreatment { get; set; }

    public virtual PatientRecord PatientRecord { get; set; }
}
