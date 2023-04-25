using Blazored.SessionStorage;
using System.Text;
using System.Text.Json;

namespace BlazorAppHNB.Client.Extensions
{
    public static class SessionStorageServiceExtension
    {
        public static async Task SaveItemEncrypredAsync<T>(this ISessionStorageService sessionStorageService, string key, T item)
        { 
            var itemJson = JsonSerializer.Serialize(item);
            var itemJsonBytes = Encoding.UTF8.GetBytes(itemJson);
            var base64Json = Convert.ToBase64String(itemJsonBytes);

            await sessionStorageService.SetItemAsync(key, base64Json);
        }

        public static async Task<T> ReadEncrypredItemAsync<T>(this ISessionStorageService sessionStorageService, string key)
        {
            var base64Json = await sessionStorageService.GetItemAsync<string>(key);
            var itemJsonByte = Convert.FromBase64String(base64Json);
            var itemJson = Encoding.UTF8.GetString(itemJsonByte);
            var item = JsonSerializer.Deserialize<T>(itemJson);
            return item;
        }
    }
}
