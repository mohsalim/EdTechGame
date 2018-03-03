using UnityEngine;

/// <summary>
/// Dialogue contains sentences spoken by a certain character.
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

}
