using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class PlayerInteractUI : MonoBehaviour
{
    [SerializeField] private GameObject containerGameobject;
    [SerializeField] private Cont playerController;
    [SerializeField] private TextMeshProUGUI interactTextMeshProUGUI;
    [SerializeField] private GameObject mainBtn;

    private void Update()
    {
        if (playerController.GetInteractableObject() != null && !playerController.isTalking)
        {
            Show(playerController.GetInteractableObject());
        }
        else if(playerController.GetInteractableObject() == null || playerController.isTalking)
        {
            Hide();
        }

        if (playerController.isObtaining)
        {
            mainBtn.SetActive(true);
        }
    }

    private void Show(NPCInteractable npcInteractable)
    {
        containerGameobject.SetActive(true);
        interactTextMeshProUGUI.text = npcInteractable.GetText();
    }

    private void Hide()
    {
        containerGameobject.SetActive(false);
    }
    
    public void LoadMainScene()
    {
        SceneManager.LoadScene(1);
    }
}
