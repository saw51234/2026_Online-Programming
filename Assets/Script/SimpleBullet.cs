using UnityEngine;
using Fusion;

public class SimpleBullet : NetworkBehaviour
{
    [SerializeField] private float speed = 10f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private float hitRadius = 0.3f;

    [Networked] private TickTimer LifeTimer { get; set; }

    [Networked] private PlayerRef Owner { get; set; }

    public void Init(PlayerRef owner)
    {
        Owner = owner;
    }

    public override void Spawned()
    {
        if(Object.HasStateAuthority)
        {
            LifeTimer = TickTimer.CreateFromSeconds(Runner, lifeTime);
        }
    }

    public override void FixedUpdateNetwork()
    {
        if (!Object.HasStateAuthority) return;

        transform.position += transform.forward * speed * Runner.DeltaTime;

        if(Object.HasStateAuthority && LifeTimer.Expired(Runner))
        {
            Runner.Despawn(Object);
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, hitRadius);

        foreach(var  hit in hits)
        {
            SimplePlayer player = hit.GetComponentInParent<SimplePlayer>();

            if(player == null) continue;

            if(player.Object.InputAuthority== Owner) continue;

            Debug.Log($"ÃÑ¾ËÀÌ ÇÃ·¹ÀÌ¾î¸¦ ¸ÂÃã : {player.Object.InputAuthority}");

            Runner.Despawn(Object);

            return;
        }
    }



}
