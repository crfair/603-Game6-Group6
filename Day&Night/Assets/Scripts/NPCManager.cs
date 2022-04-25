using DialogueEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class NPCManager : MonoBehaviour
{
    private TMP_Text tmpText;
    private Text text;
    [SerializeField]
    private NPCConversation dialogue;
    [SerializeField]
    private NPCConversation bark;

    private bool playerNearby;
    private PlayerController playerMovement;
    private bool puzzleCompleted;

    private void Start()
    {
        text = GetComponentInChildren<Text>();
        text.enabled = false;
        playerNearby = false;
        playerMovement = GameObject.Find("PlayerCharacter").GetComponent<PlayerController>();

        tmpText = GetComponentInChildren<TMP_Text>();
    }

    private void Update()
    {
        if (playerNearby && Input.GetKeyDown(KeyCode.E))
        {
            if (!puzzleCompleted)
                ConversationManager.Instance.StartConversation(dialogue);
            else
                ConversationManager.Instance.StartConversation(bark);
            ConversationManager.OnConversationEnded += DialogueEnded;
            playerMovement.enabled = false;
            playerNearby = false;
        }
    }

    private void DialogueEnded()
    {
        playerMovement.enabled = true;
        ConversationManager.OnConversationEnded -= DialogueEnded;
        //gameObject.transform.Find("Canvas").gameObject.SetActive(false);
        //enabled = false;
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

    public void SetColor(bool color)
    {
        gameObject.GetComponent<Animator>().SetBool("PuzzleComplete", color);
    }

    public void SetPuzzleCompleted(bool completed)
    {
        puzzleCompleted = completed;
    }
}
