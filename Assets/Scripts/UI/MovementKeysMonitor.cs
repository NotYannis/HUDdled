using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MovementKeysMonitor : FillBarMonitor {
    public KeyCode otherKey;
    public SpriteRenderer otherKeyFeedback;
    public GameObject onOtherKey;

    public GameObject shakeRepair;

    public Coroutine imageShakingRepair;
    // Use this for initialization
    void Start()
    {
        keyPressedFeedback = Resources.Load<Sprite>("GRAPH/UI/keyPressed");
        keyReleasedFeedback = Resources.Load<Sprite>("GRAPH/UI/keyReleased");
    }

    // Update is called once per frame
    void LateUpdate () {
        if (Input.GetKeyDown(otherKey))
        {
            OnOtherKeyPress();
        }
        if (Input.GetKeyUp(otherKey))
        {
            OnOtherKeyReleased();
        }
    }

    public void ShakeButtonRepair()
    {
        if (imageShakingRepair != null)
        {
            StopCoroutine(imageShakingRepair);
            shakeRepair.transform.rotation = Quaternion.identity;
        }

        imageShakingRepair = StartCoroutine(ShakeImg(shakeRepair));
    }

    public void OnOtherKeyPress()
    {
        otherKeyFeedback.sprite = keyPressedFeedback;
        onOtherKey.transform.localPosition -= new Vector3(0.0f, positionPressed);
        onOtherKey.GetComponentInChildren<Text>().color = Color.red;
    }

    public void OnOtherKeyReleased()
    {
        otherKeyFeedback.sprite = keyReleasedFeedback;
        onOtherKey.transform.localPosition += new Vector3(0.0f, positionPressed);
        onOtherKey.GetComponentInChildren<Text>().color = Color.yellow;
    }
}
