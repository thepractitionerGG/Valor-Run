using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    GameManager gameManager; // Make SingleTon #

    private void FixedUpdate()
    {
        if (gameManager.GetGameState()!=GameManager.GameState.Running)
                return;

        this.transform.position += PlayerController.player.transform.forward * -9f*Time.deltaTime;

        if (PlayerController.curretPlatorm == null)
            return;

        if (PlayerController.curretPlatorm.tag == "stairsUp")
            transform.Translate(0, -.06f, 0);

        if (PlayerController.curretPlatorm.tag == "stairsDonw")
            transform.Translate(0, .06f, 0);
    }
}
