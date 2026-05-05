using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;


public class PlayerController : MonoBehaviour
{
    private InputSystem_Actions inputActions;
    private InputAction moveAction;
    private InputAction jumpAction;
    [SerializeField] private Vector2 moveDir;
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float jumpForce = 7f;

    private Rigidbody2D rigid;

    bool CanJump()
    {
        Debug.DrawRay(transform.position, Vector2.down * 0.5f, Color.red, 2f);
        return Physics2D.Raycast(transform.position, Vector2.down, 0.2f, 1 << LayerMask.NameToLayer("Ground"));
    }

    void OnJump(InputAction.CallbackContext context)
    {
        Debug.Log("Jump");
        if (CanJump())
        {
            rigid.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }
    void Started(InputAction.CallbackContext context)
    {
        Debug.Log("Move Started");
    }

    void Performed(InputAction.CallbackContext context)
    {
        Debug.Log("Move Performed");
        moveDir = context.ReadValue<Vector2>();
        moveDir.x = Mathf.Sign(moveDir.x);
    }

    void Canceled(InputAction.CallbackContext context)
    {
        Debug.Log("Move Canceled");
        moveDir = Vector2.zero;
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Awake()
    {
        inputActions = new InputSystem_Actions();
        moveAction = inputActions.Player.Move;
        jumpAction = inputActions.Player.Jump;
    }

    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        moveAction.Enable();
        jumpAction.Enable();
        moveAction.started += Started;
        moveAction.performed += Performed;
        moveAction.canceled += Canceled;
        jumpAction.started += OnJump;
    }


    void OnDisable()
    {
        moveAction.Disable();
        jumpAction.Disable();
        moveAction.started -= Started;
        moveAction.performed -= Performed;
        moveAction.canceled -= Canceled;
        jumpAction.started -= OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += moveDir.x * Time.deltaTime * moveSpeed * Vector3.right;
    }
}
