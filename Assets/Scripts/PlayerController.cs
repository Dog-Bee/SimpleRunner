using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private CharacterController controller;
    private Vector3 dir;
    [SerializeField] private float speed;
    private Animator anim;
    private int lineToMove = 1;
    [SerializeField] private float lineDistance = 3f;
    [SerializeField] private float jumpForce;
    [SerializeField] private float gravity = -20f;
    [SerializeField] private GameObject losePanel;
    [SerializeField] private int coins;
    [SerializeField] private Text cointext;
    private CapsuleCollider coll;
    private bool isSliding;
    [SerializeField] private GameObject FireWorks;
    [SerializeField] private Image progressBar;


    void Start()
    {
        anim = GetComponentInChildren<Animator>();
        controller = GetComponent<CharacterController>();
        Time.timeScale = 1;
        coins = PlayerPrefs.GetInt("Coin");
        cointext.text = coins.ToString();
        coll = GetComponent<CapsuleCollider>();
        FireWorks.SetActive(false);
    }

    // Update is called once per frame

    private void Update()
    {
        DirectionsSwipe();
        Dance();
    }
    void FixedUpdate()
    {
        MovePlayer();
    }
    private void DirectionsSwipe()
    {
        if (SwipeController.swipeRight)
        {
            if (lineToMove < 2)
                lineToMove++;
        }
        if (SwipeController.swipeLeft)
        {
            if (lineToMove > 0)
                lineToMove--;
        }
        if (SwipeController.swipeUp)
        {
            if (controller.isGrounded)
                dir.y = jumpForce;
            anim.SetTrigger("isJumping");
        }
        if(SwipeController.swipeDown)
        {
            StartCoroutine(Slide());
        }

        if(controller.isGrounded && !isSliding)
            anim.SetBool("isRunning", true);        
        else
            anim.SetBool("isRunning", false);
        

        Vector3 targetPosition = transform.position.z * transform.forward + transform.position.y * transform.up;
        if (lineToMove == 0)
            targetPosition += Vector3.left * lineDistance;
        else if (lineToMove == 2)
            targetPosition += Vector3.right * lineDistance;

        Vector3 diff = targetPosition - transform.position;
        Vector3 moveDir = diff.normalized * 25 * Time.deltaTime;
        if (moveDir.sqrMagnitude < diff.sqrMagnitude)
            controller.Move(moveDir);
        else
            controller.Move(diff);


    }
    private void Dance()
    {
        if(progressBar.fillAmount==1)
        {
            FireWorks.SetActive(true);
            anim.SetBool("isRunning", false);
            anim.SetTrigger("isDancing");
            speed = 0;
        }
    }

    private void MovePlayer()
    {
        dir.z = speed;
        dir.y += gravity * Time.deltaTime;
        controller.Move(dir * Time.deltaTime);
    }
    private void OnControllerColliderHit(ControllerColliderHit hit)
    {
        if (hit.gameObject.tag == "Obstacle")
        {
            losePanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            coins++;
            PlayerPrefs.SetInt("Coin", coins);
            cointext.text = coins.ToString();            
            Destroy(other.gameObject);

        }
    }

    private IEnumerator Slide()
    {
        coll.center= new Vector3(0, 0.4f, 0);
        coll.height = 2;
        isSliding = true;
        anim.SetTrigger("isSliding");
        yield return new WaitForSeconds(1);

        coll.center = new Vector3(0, 0.8f, 0);
        coll.height = 4;
        isSliding = false;

    }
}
