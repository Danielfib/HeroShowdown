using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class SoundManager : Singleton<SoundManager>
{
    [Header("UI")]
    public AudioClip UIaffirmative;
    public AudioClip UInegative;

    [Header("Match")]
    public AudioClip jump;
    public AudioClip toss;
    public AudioClip dash;
    public AudioClip shield;
    public AudioClip die;
    public AudioClip getFlag;
    public AudioClip score;
    public AudioClip spawnFlag;
    public AudioClip cannonShoot;

    public Dictionary<AudioClipEnum, AudioClip> audioDic;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        audioDic = new Dictionary<AudioClipEnum, AudioClip>
        {
            {AudioClipEnum.TOSS, toss},
            {AudioClipEnum.JUMP, jump },
            {AudioClipEnum.DIE, die },
            {AudioClipEnum.CANNON_SHOT, cannonShoot },
            {AudioClipEnum.DASH, dash },
            {AudioClipEnum.GET_FLAG, getFlag },
            {AudioClipEnum.SCORE, score },
            {AudioClipEnum.SHIELD, shield },
            {AudioClipEnum.SPAWN_FLAG, spawnFlag }
        };
    }
}

public enum AudioClipEnum
{
    JUMP,
    TOSS,
    DASH,
    SHIELD,
    DIE,
    GET_FLAG,
    SCORE,
    SPAWN_FLAG,
    CANNON_SHOT
}
