using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    Rigidbody2D rgbd2D;
    Animator animator;
    CircleCollider2D coll;
    GUIPlayerLife UIPlayerLife;
    ReturnToMenu returnToMenu;
    public Transform shootPosition;
    public GameObject bulletPrefab;
    public LayerMask layerMask;
    int life = 5;
    public bool grounded = false;
    float speed = 1.5f;
    float vSpeed = 0f;
    float hSpeed = 0f;
    float waterSpeed = 1f;
    float hurtTimer = 0f;
    public float jumpForce = 1;
    bool jumped = false;
    public float gravityForce = 1f;
    bool duck = false;
    bool isShooting = false;
    const float SHOOT_TIME = 0.5f;
    float canShoot = 0f;

    void Awake()
    {
        rgbd2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        coll = GetComponent<CircleCollider2D>();
        GameObject.FindGameObjectWithTag("MainCamera").GetComponent<CameraMovement>().playerPos = transform;
    }

    private void Start()
    {
        UIPlayerLife = GameObject.Find("PlayerLife").GetComponent<GUIPlayerLife>();
        returnToMenu = GameObject.Find("MainObject").GetComponent<ReturnToMenu>();
    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            grounded = IsGrounded();
            ManageInputs();
            SetAnimationStates();
            if (hurtTimer > 0f)
            {
                hurtTimer -= Time.deltaTime;
            }
            else
            {
                hurtTimer = 0f;
            }
            if (canShoot > 0)
            {
                isShooting = false;
                canShoot -= Time.deltaTime;
            }
            else
            {
                canShoot = 0f;
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            waterSpeed = 0.5f;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Water")
        {
            waterSpeed = 1f;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Water") == false)
        {
            if (vSpeed > 0)
            {
                vSpeed = 0;
            }
        }
        if (collision.gameObject.CompareTag("Wall"))
        {
            GotHit(5);
        }
    }

    private void FixedUpdate()
    {
        PlayerMove();
    }
    void PlayerMove()
    {
        if (grounded == false)// || jumped == true)
        {
            vSpeed -= gravityForce;
        }
        else
        {
            vSpeed = vSpeed < 0 ? 0f : vSpeed;
        }
        if (jumped == true)
        {
            vSpeed = jumpForce;
            jumped = false;
        }


        float hVelocityModifier = (grounded == true) ? 1f : 0.75f;
        if (duck == true)
            hSpeed = 0;
        rgbd2D.MovePosition(rgbd2D.position + new Vector2(hSpeed * speed * hVelocityModifier * waterSpeed * Time.deltaTime, vSpeed));
    }
    private void ManageInputs()
    {
        hSpeed = Input.GetAxis("Horizontal");
        if (Input.GetKey(KeyCode.S))
        {
            if (grounded == true)
                duck = true;
            else
                duck = false;
        }
        if (Input.GetKeyUp(KeyCode.S))
        {
            duck = false;
        }
        if (Input.GetButtonDown("Jump") && grounded == true)
        {
            jumped = true;
            duck = false;
        }
        if (Input.GetButtonDown("Fire1") && grounded == true)
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        if (canShoot <= 0)
        {
            isShooting = true;
            Transform bullet = Instantiate(bulletPrefab).transform;
            bullet.position = shootPosition.position;
            if (duck == true)
                bullet.position -= new Vector3(0f, 0.12f, 0f);
            bullet.localScale = transform.localScale;
            canShoot = SHOOT_TIME;
        }
    }

    public void GotHit(int hp)
    {
        if (hurtTimer <= 0)
        {
            life -= hp;
            if (life <= 0)
            {
                returnToMenu.GameOver();
            }
            UIPlayerLife.UpdateLife(life);
            //animator.SetTrigger("Hurt");
            hurtTimer = 1f;
        }
    }

    private void SetAnimationStates()
    {
        animator.SetBool("Grounded", grounded);
        animator.SetFloat("vSpeed", vSpeed);
        animator.SetBool("Duck", duck);
        animator.SetBool("Shooting", isShooting);
        if (hSpeed < 0f)
        {
            transform.localScale = new Vector3(-1, 1f, 1f);
            animator.SetBool("Run", true);
        }
        else
        if (hSpeed > 0f)
        {
            transform.localScale = new Vector3(1, 1f, 1f);
            animator.SetBool("Run", true);
        }
        else
        {
            animator.SetBool("Run", false);
        }
    }

    private bool IsGrounded()
    {
        Vector2 from = (Vector2)transform.position + coll.offset;
        float distance = Mathf.Max(System.Math.Abs(vSpeed), coll.radius) + gravityForce * 2;
        RaycastHit2D hit = Physics2D.Raycast(from, Vector2.down, distance);
        if (hit.collider != null && hit.collider.tag != "Water")
        {
            return true;
        }
        return coll.IsTouchingLayers(layerMask);
    }

    private bool DownRaycast()
    {
        Vector2 left, right, center;
        left = coll.bounds.min;
        right = new Vector2(coll.bounds.max.x, left.y);
        center = new Vector2(transform.position.x, left.y);
        float downSpeed = Mathf.Abs(vSpeed);

        RaycastHit2D hit = Physics2D.Raycast(left, Vector2.down, downSpeed);
        if (hit.collider != null)
            return true;
        hit = Physics2D.Raycast(right, Vector2.down, downSpeed);
        if (hit.collider != null)
            return true;
        hit = Physics2D.Raycast(center, Vector2.down, downSpeed);
        if (hit.collider != null)
            return true;
        return false;
    }

    //private bool RayIntersects()
    //{
    //    Vector2 from = (Vector2)transform.position + coll.offset * 1.1f;
    //    float distance = Mathf.Max(System.Math.Abs(vSpeed), coll.radius);
    //    RaycastHit2D hit = Physics2D.Raycast(from, Vector2.down, distance);
    //    if (hit.collider != null)
    //        return true;
    //    return false;
    //}
}
