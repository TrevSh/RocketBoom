using UnityEngine;

public class Fuel : MonoBehaviour
{
    AudioSource audioSource;
    [SerializeField] float spinY = 1;
    [SerializeField] float spinZ = 1;
    [SerializeField] float spinX = 0;


    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    private void Update()
    {
        transform.Rotate(spinX, spinY, spinZ);
    }

    private void GetFuel()
    {
        Debug.Log("Detected");
        audioSource.Play();
    }
}
