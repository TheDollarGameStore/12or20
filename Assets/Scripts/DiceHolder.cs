using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiceHolder : MonoBehaviour
{
    public GameObject dice;

    [HideInInspector]
    public List<GameObject> dices;


    public void ShuffleDices()
    {
        dices = new List<GameObject>();

        List<int> numbers = new List<int> { 1, 1, 2, 2, 3, 3, 4, 4, 5, 5, 6, 6};

        while (dices.Count != 6)
        {
            GameObject newDice = Instantiate(dice, new Vector2(-40f + (16f * dices.Count), transform.position.y), Quaternion.identity);

            int pickValueIndex =Random.Range(0, numbers.Count);

            newDice.GetComponent<Dice>().value = numbers[pickValueIndex];

            numbers.RemoveAt(pickValueIndex);

            dices.Add(newDice);
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < dices.Count; i++)
        {
            dices[i].transform.position = Vector3.Lerp(dices[i].transform.position, new Vector2(-40f + (16f * i), transform.position.y), 10f * Time.deltaTime);
        }
    }
}
