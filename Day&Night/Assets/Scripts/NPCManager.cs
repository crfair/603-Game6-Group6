using DialogueEditor;
using UnityEngine;
using UnityEngine.UI;

public class NPCManager : MonoBehaviour
{
    private Text text;
    [SerializeField]
    private NPCConversation dialogue;

    private bool playerNearby;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        text.enabled = false;
        playerNearby = false;
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            ConversationManager.Instance.StartConversation(dialogue);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            text.enabled = true;
            playerNearby = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        text.enabled = false;
        playerNearby = false;
    }
}
