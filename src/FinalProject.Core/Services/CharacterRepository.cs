using System.Text.Json;
using FinalProject.Core.Models;

namespace FinalProject.Core.Services;

// Loads characters from JSON and stores them in a Dictionary for fast lookup
public class CharacterRepository
{
    // Dictionary satisfies the data structure requirement — O(1) lookup by ID
    private readonly Dictionary<string, Character> _characters = new();

    // Tracks recently viewed character IDs — satisfies Queue requirement
    private readonly Queue<string> _recentlyViewed = new();
    private const int MaxRecentCount = 5;

    // Loads characters from the embedded JSON file
    public void Load(string jsonPath)
    {
        try
        {
            string json = File.ReadAllText(jsonPath);
            var list = JsonSerializer.Deserialize<List<Character>>(json, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            if (list == null) throw new InvalidDataException("Character data was empty or malformed.");

            _characters.Clear();
            foreach (var c in list)
                _characters[c.Id] = c;
        }
        catch (FileNotFoundException)
        {
            throw new FileNotFoundException($"Character data file not found at: {jsonPath}");
        }
        catch (JsonException ex)
        {
            throw new InvalidDataException($"Failed to parse character JSON: {ex.Message}");
        }
    }

    // Returns all characters as a list
    public List<Character> GetAll() => _characters.Values.ToList();

    // Looks up a single character by ID, returns null if not found
    public Character? GetById(string id)
    {
        _characters.TryGetValue(id, out var character);

        // Track recently viewed
        if (character != null)
        {
            _recentlyViewed.Enqueue(id);
            if (_recentlyViewed.Count > MaxRecentCount)
                _recentlyViewed.Dequeue();
        }

        return character;
    }

    // Filters by element (e.g. "Fire", "Ice")
    public List<Character> GetByElement(string element) =>
        _characters.Values.Where(c => c.Element == element).ToList();

    // Filters by path (e.g. "Hunt", "Harmony")
    public List<Character> GetByPath(string path) =>
        _characters.Values.Where(c => c.Path == path).ToList();

    // Filters by rarity (4 or 5)
    public List<Character> GetByRarity(int rarity) =>
        _characters.Values.Where(c => c.Rarity == rarity).ToList();

    // Returns recently viewed character IDs (most recent last)
    public List<string> GetRecentlyViewed() => _recentlyViewed.ToList();

    // Returns total number of characters loaded
    public int Count => _characters.Count;
}