using UnityEngine;

//the collectible is the things let player collect
public class Collectible : MonoBehaviour
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
            ScoreManager.instance.AddScore();
            Destroy(gameObject);
        }

    }

    private void Start()
    {
        if (target == null)
        {
            target = GameObject.Find("Player");
        }
    }


    public virtual void BulletDied(Collider other)
    {
        if (other.name == "Player")
        {
            Destroy(gameObject);
        }
    }

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
        //this is the code of let collectible run away from player
        Vector3 direction = transform.position - target.transform.position;
        direction.y = 0;

        if (direction.magnitude < 200f)
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