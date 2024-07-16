using UnityEngine;

public class Bird : MonoBehaviour
{
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float rotateSpeed = 5f;
    [SerializeField] private GameObject vFx;
    private Rigidbody rb;
    private bool isFlapping = false;
    private AudioManager audioManager;
   

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = FindObjectOfType<AudioManager>();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            Flap();
        }

        RotateBird();
    }

    void Flap()
    {
        rb.velocity = Vector3.up * jumpForce;
        isFlapping = true;
        audioManager.PlayJumpSound(); 
        Invoke("ResetFlap", 0.5f); 
    }

    void ResetFlap()
    {
        isFlapping = false;
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
            vFx.SetActive(false);
        }

        Quaternion targetRotation = Quaternion.AngleAxis(targetAngle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, rotateSpeed * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Obstacle"))
        {
            audioManager.PlayDieSound();
            GameManager.instance.GameOver();
        }
    }
}
