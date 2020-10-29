using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using TMPro;

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

    public void MSNavigate(Vector2 dir)
    {
        transform.GetChild(selectedIndex).GetComponentInChildren<TextMeshProUGUI>().color = ColorUtils.UnselectedButtonColor;

        if (dir.y > 0)
            DecreaseSelectedIndex();
        else
            IncreaseSelectedIndex();

        transform.GetChild(selectedIndex).GetComponentInChildren<TextMeshProUGUI>().color = ColorUtils.SelectedButtonColor;
        UpdateSelectorPosition();
    }

    public void Submit()
    {
        transform.GetChild(selectedIndex).GetComponent<Button>().onClick.Invoke();
    }

    public void Cancel()
    {

    }

    private void UpdateSelectorPosition(float dur = 0.2f)
    {
        Vector3 finalPos = transform.GetChild(selectedIndex).transform.position;
        //finalPos.x = -10f;
        selector.transform.DOMove(finalPos, dur);
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
        transform.GetChild(selectedIndex).GetComponentInChildren<TextMeshProUGUI>().color = ColorUtils.SelectedButtonColor;
        UpdateSelectorPosition(0f);
    }
}
