using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrderController : MonoBehaviour
{
    private Dictionary<int, Order> openOrders;
    private Dictionary<int, Order> completeOrders;

    void Start()
    {
        openOrders = new Dictionary<int, Order>();
        completeOrders = new Dictionary<int, Order>();
    }

    // Returns order number
    public int CreateNewOrder()
    {
        System.Random random = new System.Random();
        // get random ingredients
        Array values = Enum.GetValues(typeof(ingredient));
        int numIngredients = random.Next(4); // between 0 and 3 (cheese, lettuce, tomato)
        // Create list of random ingredients
        List<ingredient> ingredients = new List<ingredient>();
        while (ingredients.Count < numIngredients)
        {
            switch (random.Next(3))
            {
                case 0:
                    if (!ingredients.Contains(ingredient.cutCheese))
                    {
                        ingredients.Add(ingredient.cutCheese);
                    }
                    break;
                case 1:
                    if (!ingredients.Contains(ingredient.cutLettuce))
                    {
                        ingredients.Add(ingredient.cutLettuce);
                    }
                    break;
                case 2:
                    if (!ingredients.Contains(ingredient.cutTomato))
                    {
                        ingredients.Add(ingredient.cutTomato);
                    }
                    break;
            }
        }

        int orderNum = openOrders.Count + completeOrders.Count;
        openOrders.Add(orderNum, new Order(ingredients));

        return orderNum;
    }

    public void CompleteOrder(int burger)
    {
        foreach ((int index, Order order) in openOrders)
        {
            if (burger == order.GetSerializedIngredients())
            {
                openOrders.Remove(index);
                completeOrders.Add(index, order);
                // Give reward?
                break;
            }
        }
    }

    // Complete order by orderNum. This may be useful later, or for something else
    //void CompleteOrder(int orderNum)
    //{
    //    Order order = openOrders[orderNum];
    //    openOrders.Remove(orderNum);
    //    completeOrders.Add(orderNum, order);
    //}

    public int GetOrder(int orderNum)
    {
        if (openOrders.ContainsKey(orderNum))
        {
            return openOrders[orderNum].GetSerializedIngredients();
        } else if (completeOrders.ContainsKey(orderNum))
        {
            return completeOrders[orderNum].GetSerializedIngredients();
        } else
        {
            throw new KeyNotFoundException("Order " + orderNum.ToString() + " not found");
        }
    }

    public List<int> SendOrdersToAgent()
    {
        List<int> observations = new List<int>();
        foreach ((int index, Order o) in openOrders)
        {
            observations.Add(o.GetSerializedIngredients());
        }
        return observations;
    }
}

enum ingredient
{
    // bitfield
    uncookedBeef = 1,
    cookedBeef = 2,
    buns = 4,
    plate = 8,
    uncutCheese = 16,
    cutCheese = 32,
    uncutTomato = 64,
    cutTomato = 128,
    uncutLettuce = 256,
    cutLettuce = 512
}
class Order
{
    private List<ingredient> ingredients;

    public Order(List<ingredient> i)
    {
        ingredients = i;        
    }

    public int GetSerializedIngredients()
    {
        int serialized = 13; // Beef, buns, and plate
        foreach (ingredient i in ingredients)
        {
            serialized += (int)i;
        }
        return serialized;
    }
}