using UnityEngine;

public class SetupFastMove : MonoBehaviour
{
    [SerializeField] private GameObject _content;
    [Space(5)]

    [SerializeField] private GameObject _contentParent;
    [SerializeField] private GameObject _answerBlock;

    private House[] _houses;

    private Human _currentHuman;

    private void Awake()
    {
        _houses = FindObjectsOfType<House>();
    }

    public void Set(Answer[] answers)
    {
        foreach (Transform child in _contentParent.transform)
            Destroy(child.gameObject);

        for (int i = 0; i < answers.Length; i++)
        {
            SetupAnswerBlock block = Instantiate(_answerBlock, 
                                                 _contentParent.transform.position, 
                                                 Quaternion.identity, 
                                                 _contentParent.transform).GetComponent<SetupAnswerBlock>();

            block.SetInfo(i + 1, answers[i].Content);
        }
    }

    public void Open(Human human)
    {
        _currentHuman = human;
        _content.SetActive(true);
    }

    public void TransferToHouse(int index)
    {
        for (int i = 0; i < _houses.Length; i++)
        {
            if (_houses[i].GetIndex == index) 
            {
                _houses[i].AddHuman(_currentHuman);

                _currentHuman.gameObject.SetActive(false);
                LevelController.Instance.CurrentHuman = null;

                Close();
            }
        }        
    }

    public void Close()
    {
        _currentHuman = null;
        _content.SetActive(false);
    }
}
