using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class NPCInteractable : MonoBehaviour
{
    [SerializeField] private string text;

    public string GetText()
    {
        return text;
    }
    
}
