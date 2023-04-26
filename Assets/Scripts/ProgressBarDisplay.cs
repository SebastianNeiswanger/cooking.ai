using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ProgressBarDisplay : MonoBehaviour
{
    public Slider progressBar;
    public Transform trackObj;
    public float hoverHeight = 4f;
    public float progress = 0f;

    public void updateProgress(float newVal)
    {
        // update progress bar
        progress = newVal;
        progressBar.value = progress;

        // put progress bar above relevant object
        Vector3 pos = Camera.main.WorldToScreenPoint(trackObj.position);
        pos.y += hoverHeight;
        progressBar.transform.position = pos;
    }

    public void showBar()
    {
        progressBar.gameObject.SetActive(true);
    }

    public void hideBar()
    {
        progressBar.gameObject.SetActive(false);
    }

    // Start is called before the first frame update
    void Start()
    {
        hideBar();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
