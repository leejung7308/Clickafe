using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Status : MonoBehaviour
{
    GameObject UI;
    public List<Sprite> levelSprite;
    public List<Vector2> levelPosition;
    public List<GameObject> customers;
    public List<int> revenues;
    public List<int> coffeBeanCost;
    public List<int> maxCustomerCounts;
    public List<int> upgradeCosts;
    public GameObject customer;
    public bool isEmployed = false;
    public int scale = 1;
    public bool developerMode = false;
    void Start()
    {
        if (developerMode) scale = 10000;
        UI = GameObject.FindGameObjectWithTag("UI");
        for (int i = 0; i < 50; i++)
        {
            GameObject c = Instantiate(customer);
            customers.Add(c);
            c.gameObject.SetActive(false);
        }
        for (int i = 0; i < DataController.Instance.gameData.customerCount; i++)
        {
            customers[i].gameObject.SetActive(true);
            customers[i].transform.position = new Vector2(3, -3.5f);
        }
        SetSprite();
        StartCoroutine(SpawnCustomer());
    }
    public int Sell()
    {
        if (DataController.Instance.gameData.coffeeBean >= coffeBeanCost[DataController.Instance.gameData.level-1])
        {
            DataController.Instance.gameData.coffeeBean -= coffeBeanCost[DataController.Instance.gameData.level - 1];
            DataController.Instance.gameData.money += revenues[DataController.Instance.gameData.level-1] * scale;
            UI.GetComponent<UI>().RevenueText(revenues[DataController.Instance.gameData.level-1] * scale);
            SoundManager.instance.PlaySound("Coin");
            return 1;
        }
        else
        {
            UI.GetComponent<UI>().CoffeeBeanShortageMessage();
            return -1;
        }

    }
    public void Purchase1()
    {
        if (DataController.Instance.gameData.money > 100)
        {
            SoundManager.instance.PlaySound("Purchase");
            DataController.Instance.gameData.money -= 100;
            DataController.Instance.gameData.coffeeBean += 100;
        }
        else
        {
            UI.GetComponent<UI>().MoneyShortageMessage();
        }
    }
    public void Purchase2()
    {
        if (DataController.Instance.gameData.money > 1000)
        {
            SoundManager.instance.PlaySound("Purchase");
            DataController.Instance.gameData.money -= 1000;
            DataController.Instance.gameData.coffeeBean += 1050;
        }
        else
        {
            UI.GetComponent<UI>().MoneyShortageMessage();
        }
    }
    public void Purchase3()
    {
        if (DataController.Instance.gameData.money > 5000)
        {
            SoundManager.instance.PlaySound("Purchase");
            DataController.Instance.gameData.money -= 5000;
            DataController.Instance.gameData.coffeeBean += 5500;
        }
        else
        {
            UI.GetComponent<UI>().MoneyShortageMessage();
        }
    }
    public void Purchase4()
    {
        if (DataController.Instance.gameData.money > 10000)
        {
            SoundManager.instance.PlaySound("Purchase");
            DataController.Instance.gameData.money -= 10000;
            DataController.Instance.gameData.coffeeBean += 11500;
        }
        else
        {
            UI.GetComponent<UI>().MoneyShortageMessage();
        }
    }
    public void Upgrade2()
    {
        if (DataController.Instance.gameData.money > upgradeCosts[1]+100 && DataController.Instance.gameData.level < 2)
        {
            DataController.Instance.gameData.money -= upgradeCosts[1];
            DataController.Instance.gameData.level = 2;
            DataController.Instance.gameData.maxCustomerCount = maxCustomerCounts[DataController.Instance.gameData.level-1];
            SetSprite();
            SoundManager.instance.PlaySound("Purchase");
        }
        else
        {
            if (DataController.Instance.gameData.level >= 2)
            {
                UI.GetComponent<UI>().PurchaseErrorMessage();
            }
            else
            {
                UI.GetComponent<UI>().MoneyShortageMessage();
            }
        }
    }
    public void Upgrade3()
    {
        if (DataController.Instance.gameData.money > upgradeCosts[2] + 100 && DataController.Instance.gameData.level < 3)
        {
            DataController.Instance.gameData.money -= upgradeCosts[2];
            DataController.Instance.gameData.level = 3;
            DataController.Instance.gameData.maxCustomerCount = maxCustomerCounts[DataController.Instance.gameData.level - 1];
            SetSprite();
            SoundManager.instance.PlaySound("Purchase");
        }
        else
        {
            if (DataController.Instance.gameData.level >= 3)
            {
                UI.GetComponent<UI>().PurchaseErrorMessage();
            }
            else
            {
                UI.GetComponent<UI>().MoneyShortageMessage();
            }
        }
    }
    public void Upgrade4()
    {
        if (DataController.Instance.gameData.money > upgradeCosts[3] + 100 && DataController.Instance.gameData.level < 4)
        {
            DataController.Instance.gameData.money -= upgradeCosts[3];
            DataController.Instance.gameData.level = 4;
            DataController.Instance.gameData.maxCustomerCount = maxCustomerCounts[DataController.Instance.gameData.level - 1];
            SetSprite();
            SoundManager.instance.PlaySound("Purchase");
        }
        else
        {
            if (DataController.Instance.gameData.level >= 4)
            {
                UI.GetComponent<UI>().PurchaseErrorMessage();
            }
            else
            {
                UI.GetComponent<UI>().MoneyShortageMessage();
            }
        }
    }
    public void Upgrade5()
    {
        if (DataController.Instance.gameData.money > upgradeCosts[4] + 100 && DataController.Instance.gameData.level < 5)
        {
            DataController.Instance.gameData.money -= upgradeCosts[4];
            DataController.Instance.gameData.level = 5;
            DataController.Instance.gameData.maxCustomerCount = maxCustomerCounts[DataController.Instance.gameData.level - 1];
            SetSprite();
            SoundManager.instance.PlaySound("Purchase");
        }
        else
        {
            if (DataController.Instance.gameData.level >= 5)
            {
                UI.GetComponent<UI>().PurchaseErrorMessage();
            }
            else
            {
                UI.GetComponent<UI>().MoneyShortageMessage();
            }
        }
    }
    public void Upgrade6()
    {
        if (DataController.Instance.gameData.money > upgradeCosts[5] + 1000 && DataController.Instance.gameData.level < 6)
        {
            DataController.Instance.gameData.money -= upgradeCosts[5];
            DataController.Instance.gameData.level = 6;
            DataController.Instance.gameData.maxCustomerCount = maxCustomerCounts[DataController.Instance.gameData.level - 1];
            SetSprite();
            SoundManager.instance.PlaySound("Purchase");
        }
        else
        {
            if (DataController.Instance.gameData.level >= 6)
            {
                UI.GetComponent<UI>().PurchaseErrorMessage();
            }
            else
            {
                UI.GetComponent<UI>().MoneyShortageMessage();
            }
        }
    }
    public void Upgrade7()
    {
        if (DataController.Instance.gameData.money > upgradeCosts[6] + 1000 && DataController.Instance.gameData.level < 7)
        {
            DataController.Instance.gameData.money -= upgradeCosts[6];
            DataController.Instance.gameData.level = 7;
            DataController.Instance.gameData.maxCustomerCount = maxCustomerCounts[DataController.Instance.gameData.level - 1];
            SetSprite();
            SoundManager.instance.PlaySound("Purchase");
        }
        else
        {
            if (DataController.Instance.gameData.level >= 7)
            {
                UI.GetComponent<UI>().PurchaseErrorMessage();
            }
            else
            {
                UI.GetComponent<UI>().MoneyShortageMessage();
            }
        }
    }
    public void Promotion()
    {
        if (DataController.Instance.gameData.money < 6000)
        {
            UI.GetComponent<UI>().MoneyShortageMessage();
        }
        else
        {
            if (DataController.Instance.gameData.customerCount < DataController.Instance.gameData.maxCustomerCount)
            {
                DataController.Instance.gameData.money -= 5000;
                customers[DataController.Instance.gameData.customerCount].gameObject.SetActive(true);
                customers[DataController.Instance.gameData.customerCount].transform.position = new Vector2(3, -5.5f);
                DataController.Instance.gameData.customerCount++;
            }
            else
            {
                UI.GetComponent<UI>().PurchaseErrorMessage();
            }
        }
    }
    IEnumerator SpawnCustomer()
    {
        while (true)
        {
            for (int i = 0; i < DataController.Instance.gameData.customerCount; i++)
            {
                customers[i].GetComponent<Customer>().StartMovement();
                yield return new WaitForSeconds(10.0f / DataController.Instance.gameData.customerCount);
            }
        }
    }
    public void SetSprite()
    {
        if (DataController.Instance.gameData.level == 7)
        {
            transform.localScale = new Vector3(0.7f, 0.7f, 0.5f);
        }
        else
        {
            transform.localScale = new Vector3(1.0f, 1.0f, 1.0f);
        }
        gameObject.GetComponent<SpriteRenderer>().sprite = levelSprite[DataController.Instance.gameData.level - 1];
        transform.position = levelPosition[DataController.Instance.gameData.level - 1];
    }
}
