using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class HPRemainTextBehaviour : MonoBehaviour
{
    public TextMeshProUGUI HPRemainText;
    public Slider HPBarSlider;
    public Image HPBarFillIMG;
    private void Start(){
        UpdateHPRemain();
    }
    private void Update(){
        //testing
        UpdateHPRemain();
    }
    public void UpdateHPRemain(){
        if(HPBarSlider.value > HPBarSlider.maxValue * 0.6f){
            HPBarFillIMG.color = Color.red;
        }
        else if(HPBarSlider.value > HPBarSlider.maxValue * 0.35f){
            HPBarFillIMG.color = Color.yellow;
        }
        else{
            HPBarFillIMG.color = Color.green;
        }
        HPRemainText.text = Mathf.RoundToInt(HPBarSlider.value) + " / " + Mathf.RoundToInt(HPBarSlider.maxValue);
    }
}
