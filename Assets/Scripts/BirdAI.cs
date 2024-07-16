using UnityEngine;

public class BirdAI : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private float obstacleDetectionRange = 10f;
    [SerializeField] private float verticalTolerance = 1f;
    [SerializeField] private GameObject vFx;
    private Rigidbody rb;
    private Transform nextPipe;
    private bool isFlapping = false;
    
    private LevelSpawn pipeSpawner; 

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        
        pipeSpawner = FindObjectOfType<LevelSpawn>(); 
        FindNextPipe();
    }

    void Update()
    {
        if (nextPipe == null || nextPipe.position.x < transform.position.x - 5f)
        {
            FindNextPipe();
        }

        if (nextPipe != null)
        {
            Vector3 targetPosition = GetGapCenter(nextPipe);

            float currentVerticalTolerance = Mathf.Clamp(verticalTolerance - Mathf.Abs(transform.position.y - targetPosition.y), 0f, verticalTolerance);

            if (!isFlapping && transform.position.y < targetPosition.y - currentVerticalTolerance)
            {
                Flap();
            }
        }

        RotateBird();
    }

    void Flap()
    {
        rb.velocity = Vector3.up * jumpForce;
        isFlapping = true;
        AudioManager.Instance.PlayJumpSound();
        Invoke("ResetFlap", 0.5f); 
    }

    void ResetFlap()
    {
        isFlapping = false;
    }

    void FindNextPipe()
    {
        float closestDistance = float.MaxValue;
        Transform closestPipe = null;

        foreach (GameObject pipe in pipeSpawner.spawnedPipes)
        {
            Debug.Log("Pipe found with name: " + pipe.name + " and children count: " + pipe.transform.childCount);
            float distance = pipe.transform.position.x - transform.position.x;
            if (distance > 0 && distance < closestDistance)
            {
                closestDistance = distance;
                closestPipe = pipe.transform;
            }
        }

        nextPipe = closestPipe;

        if (nextPipe == null)
        {
            Debug.LogError("No next pipe found");
        }
    }

    Vector3 GetGapCenter(Transform pipe)
    {
        if (pipe.childCount >= 2)
        {
            float gapYPosition = (pipe.GetChild(0).position.y + pipe.GetChild(1).position.y) / 2;
            return new Vector3(pipe.position.x, gapYPosition, pipe.position.z);
        }
        else
        {
            Debug.LogError("Pipe does not have enough children to determine gap center. Pipe name: " + pipe.name + " Children count: " + pipe.childCount);
            return new Vector3(pipe.position.x, 0, pipe.position.z);
        }
    }

    void RotateBird()
    {
        float targetAngle;
        if (isFlapping)
        {
            targetAngle = 0f;
            vFx.SetActive(true); ;
        }
        else
        {
            targetAngle = -90f;
            vFx.SetActive(false); ;
        }

        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            AudioManager.Instance.PlayDieSound();
            
            GameManager.instance.GameOver();
            AudioManager.Instance.PlayGameOverSound();
        }
    }
}
