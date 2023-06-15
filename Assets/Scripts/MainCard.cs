using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainCard : MonoBehaviour
{
    [SerializeField] private SceneController controller;
    [SerializeField] private GameObject Card_Back;
    public float timeScale = 1.0f;
    
    private void Start()
    {
        StartCoroutine("Wait");
    }

    public void OnMouseDown()
    {
        if(Card_Back.activeSelf && controller.canReveal)
        {
            Card_Back.SetActive(false);
            controller.CardRevealed(this);
        }
    }
    
    private int _id;
    public int id
    {
        get { return _id; }
    }

    public void ChangeSprite(int id, Sprite image)
    {
        _id = id;
        GetComponent<SpriteRenderer>().sprite = image;
    }   

    public void Unreveal()
    {
        Card_Back.SetActive(true);
    }

    IEnumerator Wait()
    {
        Card_Back.SetActive(false);
        yield return new WaitForSeconds(timeScale);
        Card_Back.SetActive(true);
    }
}
