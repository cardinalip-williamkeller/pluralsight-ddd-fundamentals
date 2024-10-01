using ArchitectureTests.Base;
using ArchUnitNET.Fluent.Syntax.Elements.Types.Interfaces;
using ArchUnitNET.NUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests.Domain;

public class InterfacesTests : ArchitectureTestBase
{
  private static string InterfacesRootNamespace { get => string.Concat(DomainAssemblyName, ".Interfaces"); }

  private static GivenInterfacesConjunction AllDomainInterfaces => Interfaces()
    .That()
    .ResideInNamespace(string.Concat(DomainAssemblyName, "*"), useRegularExpressions: true);

  [Test]
  public void InterfacesShouldResideInCorrectNamespace()
  {
    var types = AllDomainInterfaces;

    var rule = types
      .Should()
      .ResideInNamespace(string.Concat(InterfacesRootNamespace, "*"), useRegularExpressions: true);

    _architecture.CheckRule(rule);
  }

  [Test]
  public void InterfacesShouldHaveCorrectPrefix()
  {
    var types = AllDomainInterfaces;

    var rule = types
      .Should()
      .HaveNameStartingWith("I");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void InterfacesShouldBePublic()
  {
    var types = AllDomainInterfaces;

    var rule = types
      .Should()
      .BePublic();

    _architecture.CheckRule(rule);
  }
}
