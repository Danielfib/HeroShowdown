using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterSO", menuName = "Character Data")]
public class CharacterSO : ScriptableObject
{
    public string name;
    public Sprite sprite;

    public AnimatorController upperBodyAnimator;
    public AnimatorController lowerBodyAnimator;
}
