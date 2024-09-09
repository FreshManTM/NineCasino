using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character")]
public class CharacterInfo: ScriptableObject
{
    public string Name;
    public string Description;
    public string[] UpgradeName;
    public string[] UpgradeDescription;
    public int[] UpgradePrice;
}
