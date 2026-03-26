using Fusion;
using UnityEngine;

public class SimplePlayer : NetworkBehaviour
{
    [SerializeField] private float movespeed = 5f;
    [SerializeField] private float rotateSpeed = 10f;

    [Header("Bullet")]
    [SerializeField] private NetworkPrefabRef bulletPrefabs;
    [SerializeField] private Transform firePoint;

    [Networked] private TickTimer FireCoolDown { get; set; }
    [SerializeField] private float fireInterval = 0.2f;

    public override void FixedUpdateNetwork()
    {
        if (GetInput<FusionBootstrap.NetworkInputData>(out var inputData))
        {
            Vector3 move = new Vector3(inputData.move.x, 0f, inputData.move.y);

            if(move.sqrMagnitude > 1f)
                move.Normalize();

            transform.position += move * movespeed * Runner.DeltaTime;

            if(move.sqrMagnitude > 0.001f)
            {
                Quaternion targetRotation = Quaternion.LookRotation(move, Vector3.up);

                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotateSpeed * Runner.DeltaTime);
            }
        }

        if (inputData.buttons.IsSet((int)FusionBootstrap.InputButton.Fire))
        {
            if (FireCoolDown.ExpiredOrNotRunning(Runner))
            {
                Fire();
                FireCoolDown = TickTimer.CreateFromSeconds(Runner, fireInterval);
            }
        }
    }

    private void Fire()
    {
        if (!Object.HasStateAuthority) return;

        Vector3 spawnPos = firePoint != null ? firePoint.position : transform.position + transform.forward + Vector3.up * 0.5f;

        Quaternion spawnRot = transform.rotation;

        NetworkObject bulletObj =  Runner.Spawn(
            bulletPrefabs,
            spawnPos,
            spawnRot,
            Object.InputAuthority
        );

        SimpleBullet bullet = bulletObj.GetComponent<SimpleBullet>();
        if(bullet != null)
        {
            bullet.Init(Object.InputAuthority);
        }
    }

}
