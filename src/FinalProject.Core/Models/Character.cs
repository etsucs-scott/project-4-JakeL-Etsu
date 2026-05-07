/// <summary>
/// Represents a single Honkai: Star Rail character with all their key attributes.
/// </summary>
namespace FinalProject.Core.Models;

public class Character
{
    /// <summary>Unique identifier for the character (e.g. "himeko")</summary>
    public string Id { get; set; } = string.Empty;

    /// <summary>Display name shown in the UI</summary>
    public string Name { get; set; } = string.Empty;

    /// <summary>Combat element (Fire, Ice, Wind, Lightning, Quantum, Imaginary, Physical)</summary>
    public string Element { get; set; } = string.Empty;

    /// <summary>Character path (Hunt, Erudition, Harmony, Nihility, Preservation, Abundance, Destruction)</summary>
    public string Path { get; set; } = string.Empty;

    /// <summary>Star rarity — either 4 or 5</summary>
    public int Rarity { get; set; }

    /// <summary>Short description of the character's role</summary>
    public string Description { get; set; } = string.Empty;

    /// <summary>URL or path to the character's portrait image</summary>
    public string ImageUrl { get; set; } = string.Empty;
}