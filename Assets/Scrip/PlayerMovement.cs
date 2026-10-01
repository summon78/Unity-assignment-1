using UnityEngine;
using UnityEngine.InputSystem;
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    private Rigidbody rb;
    public Collider[] blockingWalls;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 move = Vector3.zero;
        if (Input.GetKey(KeyCode.W))
        {
            move += Vector3.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            move += Vector3.back;
        }
        if (Input.GetKey(KeyCode.A))
        {
            move += Vector3.left;
        }
        if (Input.GetKey(KeyCode.D))
        {
            move += Vector3.right;
        }
        
        if (move.magnitude > 1f) move.Normalize();

        Vector3 targetPosition = rb.position + move * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(targetPosition);

        float moveDistance = moveSpeed * Time.fixedDeltaTime;
        Vector3 moveDirection = move.normalized;

        RaycastHit hit;
        if (Physics.Raycast(transform.position, moveDirection, out hit, moveDistance))
        {
            bool isBlockingWall = false;
            foreach (Collider wall in blockingWalls)
            {
                if (wall == hit.collider)
                {
                    isBlockingWall = true;
                    break;
                }
            }

            if (isBlockingWall)
            {
                rb.MovePosition(transform.position + moveDirection * (hit.distance - 0.01f));
                return;
            }
        }

        rb.MovePosition(transform.position + moveDirection * moveDistance);
    }
    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("wall：" + collision.gameObject.name);
    }
}
