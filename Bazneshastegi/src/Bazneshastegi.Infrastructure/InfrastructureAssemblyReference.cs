using System.Reflection;

namespace Bazneshastegi.Infrastructure;

public static class InfrastructureAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}