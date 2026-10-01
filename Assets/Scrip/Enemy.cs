using UnityEngine;

//the enemy is the things that attack the player
public class Enemy : MonoBehaviour
{
    public GameObject target;
    public float speed = 300f;

    [Header("Avoid Walls Distance Check")]
    public float detectDistance = 5f;
    public float wallAvoidOffset = 0.5f;
    public float magnitude = 200f;


    private void OnTriggerEnter(Collider other)
    {
        if (other.name == "Player")
        {
            // touch player then game over
            if (GameManager.instance != null)
            {
                GameManager.instance.GameOver();
            }
        }
    }


    private void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Player");
        }
    }


    public virtual void BeCollect(Collider other)
    {
        if (other.name == "Player")
        {
            Destroy(gameObject);
        }
    }


    // This function checks for walls in the desired direction and adjusts the movement direction to avoid them
    Vector3 AvoidWalls(Vector3 desiredDir)
    {
        RaycastHit[] hits = Physics.RaycastAll(transform.position, desiredDir, detectDistance);

        foreach (RaycastHit hit in hits)
        {
            if (hit.collider.name.Contains("Wall"))
            {
                Vector3 slideDir = Vector3.ProjectOnPlane(desiredDir, hit.normal).normalized;

                if (slideDir.magnitude < 0.1f)
                {
                    slideDir = Vector3.Cross(hit.normal, Vector3.up).normalized;
                }

                Vector3 finalDir = (slideDir + hit.normal * wallAvoidOffset).normalized;
                return finalDir;
            }
        }
        return desiredDir;
    }


    void Update()
    {
        if (target == null) return;

        Vector3 direction = target.transform.position - transform.position;
        direction.y = 0;

        if (direction.magnitude < magnitude)
        {
            speed = 280;
            Vector3 desiredDir = direction.normalized;

            Vector3 finalDir = AvoidWalls(desiredDir);

            Vector3 moveVector = finalDir * speed * Time.deltaTime;
            transform.Translate(moveVector, Space.World);
        }
        else
        {
            speed = 0;
            return;
        }
    }
}