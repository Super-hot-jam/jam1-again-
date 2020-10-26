using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Melee Weapon", menuName = "Item/Melee")]
public class MeleeWeapon : ScriptableObject
{
    public string weaponName = "Weapon Name";
    public float weaponReach = 0.3f;
    public float pickupRadius = 1.0f;
    public float hitCount; // Amount of hits before weapon breaks
    public float hitRate; // Rate of hit
    public float throwTime; // How long to hold button before weapon is throwable
    public float throwForce; // The force behind the throw
    public float throwTorque; // The torque applied with the throw to make it do a super cool sweet spin
}
