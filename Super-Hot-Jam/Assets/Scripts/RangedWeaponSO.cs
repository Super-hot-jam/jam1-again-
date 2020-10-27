using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Ranged Weapon Settings")]
public class RangedWeaponSO : ScriptableObject
{
    public bool hasSpread; // Is the object a shotgun basically
    public float spreadAngle; // Angle of the spread
    public float projectileCount; // Number of projectiles to fire per shot, only takes up 1 ammo
    public float ammoCount; // Amount of ammo in the gun
    public float fireRate; // Rate of fire
    public float throwTime; // How long to hold button before gun is throwable
    public float throwForce; // The force behind the throw
    public float throwTorque; // The torque applied with the throw to make it do a super cool sweet spin
    public float pickupRadius; // The radius for the pickup circle
}
