using UnityEngine;
using UnityEngine.InputSystem;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public string playerName = "쭌서";
    public int hp = 100;
    public float moveSpeed = 5f;
    public Transform visual;
  private Vector2 moveInput;
    public float jumpPower = 8f;
    public Vector3 startPosition;
    private Rigidbody2D rb;
    private bool isGrounded = false;
    public float fallLimit = -10f;
    public int lives = 3;
    private bool isGameOver = false;
    public Vector2 airScale = new Vector2(0.8f, 1.2f);
    private float facing = 1f;
    public GameObject gameOverPanel;
    public TMP_Text livesText;
    public string nextSceneName = "Stage2";
    void Start()

    {
        rb = GetComponent<Rigidbody2D>();
        transform.position = startPosition;
        livesText.text = "LIFE " + lives;
    }
    void OnMove(InputValue value)
    {
        moveInput = value.Get<Vector2>();
        if (moveInput.x > 0) { facing = 1f; }
        else if (moveInput.x < 0) { facing = -1f; }
    }

    void OnJump(InputValue value)
    {
        if(value.isPressed && isGrounded && !isGameOver)
        {
            Debug.Log("점프");
            rb.linearVelocity = new Vector2(rb.linearVelocityX, jumpPower);
        }
    }
    
    void Update()
  {
        Debug.Log("Update");
        if (!isGameOver)
        {
            transform.Translate(Vector3.right * moveInput.x * moveSpeed * Time.deltaTime);
            if (transform.position.y < fallLimit)
            {
                transform.position = startPosition;
                rb.linearVelocity = Vector2.zero;
                Debug.Log("낙사");
                LoseLife();

            }
        }
        if(isGrounded)
        {
            visual.localScale = new Vector3(facing, 1f, 1f);
        }
        else
        {
            visual.localScale = new Vector3(facing * airScale.x, airScale.y, 1f);
        }
}
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
            Debug.Log("착지");
        }
    }
    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = false;
            Debug.Log("공중");
        }
    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Goal"))
        {
            SceneManager.LoadScene(nextSceneName);
        }
        else if (other.CompareTag("Trap"))
        {
            LoseLife();
        }
    }
    void LoseLife()
    {
        lives -= 1;
        transform.position = startPosition;
        rb.linearVelocity = Vector2.zero;
        livesText.text = "LIFE " + lives;
        if (lives <= 0)
        {
            isGameOver = true;
            gameOverPanel.SetActive(true);
        }
    }


}
