using System.Text;
using ArchitectureTests.Base;
using ArchUnitNET.Domain;
using ArchUnitNET.Domain.Extensions;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.NUnit;
using PluralsightDdd.SharedKernel;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests.Domain;

public class ValueObjectsTests : ArchitectureTestBase
{
  private static string ValueObjectsRootNamespace { get => string.Concat(DomainAssemblyName, ".ValueObjects"); }

  private static GivenTypesConjunction AllValueObjects => Types()
      .That()
      .ResideInNamespace(ValueObjectsRootNamespace);

  [Test]
  public void ValueObjectsShouldHaveNameEndingWithValueObject()
  {
    var types = AllValueObjects;

    var rule = types
      .Should()
      .HaveNameEndingWith("ValueObject");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ValueObjectsShouldBeAssignableToValueObject()
  {
    var types = AllValueObjects;

    var rule = types
      .Should()
      .BeAssignableTo(typeof(ValueObject));

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ValueObjectsShouldBePublic()
  {
    var types = AllValueObjects;

    var rule = types
      .Should()
      .BePublic();

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ValueObjectsShouldHaveMethodMemberWithNameGetHashCode()
  {
    var types = AllValueObjects;

    var rule = types
      .Should()
      .HaveMethodMemberWithName("GetHashCode()")
      .Because("Value Objects should enforce their own invariants and validation rules.");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ValueObjectsShouldHaveMethodMemberWithNameEquals()
  {
    var types = AllValueObjects;

    var rule = types
      .Should()
      .HaveMethodMemberWithName("Equals(System.Object)")
      .AndShould()
      .ImplementInterface(typeof(IEquatable<>))
      .Because("Two Value Objects are considered equal if their properties have the same values, not based on reference.");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ValueObjectsShouldHaveParameterlessConstructor()
  {
    var types = AllValueObjects;

    var rule = types
      .Should()
      .HaveMethodMemberWithName(".ctor()")
      .Because("Parameterless constructor is required for deserialization purposes");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ValueObjectsShouldRestrictSetters()
  {
    var types = AllValueObjects
      .GetObjects(_architecture);

    var violations = new StringBuilder();
    foreach (var type in types)
    {
      var propertyMembers = type.GetPropertyMembers();
      foreach (var propertyMember in propertyMembers)
      {
        var setterIsPublic = propertyMember.SetterVisibility == Visibility.Public;
        if (propertyMember.Setter != null && setterIsPublic)
          violations.AppendLine($"Type: {type.FullName}, Property: {propertyMember.Name} has {propertyMember.SetterVisibility} setter, but Value Objects should not have public setters to keep immutability.");
      }
    }

    Assert.That(violations is { Length: 0 }, violations.ToString());
  }

  [Test]
  public void ValueObjectsShouldNotHaveUniqueIdentifier()
  {
    var types = AllValueObjects;

    var rule = types
      .Should()
      .NotBeAssignableTo(typeof(BaseEntity<>))
      .AndShould()
      .NotHavePropertyMemberWithName("Id")
      .Because("Value Objects do not have a unique identifier. Their identity is defined by their attributes, not by a unique ID");

    _architecture.CheckRule(rule);
  }
}
