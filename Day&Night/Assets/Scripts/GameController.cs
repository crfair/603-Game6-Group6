using DialogueEditor;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    [SerializeField]
    private NPCConversation intro;
    [SerializeField]
    private PlayerController player;

    // Start is called before the first frame update
    void Start()
    {
        player.enabled = false;
        ConversationManager.Instance.StartConversation(intro);
        ConversationManager.OnConversationEnded += IntroEnd;
    }

    
    private void IntroEnd()
    {
        player.enabled = true;
        ConversationManager.OnConversationEnded -= IntroEnd;
    }
}
