using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class SlachAbility : Ability
{
    [SerializeField] private Slash slach;
    public override void DoEffect(GameObject player)
    {
        var s = PhotonNetwork.Instantiate("ElectroSlash", player.transform.GetChild(1).transform.position, Quaternion.identity);
        SoundManager.Instance.Play("beam");
        s.GetComponent<Slash>().car = player.transform;
    }
}
