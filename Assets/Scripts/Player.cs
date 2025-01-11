using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private CharacterController controller;
    private Animator animator;
    private AudioSource audioSource;
    private Vector3 playerVelocity;
    private bool groundedPlayer;
    [SerializeField]
    private float _speed = 2.0f;
    [SerializeField]
    private float _runSpeed = 4.0f;
    private float jumpHeight = 1.0f;
    private float _gravity = -9.81f;
    [SerializeField]
    private AudioClip walkClip;
    [SerializeField]
    private AudioClip runClip;

    private void Start()
    {
        controller = gameObject.AddComponent<CharacterController>();
        animator = GetComponent<Animator>();
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        audioSource.volume = 1.0f;
        audioSource.spatialBlend = 0f;
    }

    void Update()
    {
        groundedPlayer = controller.isGrounded;
        if (groundedPlayer && playerVelocity.y < 0)
        {
            playerVelocity.y = 0f;
        }

        float currentSpeed = Input.GetKey(KeyCode.LeftShift) ? _runSpeed : _speed;

        Vector3 move = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
        move = transform.TransformDirection(move);

        if (move.magnitude > 1)
        {
            move.Normalize();
        }

        controller.Move(move * Time.deltaTime * currentSpeed);

        if (Input.GetButtonDown("Jump") && groundedPlayer)
        {
            playerVelocity.y += Mathf.Sqrt(jumpHeight * -2.0f * _gravity);
        }

        playerVelocity.y += _gravity * Time.deltaTime;
        controller.Move(playerVelocity * Time.deltaTime);

        animator.SetFloat("Speed", move.magnitude * currentSpeed);
        animator.SetBool("IsRunning", Input.GetKey(KeyCode.LeftShift));

        HandleFootstepSounds(move.magnitude, currentSpeed);
    }

    private void HandleFootstepSounds(float movementMagnitude, float currentSpeed)
    {
        if (movementMagnitude > 0.1f && groundedPlayer)
        {
            AudioClip currentClip = Input.GetKey(KeyCode.LeftShift) ? runClip : walkClip;

            if (audioSource.clip != currentClip)
            {
                audioSource.clip = currentClip;
                audioSource.Play();
            }
        }
        else
        {
            if (audioSource.isPlaying)
            {
                audioSource.Stop();
            }
        }
    }
}
