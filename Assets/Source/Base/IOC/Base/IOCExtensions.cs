using System.Linq;

public static class IOCExtensions
{
    private static Container injector;

    public static void SetDependencyInjector(Container dependencyInjector)
    {
        injector = dependencyInjector;
    }

    public static void Inject<T>(this T classToInject) where T : class
    {
        injector.Inject<T>(classToInject);
    }
    /*public static void Inject<T>(this T classToInject, Container injector) where T : class
    {
        injector.Inject<T>(classToInject);
    }*/
}