using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCool : MonoBehaviour
{
    public RectTransform scrollbar;
    public float cooldown = 0;
    private Image scrollbarImage;
    private Vector3 objectInitialPosition;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = GetComponentInParent<Bomber>().shootDelay;
        scrollbarImage = scrollbar.GetComponent<Image>();
        objectInitialPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        float percentageFilled = cooldown / 100f;
        float newWidth = percentageFilled * scrollbar.rect.width;
        scrollbar.sizeDelta = new Vector2(newWidth, scrollbar.sizeDelta.y);

        if (percentageFilled == 1)
        {
            scrollbarImage.color = Color.red;
        }
        else
        {
            // Otherwise, change the color to blue
            scrollbarImage.color = Color.blue;
        }
    }
}
