using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cell : MonoBehaviour
{
    public int y;
    public int x;

    [HideInInspector]
    public GameObject dice;

    [SerializeField] private AudioClip shuffleDice;

    [SerializeField] private AudioClip place;

    public void PlaceDice()
    {
        if (GameManager.instance.diceHolder.dices.Count != 0 && dice == null)
        {
            SoundManager.instance.PlayRandomized(place);
            dice = GameManager.instance.diceHolder.dices[0];
            GameManager.instance.diceHolder.dices.RemoveAt(0);
            dice.transform.position = transform.position;
            dice.GetComponent<Wobble>().DoTheWobble();

            GameManager.instance.CheckMatches();

            if (GameManager.instance.diceHolder.dices.Count == 0)
            {
                GameManager.instance.diceHolder.Invoke("ShuffleDices", 1f);
                SoundManager.instance.PlayRandomized(shuffleDice);
            }
        }
    }
}
