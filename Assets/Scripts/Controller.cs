using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Controller : MonoBehaviour
{
    [SerializeField]
    List<string> primitive_names;
    [SerializeField]
    List<Color> loaded_colour;
    [SerializeField]
    GameObject player;
    [SerializeField]
    Camera cam;
    [SerializeField]
    Json_reader json_file;
    [SerializeField]
    GameObject ball_prefab;
    [SerializeField]
    Vector3 offset;
    [SerializeField]
    Quaternion offset_Rot;
    [SerializeField]
    float radius = 5.0f;
    public bool cam_trasition = false;
    float duration = 2f;
    float elapsedTime;
    public GameObject selected_primitive;
    public GameObject init_primative;
    public GameObject ball;
    public int spawn_counter = 0;
    // The camera's sensitivity to mouse input
    public float sensitivity = 10f;
    // The camera's smoothing factor
    public float smoothing = 0.1f;

    public Button myButton;
    public Slider radius_Slider;

    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        player = GameObject.FindGameObjectWithTag("Player");
        ball = GameObject.FindGameObjectWithTag("Ball");
        StartCoroutine(Get_Value_From_Json());
        StartCoroutine(Cam_Follow_Player());

        //For UI OnValueChange
        myButton.onClick.AddListener(delegate { Restart_Game(); });
        radius_Slider.onValueChanged.AddListener(Change_Radius);
    }
    public void Spawn_primatives()
    {
        //For getting Random colour from the list
        int no_of_colours = loaded_colour.Count;
        int random_colour = UnityEngine.Random.Range(0, no_of_colours);

        //A random position within the radius of the player
        Vector3 random_player_position = new Vector3(player.transform.position.x + UnityEngine.Random.Range(-radius, radius), 0.5f, player.transform.position.z + UnityEngine.Random.Range(-radius, radius));

        //For getting Random primatives from the list
        //int no_of_primatives = loaded_primitives.Count;
        //int random_primative = UnityEngine.Random.Range(0, no_of_primatives);

        Select_Primitive_Based_On_Json();

        MeshRenderer selected_primitive_mesh = selected_primitive.AddComponent<MeshRenderer>();
        selected_primitive.AddComponent<Rigidbody>();
        selected_primitive_mesh.material = Resources.Load("Materials/Red") as Material;
        selected_primitive_mesh.material.color = loaded_colour[random_colour];
        selected_primitive.transform.position = random_player_position;
        ball.GetComponent<MeshRenderer>().material.color = loaded_colour[random_colour];
        selected_primitive.tag = "Primatives";

        //Instentiate Random Primatives
        //selected_primitive.AddComponent<Rigidbody>();
        //init_primative = Instantiate(selected_primitive, random_player_position, Quaternion.identity);
        //init_primative.tag = "Primatives";

        //Assign random colours
        //MeshRenderer init_primative_mesh = init_primative.GetComponent<MeshRenderer>();
        //init_primative_mesh.material.color = loaded_colour[random_colour];
        //ball.GetComponent<MeshRenderer>().material.color = loaded_colour[random_colour];

        //Increment the names as per the Json sheet if more then given data make it zero
        if (spawn_counter >= primitive_names.Count - 1)
        {
            spawn_counter = 0;
        }
        else
        {
            spawn_counter++;
        }
    }
    public void Camera_Transition() 
    {
        cam_trasition = true;
    }
    public IEnumerator Cam_Follow_Player() 
    {
        while (true) 
        {
            if (cam_trasition == true)
            {
                elapsedTime += Time.deltaTime;
                float t = elapsedTime / duration;
                cam.transform.position = Vector3.Lerp(player.transform.position, selected_primitive.transform.position, t) + offset;
                if (elapsedTime >= 3.0f) 
                {
                    cam_trasition = false;
                    elapsedTime = 0.0f;
                }
            }
            else 
            {
                //cam.transform.position = player.transform.position + offset;
                //cam.transform.rotation = player.transform.rotation * offset_Rot;

                float horizontal = Input.GetAxis("Mouse X") * sensitivity;
                float vertical = Input.GetAxis("Mouse Y") * sensitivity;

                // Rotate the offset around the player based on the mouse input
                offset = Quaternion.AngleAxis(horizontal, Vector3.up) * Quaternion.AngleAxis(-vertical, cam.transform.right) * offset;

                // Calculate the desired position and rotation of the camera
                Vector3 desiredPosition = player.transform.position + offset;
                Quaternion desiredRotation = Quaternion.LookRotation(player.transform.position - desiredPosition);

                // Smoothly move and rotate the camera towards the desired position and rotation
                cam.transform.position = Vector3.Lerp(cam.transform.position, desiredPosition, smoothing);
                cam.transform.rotation = Quaternion.Lerp(cam.transform.rotation, desiredRotation, smoothing);
            }

            yield return null;
        }
    }
    private IEnumerator Get_Value_From_Json()
    {
        json_file = this.gameObject.GetComponent<Json_reader>();
        if (json_file.myPrimitiveList.primitives.Length == 0) 
        {
            yield return new WaitForSeconds(2.0f);
        }
        foreach (var value in json_file.myPrimitiveList.primitives)
        {
            Debug.Log(value);
            primitive_names.Add(value.name);
            switch (value.colour)
            {
                case "red":
                    loaded_colour.Add(Color.red);
                    break;
                case "blue":
                    loaded_colour.Add(Color.blue);
                    break;
                case "green":
                    loaded_colour.Add(Color.green);
                    break;
                case "yellow":
                    loaded_colour.Add(Color.yellow);
                    break;
                case "black":
                    loaded_colour.Add(Color.black);
                    break;
                default:
                    break;
            }
            yield return null;
        }
    }
    private void Select_Primitive_Based_On_Json()
    {
        switch (primitive_names[spawn_counter])
        {
            case "circle":
                selected_primitive = new GameObject("circle");
                selected_primitive.AddComponent<MeshFilter>().mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
                selected_primitive.AddComponent<SphereCollider>();
                break;
            case "square":
                selected_primitive = new GameObject("square");
                selected_primitive.AddComponent<MeshFilter>().mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
                selected_primitive.AddComponent<BoxCollider>();
                break;
            case "pyramid":
                //Here we generate a Pyramid Mesh
                selected_primitive = CreatePyramid();
                break;
            case "rectangle":
                selected_primitive = new GameObject("rectangle");
                selected_primitive.AddComponent<MeshFilter>().mesh = Resources.GetBuiltinResource<Mesh>("Cube.fbx");
                selected_primitive.AddComponent<BoxCollider>();
                selected_primitive.transform.localScale = new Vector3(1.5f, 0.5f, 1.0f);
                break;
            case "ellipse":
                selected_primitive = new GameObject("ellipse");
                selected_primitive.AddComponent<MeshFilter>().mesh = Resources.GetBuiltinResource<Mesh>("Sphere.fbx");
                selected_primitive.AddComponent<BoxCollider>();
                selected_primitive.transform.localScale = new Vector3(2.0f, 0.5f, 1.0f);
                break;
            default:
                break;
        }
    }
    private GameObject CreatePyramid()
    {
        Vector3[] vertices = {
            new Vector3 (0, 0, 0),
            new Vector3 (1, 0, 0),
            new Vector3 (0.5f, 1, 0.5f),
            new Vector3 (0.5f, 1, 0.5f),
            new Vector3 (0.5f, 1, 0.5f),
            new Vector3 (0.5f, 1, 0.5f),
            new Vector3 (1, 0, 1),
            new Vector3 (0, 0, 1),
        };

        int[] triangles = {
            0, 2, 1, //face front
			0, 3, 2,
            1, 2, 5, //face right
			1, 5, 6,
            0, 7, 4, //face left
			0, 4, 3,
            5, 4, 7, //face back
			5, 7, 6,
            0, 6, 7, //face bottom
			0, 1, 6
        };

        GameObject pyramid = new GameObject ("pyramid");

        Mesh mesh = pyramid.AddComponent<MeshFilter>().mesh;
        pyramid.AddComponent<BoxCollider>();
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.Optimize();
        mesh.RecalculateNormals();

        //mesh_renderer.material.color = Color.blue;

        return pyramid;
    }
    public void Restart_Game() 
    {
        //Reload Game
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void Change_Radius(float value) 
    {
        radius = value;
    }

    //Testing
}