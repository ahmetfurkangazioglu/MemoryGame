using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameControl : MonoBehaviour
{
    public Sprite DefaultSprite;
    GameObject FirstSprite;
    GameObject ButtonSprite;
    int SpriteNumber;
   public List<GameObject> Images;
    void Start()
    {
        SpriteNumber = -1;
    }

    public void TakeGameObject(GameObject MySprite)
    {
        ButtonSprite = MySprite;
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
            Destroy(ButtonSprite);
            Destroy(FirstSprite);
        }
        else
        {
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
}
