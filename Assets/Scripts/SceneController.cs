using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SceneController : MonoBehaviour
{
    public int gridRows = 2;    //카드 열의 개수
    public int gridCols = 4;    //카드 행의 개수
    public float offset_x = 4f; //x값의 간격
    public float offset_y = 5f; //y값의 간격

    [SerializeField] private MainCard originalCard;
    [SerializeField] private Sprite[] image;
    [SerializeField] private int[] numbers;

    private void Start()
    {
        GameOver.SetActive(false);
        GameClear.SetActive(false);

        tx_count.color = new Color(1, 1, 1, 1);
        tx_count.text = "chance : " + countNumber;
        Vector3 startPos = originalCard.transform.position;

        numbers = ShuffleArray(numbers);

        for (int i = 0; i < gridCols; i++)
        {
            for (int j = 0; j < gridRows; j++)
            {
                MainCard card;
                if (i == 0 && j == 0)
                {
                    card = originalCard;
                }
                else
                {
                    card = Instantiate(originalCard) as MainCard;
                }

                int index = j * gridCols + i;
                int id = numbers[index];
                card.ChangeSprite(id, image[id]);

                float posX = (offset_x * i) + startPos.x;
                float posY = (offset_y * j) + startPos.y;
                card.transform.position = new Vector3(posX, posY, startPos.z);
            }
        }
    }

    private int[] ShuffleArray(int[] numbers) //섞는 함수
    {
        int[] newArray = numbers.Clone() as int[];

        for (int i = 0; i < newArray.Length; i++)
        {
            int tmp = newArray[i];
            int r = Random.Range(i, newArray.Length);
            //Debug.Log(r);
            newArray[i] = newArray[r];
            //Debug.Log(newArray[i]);
            newArray[r] = tmp;
            //Debug.Log(newArray[r]);
        }
        return newArray;
    }

    //------------------------------------------------------------------------------------------------------
    private MainCard _firstRevealed;
    private MainCard _secondRevealed;
    
    private int count = 0;
    private int finsh_count = 0;
    public int clear = 4;
    [SerializeField]private int countNumber;
    [SerializeField] private Text tx_count;

    public GameObject GameOver;
    public GameObject GameClear;

    public bool canReveal
    {
        get { return _secondRevealed == null; }
    }

    public void CardRevealed(MainCard card)
    {
        if(_firstRevealed == null)
        {
            _firstRevealed = card;
            count++;
        }
        else
        {
            _secondRevealed = card;
            count++;
            StartCoroutine(CheckMatch());
        }
    }

    public void CountChack()
    {
        if(count == 2)
        {
            count = 0;
            tx_count.text = "chance : " + countNumber;
            if (countNumber == 0 && finsh_count != clear)
            {
                //Debug.Log("GameOver 화면으로 이동");
                GameOver.SetActive(true);
                tx_count.color = new Color(1, 1, 1, 0);
                return;
            }
        }
    }

    private IEnumerator CheckMatch()
    {
        if(_firstRevealed.id == _secondRevealed.id)
        {
            finsh_count++;

            if (finsh_count == clear)
            {
                //Debug.Log("게임종료");
                GameClear.SetActive(true);
                tx_count.color = new Color(1, 1, 1, 0);
            }
        }
        else
        {
            yield return new WaitForSeconds(0.5f);

            _firstRevealed.Unreveal();
            _secondRevealed.Unreveal();
            countNumber--;
        }

        CountChack();
        _firstRevealed = null;
        _secondRevealed = null;
    }
}
