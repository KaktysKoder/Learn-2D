using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CapsuleCollider2D))]
public class PlayerController : MonoBehaviour
{
    [Header("Set in Inspector")]
    public Transform feetPosition;  // Позиция ног игрока.
    public LayerMask whatIsGround;  // Что мы считаем за землю?
    public float checkRadius;       // Радиус насколько близко игрок должен находиться к земле.

    private Rigidbody2D rb2D;

    private float moveSpeed = 5.0f;
    private float jumpForce = 8.0f;
    private float moveInput;

    private bool isGrounded;        // Проверка на заземления игрока.
    private bool facingRight;       // Лицом вправо.


    private void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Movement
        {
            moveInput = Input.GetAxis("Horizontal");

            rb2D.velocity = new Vector2(moveInput * moveSpeed, rb2D.velocity.y);

            TurnLeft(moveInput);
            TurnRight(moveInput);
        }

        // Set value for Jump
        {
            isGrounded = Physics2D.OverlapCircle(feetPosition.position, checkRadius, whatIsGround);
        }
    }

    private void Update()
    {
        Jump(isGrounded);
    }

    /// <summary>
    /// Поворот игрока влево.
    /// </summary>
    /// <param name="left"></param>
    private void TurnLeft(float left)
    {
        if (facingRight == false && moveInput > 0)      // Смотрим влево.
        {
            Flip();
        }
    }

    /// <summary>
    /// Повотрот игрока вправо.
    /// </summary>
    /// <param name="right"></param>
    private void TurnRight(float right)
    {
        if (facingRight == true && moveInput < 0)  // Смотрим вправо.
        {
            Flip();
        }
    }

    /// <summary>
    /// Устанавливает Scale данного объекта в отрицательное значение. (Поворот игрока).
    /// </summary>
    private void Flip()
    {
        facingRight = !facingRight;

        Vector3 scaler = transform.localScale;
        scaler.x *= -1;

        transform.localScale = scaler;
    }

    /// <summary>
    /// Прыжок игрока.
    /// </summary>
    /// <param name="isGrounded">Проверка на заземление игрока.</param>
    private void Jump(bool isGrounded)
    {
        // Если мы заземлились и нажали пробел, прыгаем.
        if (isGrounded == true && Input.GetKeyDown(KeyCode.Space))
        {
            rb2D.velocity = Vector2.up * jumpForce;
        }
    }
}