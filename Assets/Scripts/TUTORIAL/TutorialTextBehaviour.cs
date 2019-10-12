using System.Collections;
using UnityEngine;
using TMPro;
public class TutorialTextBehaviour : MonoBehaviour
{
    private bool isDissapearing = false;
    private TextMeshProUGUI text;
    private void Start()
    {
        text = GetComponent<TextMeshProUGUI>();
        PlayerStatContainer.Instance.isAlive = false;
    }
    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !isDissapearing)
        {
            PlayerStatContainer.Instance.isAlive = true;
            StartCoroutine(StartDissapear());
            isDissapearing = true;
        }
    }
    private IEnumerator StartDissapear()
    {
        float time = 2f;
        while (time > 0)
        {
            text.color = Color.Lerp(text.color, Color.clear, 0.12f);
            time -= Time.deltaTime;
            yield return null;
        }
        Destroy(gameObject);
    }
}
