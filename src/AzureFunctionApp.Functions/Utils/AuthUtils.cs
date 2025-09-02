using Microsoft.Azure.Functions.Worker;

namespace AzureFunctionApp.Functions.Utils;

public class AuthUtils
{
    public static T? GetAttribute<T>(FunctionContext context) where T : Attribute
    {
        if (context.FunctionDefinition.EntryPoint == null) return null;
        return System.Reflection.Assembly
                                .GetExecutingAssembly()
                                .GetType(context.FunctionDefinition.EntryPoint.Substring(0,
                                        context.FunctionDefinition.EntryPoint.LastIndexOf('.')))
                                ?.GetMethod(context.FunctionDefinition.EntryPoint.Split('.').Last())
                                ?.GetCustomAttributes(typeof(T), true).Cast<T>().FirstOrDefault();
    }

    public static IEnumerable<T>? GetAttributes<T>(FunctionContext context) where T : Attribute
    {
        if (context.FunctionDefinition.EntryPoint == null) return null;
        return System.Reflection.Assembly
                                .GetExecutingAssembly()
                                .GetType(context.FunctionDefinition.EntryPoint.Substring(0,
                                        context.FunctionDefinition.EntryPoint.LastIndexOf('.')))
                                ?.GetMethod(context.FunctionDefinition.EntryPoint.Split('.').Last())
                                ?.GetCustomAttributes(typeof(T), true).Cast<T>();
    }
}