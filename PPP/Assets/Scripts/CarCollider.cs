using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarCollider : MonoBehaviour
{
    private bool test=false;
   
private void  OnTriggerExit(Collider collision) {
    if (collision.gameObject.TryGetComponent<Wall>(out Wall wall)) {
        test=false;
        Debug.Log(test);
    }
}
}
