using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Character Data")]
public class CharacterSO : ScriptableObject
{
    public string name;
    public Sprite sprite;

    public RuntimeAnimatorController upperBodyAnimator;
    public RuntimeAnimatorController lowerBodyAnimator;
}
