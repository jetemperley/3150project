using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FillHealthBar : MonoBehaviour
{
    private Slider slider;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void UpdateBar(int currentHealth, int maxHealth){
        slider.value = (float)currentHealth/(float)maxHealth;
    }
}
