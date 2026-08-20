using System.Reflection;

namespace Bazneshastegi.Domain;

public static class DomainAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}