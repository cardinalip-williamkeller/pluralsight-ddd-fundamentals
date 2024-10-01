using ArchitectureTests.Base;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.NUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests.Domain;

public class ConstantsTests : ArchitectureTestBase
{
  private static string ConstantsRootNamespace { get => string.Concat(DomainAssemblyName, ".Constants"); }

  private static GivenTypesConjunction AllConstants => Types()
    .That()
    .ResideInNamespace(ConstantsRootNamespace);

  [Test]
  public void ConstantsShouldBePublic()
  {
    var types = AllConstants;

    var rule = types
      .Should()
      .BePublic()
      .Because("Constants should be public");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ConstantsShouldHaveNameEndingWithConstants()
  {
    var types = AllConstants;

    var rule = types
      .Should()
      .HaveNameEndingWith("Constants")
      .Because("Constants should have name ending with Constants");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ConstantsShouldResideInCorrectNamespace()
  {
    var types = Types()
      .That()
      .HaveNameEndingWith("Constants")
      .And()
      .ResideInAssembly(string.Concat(RootApplicationAssemblyName, "*"), useRegularExpressions: true);

    var rule = types
      .Should()
      .ResideInNamespace(ConstantsRootNamespace)
      .Because("Constants should reside in correct namespace");

    _architecture.CheckRule(rule);
  }
}
