using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : CreatureController
{
    protected override void Update()
    {
        PlayerWalk();
        base.Update();
    }

    private void PlayerWalk()
    {
        float x = Input.GetAxisRaw("Horizontal") * Speed;
        float z = Input.GetAxisRaw("Vertical") * Speed;

        if (x == 0 && z == 0)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.6f * Time.deltaTime);
            return;
        }
            
        rb.velocity = new Vector3(x, rb.velocity.y, z);

    }
}
