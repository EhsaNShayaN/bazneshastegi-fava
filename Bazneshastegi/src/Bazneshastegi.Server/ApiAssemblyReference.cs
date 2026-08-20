using System.Reflection;

namespace Bazneshastegi.Server;

public static class ApiAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}