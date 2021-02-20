using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Battery" && Input.GetKeyUp(KeyCode.E))
        {
            FirstPersonMovement.battery += 100f;
            Debug.Log("Заряд: " + FirstPersonMovement.battery);
            Destroy(other.gameObject);
        }
    }
}
