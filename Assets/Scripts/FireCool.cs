using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireCool : MonoBehaviour
{
    public Slider cool;
    public float cooldown = 0f;
    private Vector3 offset = new Vector3(1f, 0f, 0f);
    private Vector3 newPosition;
    private bool isCoolingDown = false;
    // Start is called before the first frame update
    void Start()
    {
        cooldown = GetComponentInParent<Bomber>().shootDelay;
        cool = GetComponentInChildren<Slider>();
        if (cool != null)
        {
            cool.transform.position = transform.position + offset;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isCoolingDown)
        {
            // Increase the cooldown based on time
            cooldown += Time.deltaTime;
            // Clamp the cooldown to the maximum value
            //cooldown = Mathf.Clamp(cooldown, 0f, maxCooldown);

            // Update the slider value
            float percentageFilled = cooldown;
            cool.value = percentageFilled;

            if (percentageFilled >= 1)
            {
                cool.fillRect.GetComponent<Image>().color = Color.blue;
                isCoolingDown = false; // Cooldown is complete
            }
        }

    }
}
