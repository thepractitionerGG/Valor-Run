using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scroll : MonoBehaviour
{
    private void FixedUpdate()
    {
        if (PlayerController.isDead)
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
