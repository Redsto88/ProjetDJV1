using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class amongus : MonoBehaviour
{

    private Image _image;

    void OnEnable()
    {
        _image = GetComponent<Image>();
        StartCoroutine(Start());
        IEnumerator Start()
        {
            GameManager.Instance._meeting_stop = true;
            AudioManager.Instance.Play("amongus", 0.8f);
            _image.color = new Color(0, 0, 0, 1);
            while (_image.color.r < 1)
            {
                _image.color = new Color(_image.color.r + 0.03f, _image.color.g + 0.03f, _image.color.b + 0.03f, 1);
                yield return null;
            }
            yield return new WaitForSeconds(4);
            GameManager.Instance._meeting_stop = false;
            GameManager.Instance._player.GetComponent<PlayerController>().canmove = true;
            transform.parent.gameObject.SetActive(false);
        }

    }
}
