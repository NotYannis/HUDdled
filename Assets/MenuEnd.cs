using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuEnd : MonoBehaviour {
    public Text youWhat;
    public Image youLoose;
    public Image youWin;
    private Image imgEverywhere;

    public GameObject wizard;

    public bool win;
    private List<GameObject> youWhatPanels;

    void OnEnable()
    {
        if (win)
        {
            youWhat.text = "You Win !";
            imgEverywhere = youWin;
        }
        else
        {
            imgEverywhere = youLoose;
        }
        wizard.SetActive(false);

        youWhatPanels = new List<GameObject>(100);
        StartCoroutine(ImageEverywhere());
    }

    public void Restart()
    {
        SceneManager.LoadScene("mainScene");
    }

    public void Quit()
    {
        Application.Quit();
    }

    IEnumerator ImageEverywhere()
    {
        while (true)
        {
            Image img = Instantiate(imgEverywhere, GameObject.Find("Looseeeer").transform);
            Vector2 minBounds = GetComponent<Image>().rectTransform.rect.min;
            Vector2 maxBounds = GetComponent<Image>().rectTransform.rect.max;

            Vector2 pos = new Vector2(Random.Range(minBounds.x, maxBounds.x), Random.Range(minBounds.y, maxBounds.y));

            img.rectTransform.position = pos;
            img.rectTransform.Rotate(new Vector3(0.0f, 0.0f, Random.Range(-50.0f, 50.0f)));

            Text looseText = img.GetComponentInChildren<Text>();
            Color randColor = new Color(Random.value, Random.value, Random.value);
            looseText.color = randColor;

            if (win)
            {
                ParticleSystem.MainModule winParts = img.GetComponentInChildren<ParticleSystem>().main;
                winParts.startColor = randColor;
            }

            youWhatPanels.Add(img.gameObject);
            if(youWhatPanels.Count >= 100)
            {
                Destroy(youWhatPanels[0]);
                youWhatPanels.RemoveAt(0);
            }

            yield return new WaitForSeconds(Random.Range(0.1f, 0.5f));
        }
    }
}
