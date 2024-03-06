using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialog : MonoBehaviour
{
    
    public float lookRadius = 10f;
    public string dialog;
    public TextMeshProUGUI dialogText;
    public GameObject dialogContainer;

    // Start is called before the first frame update
    void Start()
    {
        dialogText.text = dialog;
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText(PlayerCollision.inBound);
    }

    public void DisplayText(bool inBound)
    {
        if (inBound)
            dialogContainer.SetActive(true);
        else
            dialogContainer.SetActive(false);
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
