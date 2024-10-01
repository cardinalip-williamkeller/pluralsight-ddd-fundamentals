using ArchUnitNET.Domain;
using ArchUnitNET.Loader;
using Assembly = System.Reflection.Assembly;

namespace ArchitectureTests.Base;

public abstract class ArchitectureTestBase
{
  protected const string RootApplicationAssemblyName = "ClinicManagement";
  protected static string DomainAssemblyName { get => string.Concat(RootApplicationAssemblyName, ".Domain"); }

  protected static string InfrastructureAssemblyName
  {
    get => string.Concat(RootApplicationAssemblyName, ".Infrastructure");
  }

  protected static readonly Architecture _architecture = new ArchLoader()
    .LoadAssemblies(new[]
    {
      Assembly.Load("ClinicManagement.Api"),
      Assembly.Load("ClinicManagement.Blazor"),
      Assembly.Load("ClinicManagement.Blazor.Host"),
      Assembly.Load("ClinicManagement.BlazorShared"),
      Assembly.Load("ClinicManagement.Domain"),
      Assembly.Load("ClinicManagement.Infrastructure"),
      Assembly.Load("ClinicManagement.JustAnotherProject"),
    })
    .Build();
}
