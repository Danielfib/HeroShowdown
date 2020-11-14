﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Character Data")]
public class CharacterSO : ScriptableObject
{
    public string name;
    public string description;
    public Sprite sprite;
    public Sprite HUDIcon;

    public RuntimeAnimatorController upperBodyAnimator;
    public RuntimeAnimatorController lowerBodyAnimator;
    public RuntimeAnimatorController UIAnimator;
}
