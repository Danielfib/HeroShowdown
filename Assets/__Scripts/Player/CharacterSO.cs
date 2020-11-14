using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Character Data")]
public class CharacterSO : ScriptableObject
{
    public string name;
    public CharacterAttributes charAttributes;
    public string description;
    public Sprite sprite;
    public Sprite HUDIcon;

    public RuntimeAnimatorController upperBodyAnimator;
    public RuntimeAnimatorController lowerBodyAnimator;
    public RuntimeAnimatorController UIAnimator;
}

[Serializable]
public class CharacterAttributes
{
    public int Mobility;
    public int Killer;

    public Sprite abilityIcon;
    public string abilityTitle;
}

