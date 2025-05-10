using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogActivator : MonoBehaviour, IInteractable
{
    [SerializeField] private DialogueObject dialogueObject;
    private bool interactedAlready = false;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.TryGetComponent(out Player player))
        {
            player.Interactable = this;
        }
    }
    public void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out Player player))
        {
            if(player.Interactable is DialogActivator dialogActivator && dialogActivator == this)
            {
                player.Interactable = null;
            }
        }
    }
    public void Interact(Player player)
    {
        player.setFrozen(true);
        if (!interactedAlready && gameObject.CompareTag("Enemy"))
        {
            interactedAlready = true;
            player.DialogueUI.ShowDialogue(dialogueObject);

        } 
        else if(!gameObject.CompareTag("Enemy"))
        {
            player.DialogueUI.ShowDialogue(dialogueObject);
        }
    }

}
