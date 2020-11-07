using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerManaView : MonoBehaviour
{
    TextMeshProUGUI _text;

    private void Awake()
    {
        _text = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        _text.text = "Mana: " + Player.instance.currentMana + "/" + Player.instance.maxMana;
    }
}