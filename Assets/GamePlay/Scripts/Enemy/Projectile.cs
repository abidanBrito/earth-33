using UnityEngine;

public class Projectile : BaseGame
{
    [SerializeField] private uint hitDamage;   // Unsigned to avoid negative damage input
    
    public uint HitDamage
    {
        get => hitDamage;
        set => hitDamage = value;
    }
    public bool firstCollision = true;

    private void OnCollisionEnter(Collision other)
    {
        if(other.gameObject.tag == GameConstants.PLAYER_TAG || other.gameObject.tag == GameConstants.ENEMY_TAG)
        {
            Destroy(gameObject);
        }
    }
}
