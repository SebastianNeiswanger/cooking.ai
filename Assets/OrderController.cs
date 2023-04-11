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
    int CreateNewOrder()
    {
        System.Random random = new System.Random();
        // get random ingredients
        Array values = Enum.GetValues(typeof(ingredient));
        int numIngredients = random.Next(values.Length + 1); // between 0 and numIngredients
        // Create list of random ingredients
        List<ingredient> ingredients = new List<ingredient>();
        for (int i = 0; i < numIngredients; ++i)
        {
            int randIngredientValue = Convert.ToInt32(Math.Pow(2, (random.Next(4, 4 + values.Length))));
            ingredients.Add((ingredient)values.GetValue(randIngredientValue));
        }

        int orderNum = openOrders.Count + completeOrders.Count;
        openOrders.Add(orderNum, new Order(ingredients));

        return orderNum;
    }

    void CompleteOrder(int orderNum)
    {
        Order order = openOrders[orderNum];
        openOrders.Remove(orderNum);
        completeOrders.Add(orderNum, order);
    }

    Order GetOrder(int orderNum)
    {
        if (openOrders.ContainsKey(orderNum))
        {
            return openOrders[orderNum];
        } else if (completeOrders.ContainsKey(orderNum))
        {
            return completeOrders[orderNum];
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

// TODO: Make an entree -> ingredient dictionary
enum ingredient
{ // uncookedBeef, cookedBeef, buns, and plate are 1, 2, 4, and 8
    cheese = 16,
    tomato = 32,
    lettuce = 64,
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
        int serialized = 7; // Beef, buns, and plate
        foreach (ingredient i in ingredients)
        {
            serialized += (int)i;
        }
        return serialized;
    }
}