using UnityEngine;

/// <summary>
/// Dialogue contains sentences spoken by a certain character.
/// TODO Make this Uppercase.
/// </summary>
[System.Serializable]
public class Dialogue
{
    /// <summary>
    /// Name of character.
    /// </summary>
    public string name;

    /// <summary>
    /// Sentences spoken by character.
    /// </summary>
    [TextArea(3, 10)]
    public string[] sentences;

    /// <summary>
    /// Success message for this problem.
    /// </summary>
    [TextArea(3, 10)]
    public string successMessage;

    /// <summary>
    /// NPC graphic.
    /// TODO Should this be another type?
    /// TODO Not used yet. Implement later.
    /// </summary>
    public string graphic;
}
