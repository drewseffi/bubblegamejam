using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    public CharacterController controller;

    public Transform groundCheck;
    public float groundDistance= 0.4f;
    public LayerMask groundMask;
    public LayerMask bubble;

    public float jumpHeight = 10f;

    public float speed = 12f;
    public float gravity = -9.81f;

    public Camera cam;

    private Vector3 impact = Vector3.zero;

    //public AudioSource footstepSound;

    Vector3 velocity;
    bool isGrounded;

    public float shotgunForce = 15;
    private int shotgunCharges = 2;

    private AudioSource src;

    // Start is called before the first frame update
    void Start()
    {
        src = gameObject.GetComponent<AudioSource>();
        //StartCoroutine(Footsteps());
    }

    // Update is called once per frame
    void Update()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if(isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
            shotgunCharges = 2;
        }

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;
        move = move.normalized;

        bubbleCollision(move);

        //Shotgun jump check
        if (Input.GetMouseButtonDown(1) && shotgunCharges > 0 && StateManager.Instance.weapon == 2)
        {
            AddImpact(-cam.transform.forward, shotgunForce);
            shotgunCharges--;
        }

        if (impact.magnitude > 0.2F)
        {
            controller.Move(impact * Time.deltaTime);
        }

        controller.Move(move * speed * Time.deltaTime);

        //Jump checks
        if(Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y += Mathf.Sqrt(jumpHeight * -2 * gravity);
            audioManager.Instance.PlaySFX("PlayerJump", src);
        }

        impact = Vector3.Lerp(impact, Vector3.zero, 5*Time.deltaTime);

        velocity.y += gravity * Time.deltaTime;

        controller.Move(velocity * Time.deltaTime);

    }

    IEnumerator Footsteps()
    {
        while(true)
        {
            //Checking for footsteps
            if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0 && isGrounded)
            {
                audioManager.Instance.PlaySteps();
                //yield return new WaitForSeconds(0.5f);
            }

        }
    }

    IEnumerator Wait()
    {
        yield return new WaitForSeconds(0.5f);
    }

    void bubbleCollision(Vector3 move)
    {
        RaycastHit hit;

        if (Physics.Raycast(controller.transform.position, move, out hit, 1f, bubble))
        {
            move = move + -move;
        }
    }

    //Shotgun jump impacts
    public void AddImpact(Vector3 dir, float force)
    {
    	dir.Normalize();
    	if (dir.y < 0) dir.y = -dir.y; // reflect down force on the ground
        if (shotgunCharges == 1 && dir.y > dir.x && dir.y > dir.z)
        {
            impact += dir.normalized * force * 2.0f;
        }
        else
        {
            impact += dir.normalized * force;
        }
    }
}