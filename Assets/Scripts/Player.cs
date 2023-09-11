using UnityEngine;

public class Player : MonoBehaviour
{
    public float gravity = 9.81f * 2f;
    public float jumpForce = 8f;

    private CharacterController character;
    private Vector3 direction;

    private void Awake()
    {
        character = GetComponent<CharacterController>();
    }

    private void OnEnable()
    {
        direction = Vector3.zero;
    }

    private void Update()
    {
        direction += Vector3.down * gravity * Time.deltaTime;

        if (character.isGrounded)
        {
            direction = Vector3.down;

            if (Input.GetMouseButtonDown(0))
            {
                direction = Vector3.up * jumpForce;
                AudioManager.instance.Play("Jump");
            }

            if (Input.touchCount > 0)
            {
                Touch touch = Input.GetTouch(0);
                if (touch.phase == TouchPhase.Began)
                {
                    direction = Vector3.up * jumpForce;
                    AudioManager.instance.Play("Jump");
                }
            }
        }

        character.Move(direction * Time.deltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            GameManager.Instance.GameOver();
            AudioManager.instance.Play("Death");
        }
    }
}
