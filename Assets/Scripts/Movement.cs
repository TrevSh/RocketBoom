using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Movement : MonoBehaviour
{
    [SerializeField] float thrusterPower;
    [SerializeField] float rotatePower;
    [SerializeField] AudioClip engineThrusters;
    [SerializeField] ParticleSystem mainThrusterParticles;
    [SerializeField] ParticleSystem rightThrusterParticles;
    [SerializeField] ParticleSystem leftThrusterParticles;


    Rigidbody rb;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            StartThrusting();
        }
        else if (audioSource.isPlaying)
        {
            StopThrusting();
        }


    }
    private void StartThrusting()
    {
        //Debug.Log("Thrusters Acrivated");
                 rb.AddRelativeForce(Vector3.up * thrusterPower * Time.deltaTime);

        if (!audioSource.isPlaying || !mainThrusterParticles)
        {
                mainThrusterParticles.Play();
                audioSource.PlayOneShot(engineThrusters);        
        }

            
    }

    private void StopThrusting()
    {
        mainThrusterParticles.Stop();
        audioSource.Stop();
    }


    void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            RotateLeft();
        }

        else if (Input.GetKey(KeyCode.D))
        {
            RotateRight();
        }
        else
        {
            StopRotating();
        }
    }
    private void RotateLeft()
    {
        ApplyRotation(rotatePower);
        if (!rightThrusterParticles.isPlaying)
        {
            rightThrusterParticles.Play();
        }
    }

    private void RotateRight()
    {
        ApplyRotation(-rotatePower);

        if (!leftThrusterParticles.isPlaying)
        {
            leftThrusterParticles.Play();
        }
    }

    private void StopRotating()
    {
        rightThrusterParticles.Stop();
        leftThrusterParticles.Stop();
    }


    void ApplyRotation(float rotationThisFrame)
    {
        rb.freezeRotation = true; //Freezing rotation so we can manually rotate
        transform.Rotate(Vector3.forward * rotationThisFrame * Time.deltaTime);
        rb.freezeRotation = false;//Unfreezing so physics can take over
       
    }

}
