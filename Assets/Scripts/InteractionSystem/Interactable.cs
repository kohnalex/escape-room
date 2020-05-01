﻿using MyBox;
using UnityEngine;

public enum InteractionBehaviour 
{
    ConsoleLog,
    Rotate,
    Empty,
    Custom,
}

public enum DetectionBehaviour
{
    ConsoleLog,
    Highlight,
    Empty,
    Custom,
}

public class Interactable : MonoBehaviour, IInteractable, IDetectable
{

    #region Variables
    
    /* IBase */
    [Header("Properties")]
    // public float holdDuration;
    public bool holdToInteract;
    public bool multipleUse;    // subject to removal
    public bool isInteractable;
    public float HoldDuration { get; set; }
    public bool HoldToInteract => holdToInteract;
    public bool MultipleUse => multipleUse;
    public bool IsInteractable => isInteractable;

    [Header("Choose Behaviour")]
    public DetectionBehaviour detectionOption;
    [ConditionalField(nameof(detectionOption), false, DetectionBehaviour.Custom)]
    public BaseDetection detectionBehaviour;
    public InteractionBehaviour interactionOption;
    [ConditionalField(nameof(interactionOption), false, InteractionBehaviour.Custom)]
    public BaseInteraction interactionBehaviour;
    #endregion

    #region Builtin
    
    protected void Start()
    {
        HandleInteractionStrategy();
        HandleDetectionStrategy();
    }
    
    #endregion

    private void HandleInteractionStrategy()
    {
        /* Subject to change
        Component c = gameObject.GetComponent<IInteractable>() as Component;
        if (c != null) Destroy(c);
        */ 
        
        switch (interactionOption)
        {
            case InteractionBehaviour.Custom:
                break;
            case InteractionBehaviour.ConsoleLog:
                interactionBehaviour = gameObject.AddComponent<ConsoleInteraction>();
                break;
            case InteractionBehaviour.Rotate:
                interactionBehaviour = gameObject.AddComponent<RotateInteraction>();
                break;
            case InteractionBehaviour.Empty:
                interactionBehaviour = gameObject.AddComponent<EmptyInteraction>();
                break;
            default:
                interactionBehaviour = gameObject.AddComponent<EmptyInteraction>();
                break;
        }    
    }

    private void HandleDetectionStrategy()
    {
        /* Subjet to change
        Component c = gameObject.GetComponent<IDetectable>() as Component;
        if (c != null) Destroy(c);
        */

        switch (detectionOption)
        {
            case DetectionBehaviour.Custom:
                break;
            case DetectionBehaviour.ConsoleLog:
                detectionBehaviour = gameObject.AddComponent<ConsoleDetection>();
                break;
            case DetectionBehaviour.Highlight:
                detectionBehaviour = gameObject.AddComponent<HighlightDetection>();
                break;
            case DetectionBehaviour.Empty:
                detectionBehaviour = gameObject.AddComponent<EmptyDetection>();
                break;
            default:
                detectionBehaviour = gameObject.AddComponent<EmptyDetection>();
                break;
        }
    }

    #region Strategy Implementation

    /* Interaction */
    public void OnInteraction()
    {
        interactionBehaviour.OnInteraction();
    }

    /* Detection */
    public void OnDetectionEnter()
    {
        detectionBehaviour.OnDetectionEnter();
    }

    public void OnDetectionExit()
    {
        detectionBehaviour.OnDetectionExit();
    }

    #endregion
}
