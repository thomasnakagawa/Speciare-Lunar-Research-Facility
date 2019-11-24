using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class FootstepPlayer : MonoBehaviour
{
    private AudioSource audioSource;
    private Animator animator;

    private NavMeshAgent agent;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        animator = GetComponent<Animator>();
        AddEvent(0.7f);
        AddEvent(1.6f);

        agent = GetComponentInParent<NavMeshAgent>();
    }

    private void AddEvent(float time)
    {
        AnimationEvent animationEvent = new AnimationEvent();
        animationEvent.functionName = "FootstepSound";
        animationEvent.time = time;

        AnimationClip clip = animator.runtimeAnimatorController.animationClips[1];
        clip.AddEvent(animationEvent);
    }

    public void FootstepSound()
    {
        audioSource.volume = agent.velocity.magnitude / 6f;
        audioSource.pitch = Random.Range(0.87f, 0.93f);
        audioSource.Play();
    }
}
