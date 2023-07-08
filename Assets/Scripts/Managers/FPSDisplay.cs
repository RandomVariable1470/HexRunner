using UnityEngine;
using System.Collections;
using TMPro;
using System.Text;

public class FPSDisplay : MonoBehaviour
{
    public TextMeshProUGUI fpsText;

    private float pollingTime = 1f;
    private float time;
    private int frameCount;

    private StringBuilder stringBuilder;

    private void Awake()
    {
        stringBuilder = new StringBuilder();
    }

    private void Start()
    {
        StartCoroutine(UpdateFPS());
    }

    private IEnumerator UpdateFPS()
    {
        while (true)
        {
            yield return new WaitForSeconds(pollingTime);

            int frameRate = Mathf.RoundToInt((float)frameCount / pollingTime);
            stringBuilder.Clear();
            stringBuilder.Append(frameRate.ToString()).Append(" fps");
            fpsText.text = stringBuilder.ToString();

            frameCount = 0;
        }
    }

    private void Update()
    {
        time += Time.deltaTime;
        frameCount++;
    }
}
