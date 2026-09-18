using UnityEngine;
using UnityEngine.InputSystem;
 
public class PlayerController : MonoBehaviour
{
    public string playerName = "쭌서";
    public int hp = 100;
    public float moveSpeed = 5f;
    public Transform visual;
  private Vector2 moveInput;
    public float jumpPower = 8f;
    private Rigidbody2D rb;

    public Vector3 startPosition;
    void Start()

    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPosition;
    }
 
  void OnMove(InputValue value)
  {
    moveInput = value.Get<Vector2>();
        if (moveInput.x > 0)
        {
            visual.localScale = new Vector3(1, 1, 1);
        }
        else if (moveInput.x < 0)
        {
            visual.localScale = new Vector3(-1, 1, 1);
        }
  }
    void OnJump(InputValue value)
    {
        if (value.isPressed)
        {
            Debug.Log("점프!");
            rb.linearVelocity = new Vector2(rb.linearVelocity.x, jumpPower);
        }
    }

    void Update()
  {
        //Debug.Log("Update");
        transform.Translate(Vector3.right * moveInput.x * moveSpeed * Time.deltaTime);
  }
}
