using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCDialog : MonoBehaviour
{
    public float lookRadius = 10f;
    public string dialog;
    public bool playerInRange = false;
    public TextMeshProUGUI dialogText;
    public GameObject dialogBox;

    void Update()
    {
        if (Input.GetButtonDown("Interact") && playerInRange){
            if (dialogBox.activeInHierarchy)
            {
                PlayerMovement.currentState = PlayerState.walk;
                dialogBox.SetActive(false);
            } else {
                dialogBox.SetActive(true);
                dialogText.text = dialog;
                print("true!!");
                PlayerMovement.currentState = PlayerState.interact;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
            playerInRange = false;
    }

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, lookRadius);
    }
}
