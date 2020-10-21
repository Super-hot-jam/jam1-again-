using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ranged Weapon Settings")]
public class RangedWeaponSO : ScriptableObject
{
    public bool hasSpread;
    public float spreadAngle;
    public float projectileCount;
    public float ammoCount;
}
