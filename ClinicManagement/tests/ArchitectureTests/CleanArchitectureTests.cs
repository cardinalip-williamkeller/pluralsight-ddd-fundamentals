using ArchitectureTests.Base;
using ArchUnitNET.NUnit;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests;

public class CleanArchitectureTests : ArchitectureTestBase
{
  [Test]
  public void DomainShouldNotDependOnOtherProjects()
  {
    var domainTypes = Types()
      .That()
      .ResideInNamespace($"{DomainAssemblyName}*", true);

    var typesFromOtherSolutionProjects = Types()
      .That()
      .ResideInAssembly($"{RootApplicationAssemblyName}*", true)
      .And()
      .DoNotResideInNamespace($"{DomainAssemblyName}*", true);

    var rule = domainTypes
      .Should()
      .NotDependOnAny(typesFromOtherSolutionProjects)
      .Because("Types in the domain assembly should not depend on types from other solution projects to maintain clean architecture.");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void InfrastructureShouldNotHaveDependencyOnSomeProjects()
  {
    var infrastructureAssembly = typeof(ClinicManagement.Infrastructure.EmailSender).Assembly;

    var otherProjectsTypes = Types()
      .That()
      .ResideInNamespace($"{RootApplicationAssemblyName}.Api")
      .Or()
      .ResideInNamespace($"{RootApplicationAssemblyName}.BlazorShared")
      .Or()
      .ResideInNamespace($"{RootApplicationAssemblyName}.Blazor.Host");

    var rule = Types()
      .That()
      .ResideInAssembly(infrastructureAssembly)
      .Should()
      .NotDependOnAny(otherProjectsTypes)
      .Because("Types in the infrastructure assembly should not depend on types from certain projects to maintain clean architecture.");

    _architecture.CheckRule(rule);
  }
}
