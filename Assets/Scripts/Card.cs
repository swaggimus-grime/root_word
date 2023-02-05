using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Card : MonoBehaviour
{
    public enum TYPE { 
        PREFIX,
        SUFFIX,
        ROOT
    };
    private static System.Random rng = new System.Random();
    public string word;
    public int points;
    public TYPE type;
    public TextMeshPro tmp;
    public bool hasPlayed;
    public int handIdx;
    private GameMode gm;
    private Transform originalPos;

    // Start is called before the first frame update
    void Start()
    {
        gm = FindObjectOfType<GameMode>();
        tmp.text = word;
        points = rng.Next(1, 11);
        originalPos = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        if (type == TYPE.ROOT)
            return;

        if(!hasPlayed)
        {     
            hasPlayed = true;
            gm.availableSlots[handIdx] = true;
            if (type == TYPE.PREFIX)
            {
                transform.position = gm.prePos.position;
                gm.prefix = this;
                gm.prefixSelected = true;
            }
            else
            {
                transform.position = gm.suffPos.position;
                gm.suffix = this;
                gm.suffixSelected = true;
            }
        }
        else
        {
            transform.position = originalPos.position;
            hasPlayed = false;
            gm.availableSlots[handIdx] = false;
            if(type == TYPE.PREFIX)
                gm.prefixSelected = false;
            else
                gm.suffixSelected = false;
        }
    }

    public void Discard()
    {
        transform.position = gm.discardPos.position;
        gm.discardPile.Add(this);
        gameObject.SetActive(false);
    }
}
