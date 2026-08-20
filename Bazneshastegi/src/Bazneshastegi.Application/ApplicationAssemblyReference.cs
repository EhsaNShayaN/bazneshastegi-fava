using System.Reflection;

namespace Bazneshastegi.Application;

public static class ApplicationAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}