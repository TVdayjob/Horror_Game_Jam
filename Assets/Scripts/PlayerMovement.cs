using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed = 10f;
    public float gravity = -9.18f;
    public float jumpForce = 10f;
    public float normalSpeed = 7f;
    public float shiftWalkSpeed = 6f;
    //public float walkAnimSpeed = 3f;
    //public int runAnimSpeed = 5;
    public float climbSpeed = 4f;
    public bool isGrounded;
    public bool isClimbing;
    public Transform groundCheck;
    public Transform frontCheck;
    public GameObject playerBody;
    //public Animator myAnim;
    //public Animator pBody_Anim;
    public float groundDistance = 0.4f;
    //private AudioSource audioSrc;
    public LayerMask groundLayer;
    public LayerMask climbLayer;
    public bool moving;
    public bool shooting = false;
    Vector3 velocity;

    // Start is called before the first frame update
    void Start()
    {
        //audioSrc = GetComponent<AudioSource>();
        //myAnim = playerBody.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(Move());
    }

    public IEnumerator Move()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundLayer);
        isClimbing = Physics.CheckSphere(frontCheck.position, groundDistance, climbLayer);
        moving = (x != 0 || z != 0);
        bool shiftWalking = false;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            velocity.y += jumpForce;
        }

        Vector3 move = transform.right * x + transform.forward * z;
        controller.Move(move * speed * Time.deltaTime);
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        if (isGrounded && Input.GetKey(KeyCode.LeftShift) && moving)
        {
            speed = shiftWalkSpeed;
            //audioSrc.Stop();
            //myAnim.SetInteger("maxSpeed", 0);
            //myAnim.SetFloat("walkSpeed", walkAnimSpeed);
            //pBody_Anim.Play("Player_Body_Walk");
            shiftWalking = true;

        }
        else
        {
            speed = normalSpeed;
            shiftWalking = false;
        }

        //if (isClimbing)
        //{
        //    controller.enabled = false;
        //    isGrounded = false;
        //    gameObject.
        //    controller.Move(climbing);
        //}

        //if (isGrounded && moving && velocity.y < 0 && !shiftWalking && !shooting)
        //{
        //    if (!audioSrc.isPlaying)
        //    {
        //        audioSrc.Play();
        //    }
        //    myAnim.SetInteger("maxSpeed", runAnimSpeed);
        //    myAnim.SetFloat("walkSpeed", walkAnimSpeed);
        //    pBody_Anim.Play("Player_Body_Run");
        //}
        //else
        //{
        //    audioSrc.Stop();
        //    myAnim.SetInteger("maxSpeed", 0);
        //    myAnim.SetFloat("walkSpeed", 0);
        //    pBody_Anim.Play("Player_Body_Idle");

        //}

        yield return new WaitForSeconds(2f);

    }
}
