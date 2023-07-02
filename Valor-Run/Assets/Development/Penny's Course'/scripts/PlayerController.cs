using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Animator anim;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    public void StopJummp()
    {
        anim.SetBool("isJumping", false);
    }

    public void StopMagic()
    {
        anim.SetBool("isMagic", false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            anim.SetBool("isJumping", true);
        }

        else if (Input.GetKeyDown(KeyCode.M))
        {
            anim.SetBool("isMagic", true);
        }

        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            transform.Rotate(Vector3.up * 90);
        }

        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            transform.Rotate(-Vector3.up * 90);
        }

        else if (Input.GetKeyDown(KeyCode.A))
        {
            transform.Translate(-0.1f,0,0);
        }

        else if (Input.GetKeyDown(KeyCode.D))
        {
            transform.Translate(0.1f, 0, 0);
        }
    }


}
