﻿using MyBox;
using UnityEngine;
using UnityEngine.Events;

public class MoveBarrel : MonoBehaviour
{
    public Animator barrelAnimator;
    public string moveBarrelTrigger;
    public string playerTag;

    public bool playSound = false;
    [ConditionalField(nameof(playSound))]
    public SoundSourcePlayer soundSourcePlayer;

    public bool unityEventOnMoved = false;
    [ConditionalField(nameof(unityEventOnMoved))]
    public UnityEvent unityEvent;

    private bool playerInside;

    public void RequestMove()
    {
        if (!gameObject.activeSelf || !enabled)
            return;

        if (playerInside)
        {
            if (barrelAnimator != null)
            {
                barrelAnimator.SetTrigger(moveBarrelTrigger);

                if (playSound && soundSourcePlayer != null)
                    soundSourcePlayer.Play();

                if (unityEventOnMoved && unityEvent != null)
                    unityEvent.Invoke();
            } else
            {
                Debug.LogError("No Animator given!", this);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            playerInside = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == playerTag)
        {
            playerInside = false;
        }
    }
}
