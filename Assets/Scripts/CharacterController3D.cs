using UnityEngine;

public class CharacterController3D : MonoBehaviour
{    

    public float moveSpeed = 5f;
    public float jumpForce = 10f;
    public Transform punchPoint;
    public float punchRange = 0.5f;
    public LayerMask enemyLayers;
    public GameObject powerPrefab;
    public Transform powerSpawnPoint;
    public Collider platformSwitch;

    private Rigidbody rb;
    public Animator animator;
    private bool isGrounded;
    public bool isHitting =false;
    private bool isJumping = false;
    private bool isCrouching = false;

    public GameObject HitHalo;
    public GameObject HitTrail;
    public float yPlatform;
    public float yOffset =0;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        animator = GetComponentInChildren<Animator>();
        yPlatform = platformSwitch.transform.localPosition.y;
    }

    void Update()
    {
        Crouch();
        Move();
        Jump();
        Punch();
        ThrowPower();
                
    }

    void Crouch()
    {
        float crouch = Input.GetAxis("Vertical");
        isCrouching = (crouch < 0);
        animator.SetBool("Crouch", isCrouching);
        animator.SetBool("Run", false);

    }
    void Move()
    {
        if(!isCrouching && (!isHitting || isJumping)) { 
            float moveInput = Input.GetAxis("Horizontal");
            Vector3 move = new Vector3(moveInput, 0, 0) * moveSpeed * Time.deltaTime;
            transform.Translate(move, Space.World);
            bool moving = (moveInput != 0);
            if (moving)
            {
                transform.forward = move;
            }

            animator.SetBool("Run", moving);
        }
        else if (isHitting)
        {
            animator.SetBool("Run", false);
        }
    }

    void Jump()
    {
        RaycastHit hit;
        isGrounded = Physics.Raycast(transform.position, Vector3.down, out hit ,2.1f,3) ;
        Debug.DrawRay(transform.position, Vector3.down,Color.red);
        if (!isHitting && Input.GetButtonDown("Jump") && isGrounded && !animator.GetBool("Jump"))
        {
            if (!isCrouching)
            {
                rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
                animator.SetBool("Jump", true);
                isJumping = true;
                MovePlatformSwitch(0);
            }
            else if(hit.collider != null && hit.collider.gameObject.tag == "Plataforma")
            {
                MovePlatformSwitch(yOffset);
            }
            else 
            { 
                MovePlatformSwitch(0);
            }
        }
    }

    private void MovePlatformSwitch(float Offset)
    {
        platformSwitch.transform.localPosition = new Vector3(platformSwitch.transform.localPosition.x, yPlatform - Offset, platformSwitch.transform.localPosition.z);
    }

    void Punch()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            isHitting = true;
            animator.SetBool("Hit",true);            
            HitHalo.SetActive(true);       
            HitTrail.SetActive(true);
        }
    }

    void ThrowPower()
    {
        if (Input.GetButtonDown("Fire2"))
        {
      //      animator.SetTrigger("ThrowPower");
            Instantiate(powerPrefab, powerSpawnPoint.position, powerSpawnPoint.rotation);
        }
    }

    public void HitOver()
    {
        isHitting=false;
        HitHalo.SetActive(false);
        HitTrail.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        if (punchPoint == null)
            return;

        Gizmos.DrawWireSphere(punchPoint.position, punchRange);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Puntos")
        {
                        
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Floor" || collision.gameObject.tag == "Plataforma")
        {
            animator.SetBool("Jump", false);
            isJumping = false;
        }
    }
}
