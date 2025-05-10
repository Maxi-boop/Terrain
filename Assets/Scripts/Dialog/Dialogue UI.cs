using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private TMP_Text textLabel;
    [SerializeField] private GameObject dialogueBox;
    private ResponseInput responseHandler;
    private TypeWriterEffect typeWriterEffect;

    public bool IsOpen { get; private set; }
    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseInput>();
        CloseDialogueBox();
    }
    public void ShowDialogue(DialogueObject dialogueObject)
    {
        IsOpen = true;
        dialogueBox.SetActive(true);
        StartCoroutine(StepThroughDialogue(dialogueObject));
        if (dialogueObject.getWrongAnswer())
        {
            GameObject player = GameObject.FindWithTag("Player");
            if (player != null) {
                Health playerHealth = player.GetComponent<Health>();
                playerHealth.removeHealth();
            }
        }
    }
    private IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
       // yield return new WaitForSeconds(2);
        for(int i = 0; i <  dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];
            yield return typeWriterEffect.Run(dialogue, textLabel);

            if (i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) break;
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Space));

        }

        if (dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses);
        }
        else CloseDialogueBox();
    }
    private void CloseDialogueBox()
    {
        IsOpen = false;
        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;

        GameObject player = GameObject.FindWithTag("Player");
        if (player != null)
        {
            Player character = player.GetComponent<Player>();
            character.setFrozen(false);
        }
    }
}
