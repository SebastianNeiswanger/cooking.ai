using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurgerDisplay : MonoBehaviour
{
    GameObject plate;
    GameObject buns;
    GameObject cookedBurger;
    GameObject cheese;
    GameObject lettuce;
    GameObject tomato;

    GameObject uncookedBurger;
    GameObject uncutCheese;
    GameObject uncutLettuce;
    GameObject uncutTomato;

    private void Start()
    {
        plate = transform.GetChild(0).gameObject;
        buns = transform.GetChild(1).gameObject;
        cookedBurger = transform.GetChild(2).gameObject;
        cheese = transform.GetChild(3).gameObject;
        lettuce = transform.GetChild(4).gameObject;
        tomato = transform.GetChild(5).gameObject;
        uncookedBurger = transform.GetChild(6).gameObject;
        uncutCheese = transform.GetChild(7).gameObject;
        uncutLettuce = transform.GetChild(8).gameObject;
        uncutTomato = transform.GetChild(9).gameObject;

        foreach(Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
    }

    enum ingredient
    {
        // bitfield
        nothing = 0b_0000_0000_0000,  // 0
        uncookedBeef = 0b_0000_0000_0001,  // 1
        cookedBeef = 0b_0000_0000_0010,  // 2
        buns = 0b_0000_0000_0100,  // 4
        plate = 0b_0000_0000_1000,  // 8
        uncutCheese = 0b_0000_0001_0000,  // 16
        cutCheese = 0b_0000_0010_0000,  // 32
        uncutTomato = 0b_0000_0100_0000,  // 64
        cutTomato = 0b_0000_1000_0000,  // 128
        uncutLettuce = 0b_0001_0000_0000,  // 256
        cutLettuce = 0b_0010_0000_0000,  // 512
    }

    public void DisplayBurger(int burger)
    {
        // for each ingredient, if it's in the burger, activate it. Otherwise, deactivate it
        uncookedBurger.SetActive((((ingredient)burger & ingredient.uncookedBeef) != 0));
        cookedBurger.SetActive((((ingredient)burger & ingredient.cookedBeef) != 0));
        buns.SetActive((((ingredient)burger & ingredient.buns) != 0));
        plate.SetActive((((ingredient)burger & ingredient.plate) != 0));
        uncutCheese.SetActive((((ingredient)burger & ingredient.uncutCheese) != 0));
        cheese.SetActive((((ingredient)burger & ingredient.cutCheese) != 0));
        uncutTomato.SetActive((((ingredient)burger & ingredient.uncutTomato) != 0));
        tomato.SetActive((((ingredient)burger & ingredient.cutTomato) != 0));
        uncutLettuce.SetActive((((ingredient)burger & ingredient.uncutLettuce) != 0));
        lettuce.SetActive((((ingredient)burger & ingredient.cutLettuce) != 0));
    }
}
