using UnityEngine;
using System.Collections;

public class Gift : MonoBehaviour {

    void OnTriggerEnter(Collider other) {
        if (other.gameObject.tag == "Player") {
            Destroy(this.gameObject);
            DigitDisplay.grenade++;
        }
    }
}
