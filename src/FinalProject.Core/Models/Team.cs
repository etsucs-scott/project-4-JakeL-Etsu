/// <summary>
/// Represents a saved team of up to 4 HSR characters.
/// Uses a HashSet internally to prevent duplicate characters.
/// </summary>
namespace FinalProject.Core.Models;

public class Team
{
    /// <summary>Unique ID for this team (used for save/load)</summary>
    public string Id { get; set; } = Guid.NewGuid().ToString();

    /// <summary>User-given name for the team</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>When the team was created</summary>
    public DateTime CreatedAt { get; set; } = DateTime.Now;

    // HashSet prevents duplicate character IDs — satisfies data structure requirement
    private readonly HashSet<string> _characterIds = new();

    /// <summary>Read-only list of character IDs on this team</summary>
    public List<string> CharacterIds
    {
        get => _characterIds.ToList();
        set
        {
            _characterIds.Clear();
            foreach (var id in value) _characterIds.Add(id);
        }
    }

    /// <summary>Max team size allowed in HSR</summary>
    public const int MaxSize = 4;

    /// <summary>
    /// Attempts to add a character to the team.
    /// Returns false if team is full or character is already on the team.
    /// </summary>
    public bool AddCharacter(string characterId)
    {
        if (_characterIds.Count >= MaxSize) return false;
        return _characterIds.Add(characterId); // HashSet.Add returns false if duplicate
    }

    /// <summary>Removes a character from the team by ID.</summary>
    public bool RemoveCharacter(string characterId) =>
        _characterIds.Remove(characterId);

    /// <summary>Returns true if the team has exactly 4 characters.</summary>
    public bool IsComplete() => _characterIds.Count == MaxSize;

    /// <summary>Returns true if the team has no characters.</summary>
    public bool IsEmpty() => _characterIds.Count == 0;

    /// <summary>Returns how many slots are still open.</summary>
    public int SlotsRemaining() => MaxSize - _characterIds.Count;
}