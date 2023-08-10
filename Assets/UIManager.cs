using System.Collections;
using System.Collections.Generic;
using BaseTemplate.Behaviours;
using UnityEngine;
using TMPro;

public class UIManager : MonoSingleton<UIManager>
{
    [SerializeField] private TextMeshProUGUI _AmmoUI;

    public void ChangeAmmoText(string ammo)
    {
        _AmmoUI.text = ammo;
    }
}
