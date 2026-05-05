using UnityEngine;

public class EmenyController : MonoBehaviour
{

    [SerializeField]
    private PlayerController player;

    [SerializeField]
    private float speed = 2f;
    [SerializeField]
    private float jumpSpeed = 7f;
    [SerializeField]
    private float jumpTrigAmount = 2f;
    private Rigidbody2D rb;

    private Vector2 unit;

    private bool jumpable = false;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other != null)
        {
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                jumpable = true;
            }
            if (other.gameObject.GetComponent<PlayerController>() != null)
            {
                Damaging();
            }
        }
    }

    public void Damaging()
    {
        Debug.Log("데미지 줌");
    }

    void Update()
    {
        unit = (player.gameObject.transform.position - transform.position).normalized;

    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(unit.x * speed, rb.linearVelocity.y);

        if ((player.gameObject.transform.position - transform.position).y > jumpTrigAmount)
        {
            if (jumpable)
            {
                rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpSpeed); 
                jumpable = false;  
            }
        }
    }
}
