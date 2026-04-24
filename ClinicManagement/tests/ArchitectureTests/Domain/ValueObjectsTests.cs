using ArchitectureTests.Base;
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
