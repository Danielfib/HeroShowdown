using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System.Linq;

public class FlagSpawnManager : NetworkBehaviour
{
    [SerializeField]
    private GameObject FlagPrefab;

    private Dictionary<TeamIDEnum, Vector3[]> flagSpawnSpots = new Dictionary<TeamIDEnum, Vector3[]>();

    public override void OnStartServer()
    {
        base.OnStartServer();

        TeamIDEnum[] teams = GameObject.FindObjectsOfType<TeamBase>().Select(x => x.teamIdEnum).ToArray();
        foreach(var team in teams)
        {
            flagSpawnSpots.Add(team, GameObject.FindObjectsOfType<FlagSpawnSpot>().Where(x => x.TeamID == team).Select(x => x.transform.position).ToArray());
            SpawnNewFlag(team);
        }
    }

    [Server]
    public void SpawnNewFlag(TeamIDEnum team)
    {
        //chooses at random a spawnspot of given team to spawn flag into
        int rInd = Mathf.RoundToInt(Random.Range(0, flagSpawnSpots[team].Length - 1));
        GameObject flag = Instantiate(FlagPrefab, flagSpawnSpots[team][rInd], Quaternion.identity);
        flag.GetComponent<Flag>().teamIDEnum = team;
        NetworkServer.Spawn(flag);
    }
}
