// Ways to fix current problem:

// 1. Everything cost 1, put everything in different function as If == 10 -> UpdateOre1

// if == 20 -> UpdateOreAfterBuyingPickaxe2
// If == 30 -> UpdateOreAfterBuyingPickaxe3

// 2. Make a function that repeats itself X amount of timmes based on the amount of resources needed
// So if it needs 5 iron ore, deduct iron ore function repeats itself 5 times. And Each time it deducts 1.

// 3. Make an Arrray[][] where you setup all the prices, 
// and then based on that a function runs "X" amount of times.
// As in the point above.

// How to make repeatable function:

/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Technology : MonoBehaviour
{
    int xTimes;
    int priceA = 5;
    int priceB = 8;
    int InitialAmount = 100;

    void RepeatableFunction1()
    {
        InitialAmount -= 1;
    }

    void RepeatableFunction2()
    {
        InitialAmount -= 1;
    }

    void RepeatableFunction3()
    {
        InitialAmount -= 1;
    }

    void RepeatableFunction4()
    {
        InitialAmount -= 1;
    }

    void RepeatableFunction5()
    {
        InitialAmount -= 1;
    }

    void RepeatableFunction6()
    {
        InitialAmount -= 1;
    }

    private void Start()
    {
        // 5 Times Repeat a function
        xTimes = 5;
        for(int i=priceA; i <= priceA; i++)
        {
            RepeatableFunctionA();
        }

        for(int i=priceB; i <= priceB; i++)
        {
            RepeatableFunctionB();
        }

        if(X == 10)
        {
            Price1 = 3
            Price2 = 4
            for (int i = price1; i <= price1; i++)
            {
                RepeatableFunctionA();
            }

            for (int i = price2; i <= price2; i++)
            {
                RepeatableFunctionB();
            }
        }
        if (X == 20)
        {
            for (int i = priceD; i <= priceD; i++)
            {
                RepeatableFunctionA();
            }

            for (int i = priceE; i <= priceE; i++)
            {
                RepeatableFunctionB();
            }
        }
    }

    // 1 Price1: 3 // Price2: 4
    // 2 Price3: 3 // Price6: 1 // Price 2: 1




    // 3
    // 4
    // 5
    // 6
    // 7
    // 8
    // 9
    // 10
}*/