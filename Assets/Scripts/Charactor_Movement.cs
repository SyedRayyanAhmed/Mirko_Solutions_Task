using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Charactor_Movement : MonoBehaviour
{
    public Rigidbody rb;
    public float playerSpeed = 2.0f;

    float horizontalSpeed = 10.0f;
    float verticalSpeed = 10.0f;
    public bool world_Space = true;
    public bool movement_fixed = false;
    public Controller controller;

    public Toggle toggle_World_Space;

    private void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody>();

        //init the Toggle for OnValueChange
        toggle_World_Space.onValueChanged.AddListener(Enable_World_Space);
    }

    void Update()
    {
        if (controller.cam_trasition == false)
        {
            //Get rotation from mouse X & Y
            float mouse_h = horizontalSpeed * Input.GetAxis("Mouse X");
            float mouse_v = verticalSpeed * Input.GetAxis("Mouse Y");

            transform.Rotate(0, mouse_h, 0);

            //Get keys value from A W S D
            //float xMove = Input.GetAxisRaw("Horizontal");
            //float zMove = Input.GetAxisRaw("Vertical");

            ////Get rotation from mouse X
            //float mouse_h = horizontalSpeed * Input.GetAxis("Mouse X");

            //Vector3 direction = new Vector3(xMove, 0, zMove);
            //if (world_Space == true)
            //{
            //    //if world space
            //    direction = transform.TransformDirection(direction);
            //}
            //rb.velocity = direction.normalized * playerSpeed;
            //transform.Rotate(0, mouse_h, 0);
        }
        else if (controller.cam_trasition == true) 
        {
            //Do nothing
        }
        
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(this.transform.position, 5.0f);
    }
    public void Enable_World_Space(bool value) 
    {
        world_Space = value;
    }
}