using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedUP : Ability
{
    [SerializeField] private float boostPower = 10f;
    public override void DoEffect(GameObject player)
    {
        SoundManager.Instance.Play("SpeedUp");
        player.GetComponent<Rigidbody>().AddForce(player.GetComponent<Rigidbody>().velocity * boostPower, ForceMode.Acceleration);
    }
}
