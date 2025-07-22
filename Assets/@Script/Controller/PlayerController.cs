using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerController : CreatureController
{
    private Transform foot;
    private Transform eye;
    private Transform body;

    private Tile curTile;
    public float mouseSpeed = 20f;

    private float xRotate;
    private float yRotate;
    private Vector3 moveDir;
    public override bool Init()
    {
        if(base.Init() == false)
            return false;

        foot = transform.Find("Foot");
        eye = transform.Find("Eye");
        body = transform.Find("default");

        Cursor.lockState = CursorLockMode.Locked;
        Camera.main.GetComponent<CameraController>().SetInfo(eye);

        return true;
    }
    protected override void Update()
    {
        DigHit();
        jumpCool = JumpHit();

        MouseRotate();
        Walk();

        if(Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetMouseButton(0))
            Dig();
    
    }
    private void FixedUpdate()
    {
        if (!jumpCool)
           rb.velocity += Vector3.up * 3 * Physics.gravity.y * Time.fixedDeltaTime;
       
    }
    protected override void Walk()
    {
        float x = Input.GetAxis("Horizontal") * Speed;
        float z = Input.GetAxis("Vertical") * Speed;

        Vector3 inputDir = new Vector3(x, 0, z);

        if (inputDir == Vector3.zero && jumpCool)
        {
            rb.velocity = Vector3.Lerp(rb.velocity, Vector3.zero, 0.6f * Time.deltaTime);
            return;
        }

        moveDir = transform.TransformDirection(inputDir);
        if (!jumpCool)
        {
            rb.velocity = new Vector3(-moveDir.x / 2, rb.velocity.y, -moveDir.z / 2) ;
            return;
        }

        rb.velocity = new Vector3(-moveDir.x, rb.velocity.y, -moveDir.z);
    }

    protected override void Jump()
    {
        if (!jumpCool)
            return;

        rb.AddForce(Vector3.up * JumpForce, ForceMode.Impulse);
    }
    protected override void Dig()
    {
        if(curTile == null)
            return;

        curTile.OnDamage(this, 5);
    }
    private bool JumpHit()
    {
        RaycastHit hit;
        bool result = Physics.Raycast(foot.position, Vector3.down, out hit, 0.3f);
        
        return result;
    }

    private void DigHit()
    {
        Tile tile;
        RaycastHit[] hit = Physics.RaycastAll(eye.transform.position  , Camera.main.transform.forward, 5f);
        foreach(var tiles in hit)
        {
            tile = tiles.transform.GetComponent<Tile>();
            if(tile ==null) continue;

            curTile = tile;
            break;
        }
    }

    private void MouseRotate()
    {
        float mouseX = Input.GetAxisRaw("Mouse X") * Time.deltaTime * mouseSpeed;
        float mouseY = Input.GetAxisRaw("Mouse Y") * Time.deltaTime * mouseSpeed;

        yRotate += mouseX;
        xRotate -= mouseY;

        xRotate = Mathf.Clamp(xRotate, -80f, 80f);

        transform.rotation = Quaternion.Euler(0, yRotate, 0);
        Camera.main.transform.rotation = Quaternion.Euler(xRotate, yRotate + 180, 0);
        
    }
}
