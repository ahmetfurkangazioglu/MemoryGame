using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;
public class GameControl : MonoBehaviour
{
   
    GameObject FirstSprite;
    GameObject ButtonSprite;
    int SpriteNumber;
    public int TargetAmount;
    int SuccessAmount;
    int AddedSpriteValu=0;
    int TotalSprite;
    bool CrateFinish=true;
    public TextMeshProUGUI Timer;
    public float TotalTime;
    public bool isThereTime;
    private float Minute;
    private float Second;
    public Sprite DefaultSprite;
    public GameObject Grid;
    public GameObject SpritePool;
    public AudioSource[] Voices;
    public List<GameObject> Images;
    public GameObject[] GamePanels;
    void Start()
    {
        SpriteNumber = -1;
        StartCoroutine(CreateLevel());
    }

    private void Update()
    {
        StartTime();
    }

    IEnumerator CreateLevel()
    {
        yield return new WaitForSeconds(.1f);
        while (CrateFinish)
        {
            TotalSprite = SpritePool.transform.childCount;
            int RandomValue = Random.Range(0, SpritePool.transform.childCount);

            if (SpritePool.transform.GetChild(RandomValue).gameObject !=null)
            {           
                SpritePool.transform.GetChild(RandomValue).SetParent(Grid.transform);
                AddedSpriteValu++;
                if (AddedSpriteValu == TotalSprite)
                {
                    CrateFinish = false;
                    Destroy(SpritePool.gameObject);
                    
                }
            }
        }
    }
  
    public void TakeGameObject(GameObject MySprite)
    {
        ButtonSprite = MySprite;
        Voices[0].Play();
        ButtonSprite.GetComponent<Image>().sprite = ButtonSprite.GetComponentInChildren<SpriteRenderer>().sprite;
        ButtonSprite.GetComponent<Image>().raycastTarget = false;
    }
    public void TakeSpriteNumber(int Number)
    {
        if (SpriteNumber==-1)
        {
            SpriteNumber = Number;
            FirstSprite = ButtonSprite;
        }
        else
        {
          RayCastTargetControl(false);
          StartCoroutine(ControlSprite(Number));     
        }
    }
    IEnumerator ControlSprite(int Number)
    {
      yield return new WaitForSeconds(1f);

        if (SpriteNumber == Number)
        {
            SuccessAmount++;
            ButtonSprite.GetComponent<Image>().enabled = false;
            FirstSprite.GetComponent<Image>().enabled = false;
            if (TargetAmount==SuccessAmount)
            {
                EndGame(0);
            }
        }
        else
        {
            Voices[1].Play();
            ButtonSprite.GetComponent<Image>().sprite = DefaultSprite;
            FirstSprite.GetComponent<Image>().sprite = DefaultSprite;
        }
        SpriteNumber = -1;
        RayCastTargetControl(true);
        FirstSprite = null;
    }
  


    void RayCastTargetControl(bool Value)
    {
        foreach (var item in Images)
        {
            if (item!=null)
            {
                item.GetComponent<Image>().raycastTarget = Value;
            }        
        }
    }


    public void MainMenu(string respond)
    {
        switch (respond)
        {
            case "Sure":
               GamePanels[2].SetActive(true);
                Time.timeScale = 0;
                 break;
            case "Yes":
                Time.timeScale = 1;
                SceneManager.LoadScene("MainMenu");
                break;
            case "No":
                GamePanels[2].SetActive(false);
                Time.timeScale = 1;
                break;
        }
        
    }
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    private void StartTime()
    {
        if (isThereTime && TotalTime > 1)
        {
            TotalTime -= Time.deltaTime;
            Minute = Mathf.FloorToInt(TotalTime / 60);
            Second = Mathf.FloorToInt(TotalTime % 60);
            Timer.text= string.Format("{0:00}:{1:00}",Minute,Second);
        }
        else if (isThereTime)
        {
            EndGame(1);
        }    
    }

   private void EndGame(int Value)
    {
        GamePanels[Value].SetActive(true);
    }
    


}
