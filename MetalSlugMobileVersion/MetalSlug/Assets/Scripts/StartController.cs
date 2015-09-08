using UnityEngine;
using System.Collections;

public class StartController : MonoBehaviour {

    private float myTime = 0;
    void Update() {
        myTime += Time.deltaTime;
        if (myTime > 0.08f) {
            Destroy(this.gameObject);
        }
    }
}
