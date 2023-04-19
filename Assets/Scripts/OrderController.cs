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
    public int CreateNewOrder(int burger = 0)
    {
        int orderNum = openOrders.Count + completeOrders.Count;
        if (burger == 0)
        {
            burger += 14;
            System.Random random = new System.Random();
            // get random ingredients
            int numIngredients = random.Next(4); // between 0 and 3 (cheese, lettuce, tomato)
                                                 // Create list of random ingredients
            int i = 0;
            while (i < numIngredients)
            {
                switch (random.Next(3))
                {
                    // If burger does not have given ingredient, add it to burger
                    case 0:
                        if (((ingredient) burger & ingredient.cutCheese) == 0)
                        {
                            burger += (int) ingredient.cutCheese;
                            ++i;
                        }
                        break;
                    case 1:
                        if (((ingredient)burger & ingredient.cutLettuce) == 0)
                        {
                            burger += (int)ingredient.cutLettuce;
                            ++i;
                        }
                        break;
                    case 2:
                        if (((ingredient)burger & ingredient.cutTomato) == 0)
                        {
                            burger += (int)ingredient.cutTomato;
                            ++i;
                        }
                        break;
                }
            }
        }
        Order order = new Order(burger);
        openOrders.Add(orderNum, order);
        
        return orderNum;
    }

    public bool CompleteOrder(int burger)
    {
        foreach ((int index, Order order) in openOrders)
        {
            if (burger == order.GetSerializedIngredients())
            {
                openOrders.Remove(index);
                completeOrders.Add(index, order);
                return true;
            }
        }
        return false;
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

    public void ResetOrders()
    {
        openOrders = new Dictionary<int, Order>();
        completeOrders = new Dictionary<int, Order>();
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
class Order
{
    ingredient ingredients;

    public Order(int i)
    {
        ingredients = (ingredient)i;
    }

    public int GetSerializedIngredients()
    {
        return (int)ingredients;
    }

    public void SetIngredients(int i)
    {
        ingredients = (ingredient)i;
    }
}