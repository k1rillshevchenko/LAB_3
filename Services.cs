using System.Text.Json;
using LAB_3.Models;

namespace LAB_3.Services
{
    public static class JsonService
    {
        private static readonly JsonSerializerOptions _options = new() { WriteIndented = true };

        public static async Task SaveToFileAsync(string filePath, List<Postgraduate> data)
        {
            var json = JsonSerializer.Serialize(data, _options);
            await File.WriteAllTextAsync(filePath, json);
        }

        public static async Task<List<Postgraduate>> LoadFromFileAsync(string filePath)
        {
            if (!File.Exists(filePath)) return new List<Postgraduate>();
            var json = await File.ReadAllTextAsync(filePath);
            return JsonSerializer.Deserialize<List<Postgraduate>>(json, _options) ?? new List<Postgraduate>();
        }
    }
}
