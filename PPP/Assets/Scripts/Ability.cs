using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public Sprite abilityIcon;
    public int currentCastTimes = 1;
    public int castTimes = 1;
    public abstract void DoEffect(GameObject player);
}
