/// <summary>
/// Symbol Set
/// Joonas Erho, 12.6.2022
/// 
/// Scriptable object that allows for easier making of symbol sets or "levels"
/// for the game. Currently supports a name and the array of sprites. Plans are
/// to extend this to support unlocks, different orders for symbols and other
/// features.
/// </summary>

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "SymbolSet", menuName = "Cycle1Reactor/SymbolSet", order = 0)]
public class SymbolSet : ScriptableObject {
    public string setName;
    public Sprite[] sprites;
}
