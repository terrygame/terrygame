using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class BossBloodBar : MonoBehaviour
{
    // Start is called before the first frame update
    public static BossBloodBar instance { get; private set; }
    public Image mask;
    float originalSize;
    // Start is called before the first frame update

    void Awake()
    {
        mask = GetComponent<Image>();
        instance = this;
        Debug.Log("Bossblood bar awake");
        originalSize = mask.rectTransform.rect.width;
    }
    void Start()
    {
        //Debug.Log("Health bar");


    }
    // Update is called once per frame

    public void SetValue(float value)
    {
        Debug.Log("change bossblood value " + value);
        mask.rectTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, originalSize * value);

    }
}
