using System;
using System.Collections.Generic;
using PluralsightDdd.SharedKernel;

namespace ClinicManagement.Domain.ValueObjects
{
  public class AnimalValueObject : ValueObject, IEquatable<AnimalValueObject>
  {
    public string Species { get; }
    public string Breed { get; }

    public AnimalValueObject() { }

    public AnimalValueObject(string species, string breed)
    {
      Species = species;
      Breed = breed;
    }

    protected override IEnumerable<object> GetEqualityComponents()
    {
      yield return Breed;
      yield return Species;
    }

    public override bool Equals(object obj)
    {
      if (obj != null && GetType() == obj.GetType() && obj is AnimalValueObject other)
      {
        return Species == other.Species && Breed == other.Breed;
      }

      return false;
    }

    public override int GetHashCode()
    {
      return HashCode.Combine(base.GetHashCode(), Species, Breed);
    }

    public bool Equals(AnimalValueObject other)
    {
      if (other is null)
      {
        return false;
      }

      if (ReferenceEquals(this, other))
      {
        return true;
      }

      return base.Equals(other) && Species == other.Species && Breed == other.Breed;
    }
  }
}
