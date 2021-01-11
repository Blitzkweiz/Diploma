using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public Text nameText;
    public Text dialogueText;

    public Animator animator;

    public GameObject player;

    private Queue<string> sentences;
    // Start is called before the first frame update
    void Start()
    {
        sentences = new Queue<string>();
    }

    public void StartDialogue (Dialogue dialogue)
    {
        GameEvents.current.theBattleBegins = false;

        Cursor.lockState = CursorLockMode.Confined;
        Cursor.visible = true;

        player.GetComponent<PlayerController>().topAnimator.enabled = false;
        player.GetComponent<PlayerController>().bottomAnimator.enabled = false;
        player.GetComponent<PlayerController>().enabled = false;
        player.GetComponent<Rigidbody2D>().velocity = new Vector3(0.0f, 0.0f, 0.0f);
        

        animator.SetBool("IsOpen", true);

        nameText.text = dialogue.name;

        sentences.Clear();

        foreach(string sentence in dialogue.sentences)
        {
            sentences.Enqueue(sentence);
        }

        DisplayNextSentence();
    }

    public void DisplayNextSentence()
    {
        if(sentences.Count == 0)
        {
            EndDialogue();
            return;
        }
        string sentence = sentences.Dequeue();
        StopAllCoroutines();
        StartCoroutine(TypeSentence(sentence));
    }

    private IEnumerator TypeSentence(string sentence)
    {
        dialogueText.text = "";
        foreach(char letter in sentence.ToCharArray())
        {
            dialogueText.text += letter;
            yield return null;
        }
    }

    private void EndDialogue()
    {
        animator.SetBool("IsOpen", false);

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        player.GetComponent<PlayerController>().topAnimator.enabled = true;
        player.GetComponent<PlayerController>().bottomAnimator.enabled = true;
        player.GetComponent<PlayerController>().enabled = true;

        GameEvents.current.theBattleBegins = true;

        if(nameText.text == "Dazut'Lek")
        {
            player.GetComponent<PlayerController>().WinGame();
        }
    }
}
