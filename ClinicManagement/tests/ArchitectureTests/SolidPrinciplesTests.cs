using ArchitectureTests.Base;
using ArchUnitNET.NUnit;
using Ardalis.Specification.EntityFrameworkCore;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests;

public class SolidPrinciplesTests : ArchitectureTestBase
{
  #region S-ingle-responsibility Principle
  //todo: add test for single-responsibility principle
  #endregion

  #region O-pen-closed Principle
  //todo: add test for open-closed principle
  #endregion

  #region L-iskov Substitution Principle
  //todo: add test for Liskov substitution principle
  #endregion

  #region I-nterface Segregation Principle
  //todo: add test for interface segregation principle
  #endregion

  #region D-ependency Inversion Principle

  [Test]
  public void ClassesShouldNotDependOnConcreteRepositoryImplementations()
  {
    var concreteRepositoryImplementations = Classes()
      .That()
      .AreNotAbstract()
      .And()
      .AreAssignableTo(typeof(RepositoryBase<>))
      .And()
      .DoNotResideInNamespace(string.Concat(InfrastructureAssemblyName, ".Repositories", "*"), useRegularExpressions: true);

    var rule = Classes()
      .Should()
      .NotDependOnAny(concreteRepositoryImplementations);

    _architecture.CheckRule(rule);
  }
  #endregion
}
