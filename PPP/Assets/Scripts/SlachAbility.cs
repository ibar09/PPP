using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlachAbility : Ability
{
    [SerializeField] private Slash slach;
    public override void DoEffect(GameObject player)
    {
        var s = Instantiate(slach.gameObject, player.transform.GetChild(1).transform.position, Quaternion.identity);
        SoundManager.Instance.Play("beam");
        s.GetComponent<Slash>().car = player.transform;
    }
}
