using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeStoreManager : MonoBehaviour
{
    public Image purchasedImage;
    public Button purchaseButton;
    public Button prevButton;
    public Button nextButton;
    public Image spritePerLevel;
    public List<Sprite> spritesPerLevel;
    public Text infoText;
    public Text priceText;
    public Text levelText;
    public Image lockedImage;
    private int currentPage;
    public GameObject player;
    public void OnEnable()
    {
        currentPage = DataController.Instance.gameData.level - 1;
        UpgradeStore(currentPage);
    }
    public void UpgradeStore(int currentPage)
    {
        purchasedImage.gameObject.SetActive(false);
        purchaseButton.gameObject.SetActive(true);
        prevButton.gameObject.SetActive(true);
        nextButton.gameObject.SetActive(true);
        spritePerLevel.sprite = spritesPerLevel[currentPage];
        infoText.text = "+" + player.GetComponent<Status>().revenues[currentPage] + "\n" + "-" + player.GetComponent<Status>().coffeBeanCost[currentPage]+"\n"+"최대 "+player.GetComponent<Status>().maxCustomerCounts[currentPage]+"명";
        priceText.text = "가격 : " + player.GetComponent<Status>().upgradeCosts[currentPage] + " 골드";
        levelText.text = "LV" + (currentPage + 1).ToString();

        if (currentPage <= DataController.Instance.gameData.level - 1)
        {
            purchaseButton.gameObject.SetActive(false);
            purchasedImage.gameObject.SetActive(true);
        }

        if (currentPage <= DataController.Instance.gameData.level)
            lockedImage.gameObject.SetActive(false);
        else
            lockedImage.gameObject.SetActive(true);

        if (currentPage == 0)
        {
            prevButton.gameObject.SetActive(false);
            priceText.text = "기본";
        }
        else if (currentPage == 6)
        {
            nextButton.gameObject.SetActive(false);
        }
    }
    public void PreviousPage()
    {
        UpgradeStore(--currentPage);
    }
    public void NextPage()
    {
        UpgradeStore(++currentPage);
    }
    public void Upgrade()
    {
        switch (DataController.Instance.gameData.level)
        {
            case 1:
                player.GetComponent<Status>().Upgrade2();
                break;
            case 2:
                player.GetComponent<Status>().Upgrade3();
                break;
            case 3:
                player.GetComponent<Status>().Upgrade4();
                break;
            case 4:
                player.GetComponent<Status>().Upgrade5();
                break;
            case 5:
                player.GetComponent<Status>().Upgrade6();
                break;
            case 6:
                player.GetComponent<Status>().Upgrade7();
                break;
        }
        UpgradeStore(currentPage);
    }
}
