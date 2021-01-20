using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class ProjectileSpawnManager : NetworkBehaviour
{
    [SerializeField] public List<GameObject> projectilePrefabs;

    public override void OnStartServer()
    {
        base.OnStartServer();

        int randomProjectileIndex = Mathf.RoundToInt(Random.Range(0, projectilePrefabs.Count));
        GameObject projectile = Instantiate(projectilePrefabs[randomProjectileIndex]);
        NetworkServer.Spawn(projectile);
    }
}
