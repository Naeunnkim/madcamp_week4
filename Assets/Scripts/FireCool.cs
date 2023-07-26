using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCool : MonoBehaviour
{
    public Scrollbar scrollbar;
    public float cooldown = 0;
    private Image scrollbarImage;
    private Vector3 offset = new Vector3(1f, 0f, 0f);
    private Vector3 newPosition;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = GetComponentInParent<Bomber>().shootDelay;
        scrollbarImage = scrollbar.GetComponent<Image>();
        newPosition = transform.position + offset;
        scrollbar.transform.position = newPosition;
    }

    // Update is called once per frame
    void Update()
    {
        float percentageFilled = cooldown / 100f;
        scrollbar.value = percentageFilled;

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
