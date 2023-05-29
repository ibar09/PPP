using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAbility : MonoBehaviour
{


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            //DoEffect(other.gameObject);
            Destroy(gameObject);
            Debug.Log("dkhalt");
            GameManager.Instance.getRandomAbility(other.gameObject.GetComponent<Player>());
            SoundManager.Instance.Play("AbilityPickUp");
        }
    }
}
