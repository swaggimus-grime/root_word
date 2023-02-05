using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements;

public class GameMode : MonoBehaviour
{
    public List<string> prompts;
    public List<Card> roots;
    public Transform discardPos;
    public List<Card> discardPile;
    public List<Card> words;
    public Transform[] hand;
    public Card root;
    public Card prefix;
    public Card suffix;
    public bool prefixSelected;
    public bool suffixSelected;
    public bool[] availableSlots;
    public int score;
    public TextMeshPro scoreBoard;
    public TextMeshPro promptBoard;
    private string currentPrompt;

    public Transform rootPos;
    public Transform prePos;
    public Transform suffPos;

    public void Start()
    {
        rootPos = root.transform;
        prePos = prefix.transform;
        suffPos = suffix.transform;
        NewTurn();
    }
    public void NewTurn()
    {
        if (prompts.Count >= 1)
        {
            int promptIdx = Random.Range(0, prompts.Count - 1);
            currentPrompt = prompts[promptIdx];
            prompts.Remove(currentPrompt);
            promptBoard.text = currentPrompt;

            root = roots[Random.Range(0, roots.Count - 1)];
            root.transform.position = rootPos.position;
            root.gameObject.SetActive(true);
            roots.Remove(root);

            for (int i = 0; i < availableSlots.Length; i++)
            {
                if (words.Count == 0)
                    return;
                if (availableSlots[i])
                {
                    Card drawed = words[Random.Range(0, words.Count - 1)];
                    drawed.gameObject.SetActive(true);
                    drawed.handIdx = i;
                    drawed.transform.position = hand[i].position;
                    availableSlots[i] = false;
                    words.Remove(drawed);
                }
            }
        }
        else
        {
            OnEnd();
        }
    }

    void OnEnd()
    {
        promptBoard.text = "Yippee! Your score: " + score;
    }

    public void OnSubmit()
    {
        string word = "";
        if (prefixSelected)
        {
            score += prefix.points;
            word += prefix.word;
            prefix.Discard();
        }
        word += root.word;
        score += root.points;
        root.Discard();
        if (suffixSelected)
        {
            score += suffix.points;
            word += suffix.word;
            suffix.Discard();
        }
        scoreBoard.text = "" + score;
        currentPrompt = currentPrompt.Replace("___", word);
        promptBoard.text = currentPrompt;
        prefixSelected = false;
        suffixSelected = false;
        Invoke("NewTurn", 3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
