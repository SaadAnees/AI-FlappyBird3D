using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class Pipe : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            GameManager.instance.AddScore();
            AudioManager.Instance.PlayCollectSound();
            //Debug.Log("Trigger score ");
        }
    }
}
