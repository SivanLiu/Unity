using UnityEngine;
using System.Collections;

public class Gameover2 : MonoBehaviour {

    void Update()
    {
        if (DigitDisplay.life < 1)
        {
            Application.LoadLevel("gameover4");
        }

    }
}
