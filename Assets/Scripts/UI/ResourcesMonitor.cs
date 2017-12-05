using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourcesMonitor : MonoBehaviour {
    public int minAmount = 0;
    public int maxAmount;
    public int currentAmount;

    public bool isEmpty;
    public bool isFull;

    public GameObject toShakeIfEmpty;
    public GameObject toShakeIfFull;
    public KeyCode key;
    public SpriteRenderer keyFeedback;
    public GameObject onKey;
    public float positionPressed = 10.0f;
    protected Sprite keyReleasedFeedback;
    protected Sprite keyPressedFeedback;

    Coroutine imageShakingEmpty;
    Coroutine imageShakingFull;
    void Start()
    {
    }

	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown(key))
        {
            OnKeyPress();
        }
        if (Input.GetKeyUp(key))
        {
            OnKeyReleased();
        }
	}

    public void UpdateAmount(int amount)
    {
        currentAmount = amount;
        if(currentAmount <= minAmount)
        {
            currentAmount = minAmount;
            isEmpty = true;

        }
        else if(currentAmount >= maxAmount)
        {
            currentAmount = maxAmount;
        }
        UpdateUI();
    }

    public virtual void UpdateUI() { }
    public virtual void EmptyFeedback() { }
    public virtual void FullFeedback() { }

    public virtual void OnKeyPress() {
        keyFeedback.sprite = keyPressedFeedback;
        onKey.transform.localPosition -= new Vector3(0.0f, positionPressed);
        onKey.GetComponentInChildren<Text>().color = Color.red;
    }

    public virtual void OnKeyReleased() {
        keyFeedback.sprite = keyReleasedFeedback;
        onKey.transform.localPosition += new Vector3(0.0f, positionPressed);
        onKey.GetComponentInChildren<Text>().color = Color.yellow;
    }

    public void ShakeButtonEmpty()
    {
        if (imageShakingEmpty != null)
        {
            StopCoroutine(imageShakingEmpty);
            toShakeIfEmpty.transform.rotation = Quaternion.identity;
        }

        imageShakingEmpty = StartCoroutine(ShakeImg(toShakeIfEmpty));
    }

    public void ShakeButtonFull()
    {
        if (imageShakingFull != null)
        {
            StopCoroutine(imageShakingFull);
            toShakeIfFull.transform.rotation = Quaternion.identity;
        }

        imageShakingFull = StartCoroutine(ShakeImg(toShakeIfFull));
    }

    protected IEnumerator ShakeImg(GameObject img)
    {
        Quaternion baseRotation = img.transform.rotation;
        float zRotation = 0.15f;
        float rotationTime = 0.0f;
        float waitDuration = 0.025f;
        Quaternion randomRotation;

        while (rotationTime < 0.2f)
        {
            rotationTime += Time.deltaTime;

            randomRotation = new Quaternion(0.0f, 0.0f, zRotation, 1.0f);
            img.transform.rotation = baseRotation * randomRotation;

            yield return new WaitForSeconds(waitDuration);
            rotationTime += waitDuration;

            randomRotation = new Quaternion(0.0f, 0.0f, -zRotation, 1.0f);
            img.transform.rotation = baseRotation * randomRotation;

            yield return new WaitForSeconds(waitDuration);
            rotationTime += waitDuration;

            zRotation -= 0.03f;
        }

        img.transform.rotation = baseRotation;

        yield return null;
    }
}
