using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinLoseScreen : MonoBehaviour
{
    public Image back;
    public Image Quit;
    public Image Restart;

    public Image Win;

    public Image Lose;

    void OnEnable()
    {
        StartCoroutine(Start());

        IEnumerator Start(){
            back.color = new Color(0,0,0,0);
            Quit.color = new Color(1,1,1,0);
            Restart.color = new Color(1,1,1,0);
            Win.color = new Color(1,1,1,0);
            Lose.color = new Color(1,1,1,0);
            while (back.color.a < 1)
            {
                if(GameManager.Instance._win)
                    Win.color = new Color(1,1,1,Win.color.a + 0.01f);
                else
                    Lose.color = new Color(1,1,1,Lose.color.a + 0.01f);
                back.color = new Color(0,0,0,back.color.a + 0.01f);
                Quit.color = new Color(1,1,1,Quit.color.a + 0.01f);
                Restart.color = new Color(1,1,1,Restart.color.a + 0.01f);
                yield return new WaitForSeconds(0.01f);
            }

        }
    }

    public void RestartGame(){
        GameManager.Instance.Restart();
    }


}
