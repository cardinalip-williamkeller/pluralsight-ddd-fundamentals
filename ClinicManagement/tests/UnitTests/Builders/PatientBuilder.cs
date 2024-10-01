using ClinicManagement.Domain.Aggregates.ClientAggregate;
using ClinicManagement.Domain.ValueObjects;

namespace UnitTests.Builders
{
  public class PatientBuilder
  {
    private Patient _patient;

    public PatientBuilder()
    {
      WithDefaultValues();
    }

    public PatientBuilder Id(int id)
    {
      _patient.Id = id;
      return this;
    }

    public PatientBuilder SetPatient(Patient patient)
    {
      _patient = patient;
      return this;
    }

    public PatientBuilder WithDefaultValues()
    {
      _patient = new Patient
      {
        ClientId = 1,
        Name = "Test Patient",
        Sex = "MALE"
      };
      _patient.AnimalValueObject = new AnimalValueObject("Cat", "Mixed");

      return this;
    }

    public Patient Build() => _patient;
  }
}
