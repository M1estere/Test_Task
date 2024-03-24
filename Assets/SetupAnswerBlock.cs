using System;
using UnityEngine;
using UnityEngine.UI;

public class SetupAnswerBlock : MonoBehaviour
{
    [SerializeField] private TMPro.TMP_Text _indexText;
    [SerializeField] private TMPro.TMP_Text _answerText;

    [SerializeField] private Button _button;

    public void SetInfo(int index, string answer)
    {
        _indexText.SetText(index.ToString());
        _answerText.SetText(answer);

        _button.onClick.AddListener( () => FindObjectOfType<SetupFastMove>().TransferToHouse(Convert.ToInt32(_indexText.text) - 1) );
    }
}
