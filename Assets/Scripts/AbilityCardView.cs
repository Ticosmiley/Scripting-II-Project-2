using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityCardView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _nameTextUI = null;
    [SerializeField] TextMeshProUGUI _costTextUI = null;
    [SerializeField] TextMeshProUGUI _descriptionTextUI = null;
    [SerializeField] Image _graphicUI = null;

    Transform _object;

    private void Update()
    {
        if (_object != null)
        {
            GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(_object.transform.position);
            if (_object.gameObject.GetComponent<CardObject>().Selected)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void Display(AbilityCard abilityCard, Transform transform)
    {
        _object = transform;
        _nameTextUI.text = abilityCard.Name;
        _costTextUI.text = abilityCard.Cost.ToString();
        _graphicUI.sprite = abilityCard.Graphic;
        _descriptionTextUI.text = abilityCard.Description;
    }
}
