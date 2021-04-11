using UnityEngine;

public class Projectile : BaseGame
{
    [SerializeField] private uint hitDamage;   // Unsigned to avoid negative damage input
    public uint HitDamage
    {
        get => hitDamage;
        set => hitDamage = value;
    }
}
