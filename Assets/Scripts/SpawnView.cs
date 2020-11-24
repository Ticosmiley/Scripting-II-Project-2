using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SpawnView : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI _name;
    [SerializeField] TextMeshProUGUI _attack;
    [SerializeField] TextMeshProUGUI _health;

    Creature _creature;

    private void Update()
    {
        if (_creature != null)
        {
            GetComponent<RectTransform>().position = Camera.main.WorldToScreenPoint(_creature.gameObject.transform.position);
            if (_creature.Selected)
            {
                transform.GetChild(0).gameObject.SetActive(true);
            }
            else
            {
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
    }

    public void Display(Creature creature)
    {
        _creature = creature;
        _name.text = creature.Name;
        _attack.text = "Attack: " + creature.Attack;
        _health.text = "Health: " + creature.CurrentHealth + "/" + creature.MaxHealth;

        _creature.OnTakeDamage += UpdateHealth;
    }

    public void UpdateHealth(Creature creature)
    {
        _health.text = "Health: " + creature.CurrentHealth + "/" + creature.MaxHealth;
    }
}
