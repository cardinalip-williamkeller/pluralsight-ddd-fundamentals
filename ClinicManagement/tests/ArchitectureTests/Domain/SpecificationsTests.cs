using ArchitectureTests.Base;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.NUnit;
using Ardalis.Specification;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests.Domain;

public class SpecificationsTests : ArchitectureTestBase
{
  private static string SpecificationsRootNamespace { get => string.Concat(DomainAssemblyName, ".Specifications"); }

  private static GivenTypesConjunction AllSpecifications => Types()
    .That()
    .ResideInNamespace(string.Concat(SpecificationsRootNamespace, "*"), useRegularExpressions: true);

  [Test]
  public void SpecificationsShouldBeAssignableToSpecification()
  {
    var types = AllSpecifications;

    var rule = types
      .Should()
      .BeAssignableTo(typeof(Specification<>));

    _architecture.CheckRule(rule);
  }

  [Test]
  public void SpecificationsShouldBePublic()
  {
    var types = AllSpecifications;

    var rule = types
      .Should()
      .BePublic();

    _architecture.CheckRule(rule);
  }

  [Test]
  public void SpecificationsShouldHaveNameEndingWithSpec()
  {
    var types = AllSpecifications;

    var rule = types
      .Should()
      .HaveNameEndingWith("Spec");

    _architecture.CheckRule(rule);
  }
}
