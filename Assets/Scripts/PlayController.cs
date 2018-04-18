using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PlayController : MonoBehaviour {

    // Use this for initialization
    private CharacterController myController;
    public float gravityForce;
    public float ySpeed;
    public float jumpForce;
    public float hangTime;
    public float hangTimer;
    public float gravityModifier;
    public float forwardSpeed;
    public float runSpeed;
    public float lerpTime;

    public Animator myAnimator;

    //wall jump
    private Quaternion myRotation;
    public bool hasJump;

    //Coin Pickup
    public int coinScore;
    public Text coinText;

    //GameOver
    public static bool isAlive=true;
    public bool isWinner = false;
    public GameObject gameOverText;

    public static int levelCounter;
    public int currentLevel;


  

    void Start() {
        isAlive = true;
        isWinner = false;
        myController = GetComponent<CharacterController>();
        myRotation = transform.rotation;
        
    }

    // Update is called once per frame
    void FixedUpdate() {
       
        myGravity();
        if(isWinner==false)
        {
           
            Jump();
            ForwardMovement();
            SpeedApply();
            GroundLanging();
        }
       
        
    }
    private void Update()
    {
       
        if (Input.GetButtonUp("Fire1"))
        {
            hasJump = false;
        }
        myAnimator.SetBool("IsGrounded", myController.isGrounded);
    }
    void myGravity()
    {
        ySpeed = myController.velocity.y;
        ySpeed -= gravityForce * Time.deltaTime;
    }
    void Jump()
    {
        // if (Input.GetButton("Fire1"))
        if (Input.GetButton("Fire1"))
        {
            if (myController.isGrounded && !hasJump)
            {
                hasJump = true;
                hangTimer = hangTime;
                ySpeed = jumpForce;
                
            }
            else
            {
                if (hangTimer > 0)
                {
                    hangTimer -= Time.deltaTime;
                    ySpeed += gravityModifier * hangTimer * Time.deltaTime;
                }
            }
        }
    }
    void ForwardMovement()
    {
        if (myController.isGrounded )
        {
            if (forwardSpeed <= runSpeed - .1f || forwardSpeed >= runSpeed + .1f)
                forwardSpeed = Mathf.Lerp(forwardSpeed, runSpeed, lerpTime);
            else
                forwardSpeed = runSpeed;
        }
    }
    void SpeedApply()
    {
        myController.Move(transform.forward * forwardSpeed * Time.deltaTime);
        myAnimator.SetFloat("XSpeed", myController.velocity.x);
        myController.Move(new Vector3(0, ySpeed, 0)* Time.deltaTime);
        myAnimator.SetFloat("YSpeed", myController.velocity.y);
    }
    void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.transform.tag == "Finish")
        {
            hit.gameObject.GetComponent<Animator>().SetBool("Winner", true);
            isWinner = true;
            if (levelCounter == currentLevel)
            {
                levelCounter++;
            }
            StartCoroutine(Death());
        }
        GroundLanging();
        WallJump(hit);
        

    }

    void WallJump(ControllerColliderHit hitSent)
    {
        if (!myController.isGrounded && hitSent.normal.y <.1f && hitSent.normal.y > -.1f)
        {
            if (Input.GetButton("Fire1") && !hasJump)
            {
                hasJump = true;
                transform.forward = hitSent.normal;
                transform.rotation = Quaternion.Euler(new Vector3(0, transform.rotation.eulerAngles.y, 0));
                forwardSpeed = runSpeed;
                hangTimer = hangTime;
                ySpeed = jumpForce;
            }
        }
    }
    void GroundLanging()
    {
        if(myRotation != transform.rotation && myController.isGrounded)
        {
            transform.rotation = myRotation;
        }
    }
    void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "Coin")
        {
            coinScore++;
            coinText.text = coinScore.ToString();
            Destroy(other.gameObject);
        }
        if (other.transform.tag == "DeathZone")
        {
            isAlive = false;
            gameOverText.SetActive(true);
            StartCoroutine(Death());
        }
    }
    
    IEnumerator Death()
    {
        yield return new WaitForSeconds(2);
        SceneManager.LoadScene(0);
        
    }
}
