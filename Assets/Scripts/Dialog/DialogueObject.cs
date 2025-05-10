using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Dialogue/DialogueObject")] 
public class DialogueObject : ScriptableObject
{
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;
    [SerializeField] private bool wrongAnswer = false;

    public bool HasResponses => Responses != null && Responses.Length > 0;
    public string[] Dialogue => dialogue; //prevents external rewriting forces
    public Response[] Responses => responses;

    public bool getWrongAnswer()
    {
        return wrongAnswer;
    }
    
}
