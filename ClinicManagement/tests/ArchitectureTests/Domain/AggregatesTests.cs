using ArchitectureTests.Base;
using ArchUnitNET.Fluent.Syntax.Elements.Types;
using ArchUnitNET.NUnit;
using PluralsightDdd.SharedKernel;
using PluralsightDdd.SharedKernel.Interfaces;
using static ArchUnitNET.Fluent.ArchRuleDefinition;

namespace ArchitectureTests.Domain;

public class AggregatesTests : ArchitectureTestBase
{
  private static string AggregatesRootNamespace { get => string.Concat(DomainAssemblyName, ".Aggregates"); }

  private static GivenTypesConjunction AllRootAggregates => Types()
    .That()
    .ImplementInterface(typeof(IAggregateRoot));

  private static GivenTypesConjunction AllEntities => Types()
    .That()
    .AreAssignableTo(typeof(BaseEntity<>))
    .And()
    .AreNot(typeof(BaseEntity<>));

  [Test]
  public void RootAggregatesShouldBeAssignableToBaseEntity()
  {
    var types = AllRootAggregates;

    var rule = types
      .Should()
      .BeAssignableTo(typeof(BaseEntity<>))
      .Because("Aggregates should be assigned to BaseEntity<>");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void RootAggregatesShouldBePublic()
  {
    var types = AllRootAggregates;

    var rule = types
      .Should()
      .BePublic()
      .Because("Root Aggregates should be public");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void RootAggregatesShouldResideInCorrectNamespace()
  {
    var types = AllRootAggregates;

    var rule = types
      .Should()
      .ResideInNamespace(string.Concat(AggregatesRootNamespace, "*"), useRegularExpressions: true)
      .Because("Root Aggregates should reside in correct namespace");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void EntitiesShouldResideInCorrectNamespace()
  {
    var types = AllEntities;

    var rule = types
      .Should()
      .ResideInNamespace(string.Concat(AggregatesRootNamespace, ".*"), useRegularExpressions: true)
      .Because("Entities should reside in correct namespace");

    _architecture.CheckRule(rule);
  }

  [Test]
  public void RootAggregatesShouldNotDependDirectlyOnEntitiesFromOtherRootAggregates()
  {
    var aggregateRootTypes = AllRootAggregates.GetObjects(_architecture);

    foreach (var aggregateRootType in aggregateRootTypes)
    {
      var rule = Types()
        .That()
        .Are(aggregateRootType.FullName)
        .Should()
        .NotDependOnAny(

    Types()
          .That()
          .AreAssignableTo(typeof(BaseEntity<>))
          .And()
          .AreNot(typeof(BaseEntity<>))
          .And()
          .DoNotImplementInterface(typeof(IAggregateRoot))
          .And()
          .DoNotResideInNamespace(aggregateRootType.Namespace.FullName)

        )
        .Because("Root Aggregates should not depend directly on entities from other Root Aggregates");

      _architecture.CheckRule(rule);
    }
  }
}
