using System.Reflection;

namespace Bazneshastegi.Presentation;

public static class PresentationAssemblyReference
{
    public static Assembly Assembly => Assembly.GetExecutingAssembly();
}