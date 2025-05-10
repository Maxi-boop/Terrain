using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    // Start is called before the first frame update
    private Rigidbody2D rb;
    private float MoveSpeed = 5f;


    private float moveInput;
    public float jumpForce;
    private bool isGrounded;
    public Transform feetPos;
    public float checkRadius;
    public LayerMask whatIsGround;

    private bool isJumping;
    private float jumpTimeCounter;
    private float jumpTime;

    private bool frozen;
    private bool enemy = false;


    [SerializeField] private DialogueUI dialogueUI;
    public DialogueUI DialogueUI => dialogueUI;
    private SpriteRenderer spriteRenderer;
    public IInteractable Interactable { get;  set; }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>(); 
         spriteRenderer= GetComponent<SpriteRenderer>();
        rb.freezeRotation = true;
        frozen = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!frozen)
        {
            moveInput = Input.GetAxisRaw("Horizontal");
            rb.velocity = new Vector2(moveInput * MoveSpeed, rb.velocity.y);

            if (Input.GetKeyDown(KeyCode.D))
            {
                rb.transform.localScale = new Vector3(-1, 1, 1);
            }
        }

    }
   
    private void Update()
    {
        if (!frozen) {
            if (Input.GetKeyDown(KeyCode.A))
            {
                rb.transform.localScale = new Vector3(1, 1, 1);
            }
            if (Input.GetKeyDown(KeyCode.E) && !enemy) Interactable?.Interact(this);
            isGrounded = Physics2D.OverlapCircle(feetPos.position, checkRadius, whatIsGround);
            if (isGrounded && Input.GetKeyDown(KeyCode.Space))
            {
                isJumping = true;
                jumpTimeCounter = jumpTime;
                rb.velocity = Vector2.up * jumpForce;
                AudioSource.PlayClipAtPoint(jumpSFX, transform.position, 1f);
            }
            if (Input.GetKey(KeyCode.Space) && isJumping == true)
            {
                if (jumpTimeCounter > 0)
                {
                    rb.velocity = Vector2.up * jumpForce;
                    jumpTimeCounter -= Time.deltaTime;
                }
                else
                {
                    isJumping = false;
                }
            }

            if (Input.GetKeyUp(KeyCode.Space))
            {
                isJumping = false;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Enemy"))
        {
            enemy = true;
            Interactable?.Interact(this);
        } else
        {
            enemy = false;
        }
    }
    public void setFrozen(bool value)
    {
        frozen = value;
    }
}
