using UnityEngine;

public enum TextType
{
    Damage,
    Gold,
    Exp,
}

[CreateAssetMenu(fileName = "FloatingText", menuName = "TextUI/FloatingText")]
public class FloatingText : ScriptableObject
{
    public string textName;
    public TextType type;
    public Color color;
}
