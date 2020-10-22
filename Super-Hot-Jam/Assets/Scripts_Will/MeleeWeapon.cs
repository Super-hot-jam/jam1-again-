using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Item/Melee")]
public class MeleeWeapon : ScriptableObject
{
    public string weaponName = "Weapon Name";
    public float weaponReach = 0.3f;
    public float arcDistance = 0.3f;
    public float attackSpeed = 1.0f;
}
