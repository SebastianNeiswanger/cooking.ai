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
        // get random entree
        Array values = Enum.GetValues(typeof(entree));
        System.Random random = new System.Random();
        entree randomEntree = (entree)values.GetValue(random.Next(values.Length));

        // get random ingredients
        values = Enum.GetValues(typeof(ingredient));
        int numIngredients = random.Next(values.Length + 1);
        // Create list of random ingredients
        List<ingredient> ingredientList = new List<ingredient>();
        for (int i = 0; i < numIngredients; ++i)
        {
            ingredientList.Add((ingredient)values.GetValue(random.Next(values.Length)));
        }

        int orderNum = openOrders.Count + completeOrders.Count;
        openOrders.Add(orderNum, new Order(randomEntree, ingredientList));

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
}

enum entree
{
    burger
}

// TODO: Make an entree -> ingredient dictionary
enum ingredient
{
    lettuce,
    cheese,
    tomato
}
class Order
{
    private entree foodType;
    private List<ingredient> ingredients;

    public Order(entree type, List<ingredient> i)
    {
        foodType = type;
        ingredients = i;
    }
}