using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Ball_behaviour : MonoBehaviour
{
    [SerializeField]
    Rigidbody rb;
    [SerializeField]
    Transform player_transform;
    [SerializeField]
    Transform anchor_pos;
    [SerializeField]
    Controller controller;
    [SerializeField]
    Slider force_Change_Slider;
    [SerializeField]
    Button shoot_again_button;
    [SerializeField]
    GameObject explosion;

    public float force;
    float momentum;


    // Start is called before the first frame update
    void Start()
    {
        player_transform = GameObject.FindGameObjectWithTag("Player").transform;
        controller = GameObject.FindGameObjectWithTag("Controller").GetComponent<Controller>();
        rb = this.GetComponent<Rigidbody>();

        //init the slider for OnValueChange
        force_Change_Slider.onValueChanged.AddListener(Change_The_Force);
        shoot_again_button.onClick.AddListener(delegate { Shoot_again(); });
    }
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            // Do something here
            rb.isKinematic = false;

            Vector3 direction = transform.position - player_transform.position;
            Vector3 kick_direction = direction.normalized;

            rb.velocity = kick_direction * force;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.name);
        if (collision.gameObject.tag == "Player")
        {
            Vector3 direction = transform.position - collision.transform.position;
            Vector3 kick_direction = direction.normalized;

            rb.velocity = kick_direction * force;

            //if mass is 1 then Momentum = Force
            momentum = force;

            Rigidbody player_rb = collision.gameObject.GetComponent<Rigidbody>();
            player_rb.velocity = Vector3.zero;
            player_rb.angularVelocity = Vector3.zero;
        }

        else if (collision.gameObject.tag == "Primatives")
        {
            Debug.Log("collide with Primative");
            Collided_With_Primative(collision);
        }
    }
    private void Collided_With_Primative(Collision collision)
    {
        Destroy(collision.gameObject, 2.0f);
        this.gameObject.GetComponent<MeshRenderer>().enabled = false;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;

        //Add Explosion
        Rigidbody target_rb = collision.gameObject.GetComponent<Rigidbody>();
        target_rb.AddExplosionForce(1000.0f, collision.gameObject.transform.position, 15.0f, 3.0f, ForceMode.Force);
        collision.gameObject.GetComponent<Collider>().enabled = false;
        //Explosion Particle
        GameObject explosion_GameObject = Instantiate(explosion, collision.gameObject.transform.position, Quaternion.identity);
        Destroy(explosion_GameObject, 2.0f);

        StartCoroutine(Ball_snap_to_root_pos());

        rb.velocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
        controller.Spawn_primatives();
        controller.Camera_Transition();
    }
    IEnumerator Ball_snap_to_root_pos() 
    {
        yield return new WaitForSeconds(2.0f);
        this.gameObject.transform.position = anchor_pos.position;
        this.gameObject.GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<Rigidbody>().isKinematic = true;
        yield return null;
    }
    void Change_The_Force(float value)
    {
        force = value;
    }
    void Shoot_again() 
    {
        StartCoroutine(Ball_snap_to_root_pos());
    }
}
