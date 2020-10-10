using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class MS_OptionsManager : MonoBehaviour
{
    [SerializeField]
    private GameObject selectorPrefab;
    private GameObject selector;

    private int selectedIndex = 0;

    private void Start()
    {
        selector = Instantiate(selectorPrefab, transform.parent);
        selector.SetActive(false);
    }

    public void Navigate(Vector2 dir)
    {
        if (dir.y > 0)
            DecreaseSelectedIndex();
        else
            IncreaseSelectedIndex();

        UpdateSelectorPosition();
    }

    public void Submit()
    {
        transform.GetChild(selectedIndex).GetComponent<Button>().onClick.Invoke();
    }

    public void Cancel()
    {

    }

    private void UpdateSelectorPosition()
    {
        selector.transform.DOMove(transform.GetChild(selectedIndex).transform.position, 0.2f);
    }

    private void IncreaseSelectedIndex()
    {
        if (selectedIndex + 1 <= transform.childCount - 1)
            selectedIndex++;
        else
            selectedIndex = 0;
    }

    private void DecreaseSelectedIndex()
    {
        if (selectedIndex - 1 >= 0)
            selectedIndex--;
        else
            selectedIndex = transform.childCount - 1;
    }

    public void InitializeSelector()
    {
        selector.SetActive(true);
        selector.transform.position = transform.GetChild(0).transform.position;
    }
}
