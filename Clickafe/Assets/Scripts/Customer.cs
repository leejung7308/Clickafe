using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public float spawnDelay = 0f;
    public Sprite back;
    public Sprite front;
    GameObject player;
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    public void StartMovement()
    {
        StartCoroutine(CustomerMovement());
    }
    IEnumerator CustomerMovement()
    {
        gameObject.GetComponent<SpriteRenderer>().sprite = back;
        transform.position = new Vector2(3, -5.5f);
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-3, 4).normalized*2;
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<Rigidbody2D>().velocity *= 0;
        yield return new WaitForSeconds(1f);
        gameObject.GetComponent<SpriteRenderer>().sprite = front;
        if (player.GetComponent<Status>().Sell() < 0)
        {
            transform.Find("ThumbDown").gameObject.SetActive(true);
            if (DataController.Instance.gameData.thumbDownCount < 20)
            {
                DataController.Instance.gameData.thumbDownCount++;
            }
            else
            {
                DataController.Instance.gameData.thumbDownCount -= 19;
                if(DataController.Instance.gameData.customerCount > 0) DataController.Instance.gameData.customerCount--;
            }
        }
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(-3, -4).normalized*2;
        yield return new WaitForSeconds(2.5f);
        gameObject.GetComponent<Rigidbody2D>().velocity *= 0;
        transform.Find("ThumbDown").gameObject.SetActive(false);
    }
}
