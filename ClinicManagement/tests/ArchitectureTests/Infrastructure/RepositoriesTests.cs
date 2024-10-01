using ArchitectureTests.Base;
using ArchUnitNET.NUnit;
using Ardalis.Specification;
using Ardalis.Specification.EntityFrameworkCore;
using ClinicManagement.Infrastructure.Data;
using ClinicManagement.Infrastructure.Repositories.Base;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests.Infrastructure;

public class RepositoriesTests : ArchitectureTestBase
{
  private static string RepositoriesRootNamespace { get => string.Concat(InfrastructureAssemblyName, ".Repositories"); }

  [Test]
  public void ClassesThatHaveNameEndingWithRepositoryShouldResideInCorrectNamespace()
  {
    var types = Classes()
      .That()
      .HaveNameEndingWith("Repository");

    var rule = types
      .Should()
      .ResideInNamespace(string.Concat(RepositoriesRootNamespace, "*"), useRegularExpressions: true);

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ClassesThatAreAssignableToRepositoryBaseShouldResideInCorrectNamespace()
  {
    var types = Classes()
      .That()
      .AreAssignableTo(typeof(RepositoryBase<>))
      .And()
      .AreNot(typeof(RepositoryBase<>));

    var rule = types
      .Should()
      .ResideInNamespace(string.Concat(RepositoriesRootNamespace, "*"), useRegularExpressions: true);

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ClassesThatAreAssignableToEfRepositoryBaseShouldResideInCorrectNamespace()
  {
    var types = Classes()
      .That()
      .AreAssignableTo(typeof(EfRepository<>))
      .And()
      .AreNot(typeof(EfRepository<>));

    var rule = types
      .Should()
      .ResideInNamespace(string.Concat(RepositoriesRootNamespace, "*"), useRegularExpressions: true);

    _architecture.CheckRule(rule);
  }

  [Test]
  public void ClassesThatImplementInterfaceIRepositoryBaseShouldResideInCorrectNamespace()
  {
    var types = Classes()
      .That()
      .ImplementInterface(typeof(IRepositoryBase<>));

    var rule = types
      .Should()
      .ResideInNamespace(string.Concat(RepositoriesRootNamespace, "*"), useRegularExpressions: true);

    _architecture.CheckRule(rule);
  }

  [Test]
  public void RepositoriesShouldBeAssignableToEfRepository()
  {
    var types = Classes()
      .That()
      .ResideInNamespace(string.Concat(RepositoriesRootNamespace, "*"), useRegularExpressions: true)
      .And()
      .AreNot(typeof(EfRepository<>));

    var rule = types
      .Should()
      .BeAssignableTo(typeof(EfRepository<>));

    _architecture.CheckRule(rule);
  }

  [Test]
  public void RepositoriesShouldDependOnAppDbContext()
  {
    var types = Classes()
      .That()
      .ResideInNamespace(string.Concat(RepositoriesRootNamespace, "*"), useRegularExpressions: true);

    var rule = types
      .Should()
      .DependOnAnyTypesThat()
      .Are(typeof(AppDbContext));

    _architecture.CheckRule(rule);
  }
}
