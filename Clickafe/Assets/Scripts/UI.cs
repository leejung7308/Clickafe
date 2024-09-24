using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class UI : MonoBehaviour
{
    public GameObject player;

    public GameObject money;
    public GameObject coffeeBean;
    public GameObject timer;

    public Text moneyText;
    public Text coffeeBeanText;
    public Text timerText;
    public Text thumbDownText;
    public Text customerText;

    public Image coffeeBeanStore;
    public Image upgradeStore;
    public Image employPage;
    public Image employedPage;
    public Image promotionPage;
    public Image settings;
    public Image quitConfirmPage;

    public Image coffeeBeanShortage;
    public Image moneyShortage;
    public Image purchaseError;

    public Image thumbDownBar;
    public Image customerBar;

    public Image revenuePrefab;
    Queue<Image> revenueImages = new Queue<Image>();

    public Action quitEvent;

    private int currentPage = 1;
    private bool isBeanShortageAlerted = false;
    private bool isEmployed = false;
    private bool isMoneyShortageAlerted = false;
    private bool isPurchaseErrorAlerted = false;
    private bool isApplicationQuit = false;

    float employeeTime;

    private void Awake()
    {
        InitializeApplicationQuit();
    }
    private void Start()
    {
        for (int i = 0; i < 20; i++)
        {
            Image temp = Instantiate(revenuePrefab, Vector2.zero, Quaternion.identity, GameObject.Find("UI").transform);
            temp.transform.localPosition = new Vector2(-100, 0);
            temp.transform.SetSiblingIndex(i);
            temp.gameObject.SetActive(false);
            revenueImages.Enqueue(temp);
        }
    }
    void Update()
    {
        moneyText.text = DataController.Instance.gameData.money.ToString();
        coffeeBeanText.text = DataController.Instance.gameData.coffeeBean.ToString();
        thumbDownBar.fillAmount = DataController.Instance.gameData.thumbDownCount / 20f;
        thumbDownText.text = string.Format("{0}/20", DataController.Instance.gameData.thumbDownCount);
        customerBar.fillAmount = DataController.Instance.gameData.customerCount / (float)DataController.Instance.gameData.maxCustomerCount;
        customerText.text = string.Format("{0}/{1}", DataController.Instance.gameData.customerCount, DataController.Instance.gameData.maxCustomerCount);
    }
    public void OpenCoffeeBeanStore()
    {
        isBeanShortageAlerted = true;
        coffeeBeanShortage.gameObject.SetActive(false);
        coffeeBeanStore.gameObject.SetActive(true);
        money.transform.localPosition = new Vector2(0, 100);
        coffeeBean.transform.localPosition = new Vector2(0, 100);
    }
    public void CloseCoffeBeanStore()
    {
        isBeanShortageAlerted = false;
        coffeeBeanStore.gameObject.SetActive(false);
        money.transform.localPosition = new Vector2(0, 0);
        coffeeBean.transform.localPosition = new Vector2(0, 0);
    }
    public void OpenUpgradeStore()
    {
        upgradeStore.gameObject.SetActive(true);
        money.transform.localPosition = new Vector2(0, 100);
        coffeeBean.transform.localPosition = new Vector2(0, 100);
    }
    public void CloseUpgradeStore()
    {
        upgradeStore.gameObject.SetActive(false);
        money.transform.localPosition = new Vector2(0, 0);
        coffeeBean.transform.localPosition = new Vector2(0, 0);
    }
    public void OpenEmployPage()
    {
        if (isEmployed)
        {
            employedPage.gameObject.SetActive(true);
        }
        else
        {
            employPage.gameObject.SetActive(true);
        }
    }
    public void CloseEmployPage()
    {
        if (isEmployed)
        {
            employedPage.gameObject.SetActive(false);
        }
        else
        {
            employPage.gameObject.SetActive(false);
        }
    }
    public void Employ()
    {
        if (DataController.Instance.gameData.money < 11000)
        {
            MoneyShortageMessage();
            CloseEmployPage();
        }
        else
        {
            CloseEmployPage();
            if (isEmployed)
            {
                employeeTime += 600;
            }
            else
            {
                StartCoroutine(EmployCoroutine());
            }
        }
    }
    public void OpenPromotionPage()
    {
        promotionPage.gameObject.SetActive(true);
    }
    public void ClosePromotionPage()
    {
        promotionPage.gameObject.SetActive(false);
    }
    public void CoffeeBeanShortageMessage()
    {
        if (!isBeanShortageAlerted)
        {
            settings.gameObject.SetActive(false);
            upgradeStore.gameObject.SetActive(false);
            employedPage.gameObject.SetActive(false);
            promotionPage.gameObject.SetActive(false);
            coffeeBeanShortage.gameObject.SetActive(true);
            isBeanShortageAlerted = true;
        }
    }
    public void CloseCoffeeBeanShortageMessage()
    {
        coffeeBeanShortage.gameObject.SetActive(false);
    }
    public void OpenSettings()
    {
        settings.gameObject.SetActive(true);
    }
    public void CloseSettings()
    {
        settings.gameObject.SetActive(false);
    }
    IEnumerator EmployCoroutine()
    {
        if (!isEmployed)
        {
            isEmployed = true;
            player.GetComponent<Status>().scale *= 2;
            employeeTime = 600;
            timer.gameObject.SetActive(true);
            while(employeeTime > 0)
            {
                timerText.text = ((int)(employeeTime / 60)).ToString() + " : " + ((int)(employeeTime % 60)).ToString().PadLeft(2,'0');
                employeeTime -= Time.deltaTime;
                yield return null;
            }
            timer.gameObject.SetActive(false);
            player.GetComponent<Status>().scale = 1;
            isEmployed = false;
        }
    }
    public void Promotion()
    {
        player.GetComponent<Status>().Promotion();
        OpenPromotionPage();
    }
    private IEnumerator fadeInOut(Image target, float durationTime, bool inOut)
    {
        float start, end;
        if (inOut)
        {
            start = 0.0f;
            end = 1f;
        }
        else
        {
            start = 1f;
            end = 0f;
        }

        float elapsedTime = 0.0f;

        while (elapsedTime < durationTime)
        {
            float alpha = Mathf.Lerp(start, end, elapsedTime / durationTime);

            target.GetComponent<CanvasGroup>().alpha = alpha;

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    private IEnumerator MoneyShortageMessageCoroutine(float durationTime)
    {
        Color originalColor = moneyShortage.color;
        moneyShortage.gameObject.SetActive(true);
        isMoneyShortageAlerted = true;
        yield return fadeInOut(moneyShortage, 0.3f, true);

        float elapsedTime = 0.0f;
        while (elapsedTime < durationTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return fadeInOut(moneyShortage, 0.3f, false);

        moneyShortage.gameObject.SetActive(false);
        moneyShortage.color = originalColor;
        isMoneyShortageAlerted = false;
    }
    private IEnumerator PurchaseErrorMessageCoroutine(float durationTime)
    {
        Color originalColor = purchaseError.color;
        purchaseError.gameObject.SetActive(true);
        isPurchaseErrorAlerted = true;
        yield return fadeInOut(purchaseError, 0.3f, true);

        float elapsedTime = 0.0f;
        while (elapsedTime < durationTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return fadeInOut(purchaseError, 0.3f, false);

        purchaseError.gameObject.SetActive(false);
        purchaseError.color = originalColor;
        isPurchaseErrorAlerted = false;
    }
    private IEnumerator RevenueTextCoroutine(float durationTime, int revenue)
    {
        Image temp = revenueImages.Dequeue();
        StartCoroutine(MoveRevenue(temp));
        Color originalColor = temp.color;
        temp.transform.GetChild(0).GetComponent<Text>().text = "+" + revenue.ToString(); 
        temp.gameObject.SetActive(true);
        yield return fadeInOut(temp, 0.3f, true);

        float elapsedTime = 0.0f;
        while (elapsedTime < durationTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        yield return fadeInOut(temp, 0.3f, false);

        temp.gameObject.SetActive(false);
        temp.color = originalColor;
        revenueImages.Enqueue(temp);
    }
    private IEnumerator MoveRevenue(Image revenue)
    {

        for (int i = 0; i < 100; i++)
        {
            revenue.transform.localPosition = new Vector2(-100, i*1.5f);
            yield return new WaitForSeconds(0.01f);
        }
    }
    public void MoneyShortageMessage()
    {
        if(!isMoneyShortageAlerted)
            StartCoroutine(MoneyShortageMessageCoroutine(0.5f));
    }
    public void PurchaseErrorMessage()
    {
        if (!isPurchaseErrorAlerted)
            StartCoroutine(PurchaseErrorMessageCoroutine(0.5f));

    }
    public void RevenueText(int revenue)
    {
        StartCoroutine(RevenueTextCoroutine(0.5f, revenue));
    }
    public void ButtonSoundPlay()
    {
        SoundManager.instance.PlaySound("Button");
    }
    private void InitializeApplicationQuit()
    {
        quitEvent += () =>
        {
            quitConfirmPage.gameObject.SetActive(true);
        };
        Application.wantsToQuit += ApplicationQuit;
    }
    public void Quit()
    {
        isApplicationQuit = true;
        Application.Quit();
    }
    public void CloseQuitConfirmPage()
    {
        isApplicationQuit = false;
        quitConfirmPage.gameObject.SetActive(false);
    }
    private bool ApplicationQuit()
    {
        if (!isApplicationQuit)
        {
            quitEvent?.Invoke();
        }
        return isApplicationQuit;
    }
    public void ResetGame()
    {
        DataController.Instance.gameData.coffeeBean = 100;
        DataController.Instance.gameData.money = 1000;
        DataController.Instance.gameData.customerCount = 4;
        DataController.Instance.gameData.maxCustomerCount = 10;
        DataController.Instance.gameData.level = 1;
        DataController.Instance.gameData.thumbDownCount = 0;
        player.GetComponent<Status>().SetSprite();
    }
}
