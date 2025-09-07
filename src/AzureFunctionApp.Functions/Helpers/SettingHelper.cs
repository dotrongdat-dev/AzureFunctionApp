using System.Collections;
using Azure;
using Azure.Security.KeyVault.Secrets;
using AzureFunctionApp.Functions.Configurations;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace AzureFunctionApp.Functions.Helpers;

public class SettingHelper
(
    IConfiguration _configuration,
    SecretClient _secretClient
)
{
    private static Hashtable _cached = new Hashtable();
    private static int _transactionCount = 0;

    public T? GetValue<T>(string key)
    {
        // Retrieve the value from the configuration
        var value = Environment.GetEnvironmentVariable(key) ?? _configuration[key];

        // If the value is null, try to get it from Key Vault
        if (value == null)
        {
            if (_cached.ContainsKey(key))
                value = (string?)_cached[key];
            else
            {
                try
                {
                    value = _secretClient.GetSecret(key.Replace(".", "-")).Value.Value;
                }
                catch (RequestFailedException) { }
                _cached.Add(key, value);
                _transactionCount++;
                Console.WriteLine($"Secret value: {value}");
            }
        }

        Console.WriteLine($"Total transactions: {_transactionCount}");

        if (value == null) return default;
        if (typeof(T) == typeof(string)) return (T)(object) value;

        // Convert the value to the specified type and return it
        return JsonConvert.DeserializeObject<T>(value);
    }
}