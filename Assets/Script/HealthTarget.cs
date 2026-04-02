using UnityEngine;
using Fusion;

public class HealthTarget : NetworkBehaviour
{
    [Networked] public int HP { get; set; }

    public override void Spawned()
    {
        if (Object.HasStateAuthority)
        {
            HP = 5;

        }
    }

    public void TakeDamage(int damage)
    {
        if (Object.HasStateAuthority) return;

        HP -= damage;
        Debug.Log($"{name} HP : {HP}");

        if(HP <= 0)
        {
            HP = 5;
            transform.position = Vector3.zero;
            Debug.Log($"{name} ¸®½ºÆù");
        }
    }
}
