using UnityEngine;

public class showCanvas : MonoBehaviour
{
    [SerializeField] GameObject[] _itemToShow;
    public void show()
    {
        for (byte index = 0; index < _itemToShow.Length; index++)
        {
            _itemToShow[index].SetActive(true);
        }
    }

    public void hide()
    {
        for (byte index = 0; index < _itemToShow.Length - 1; index++)
        {
            _itemToShow[index].SetActive(false);
        }
    }
}
