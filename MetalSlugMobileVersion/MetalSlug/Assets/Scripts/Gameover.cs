using UnityEngine;
using System.Collections;

public class Gameover : MonoBehaviour {

    void Update()
    {
        if (DigitDisplay.life < 1)
        {
           
            Application.LoadLevel("gameover5");
        }

    }
}
