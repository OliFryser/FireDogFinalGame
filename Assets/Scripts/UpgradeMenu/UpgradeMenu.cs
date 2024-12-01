using UnityEngine;
using UnityEngine.EventSystems;
using System;
using System.Collections; 
using System.Collections.Generic;

public class UpgradeMenu : MonoBehaviour
{
    private RoomManager _roomManager;

    [SerializeField]
    private UpgradeDisplay[] _displays = new UpgradeDisplay[3];

    [SerializeField]
    private Upgrade[] _upgrades = new Upgrade[11];

    private List<Upgrade> _selectedUpgrades;

    private void Awake()
    {
        _roomManager = FindAnyObjectByType<RoomManager>();
        _selectedUpgrades = new List<Upgrade>();
        
        for (int i = 0; i < _displays.Length; i++)
        {  
            
            while (_displays[i].Upgrade == null) {
            System.Random random = new System.Random();
            int val = random.Next(0,10);
            if (val <= 5){
                System.Random rnd = new System.Random();
                int innerVal = rnd.Next(0,5);
                if (!_selectedUpgrades.Contains(_upgrades[innerVal])){
                    _displays[i].Upgrade = _upgrades[innerVal];
                    _displays[i].AddOnClickListener(OnUpgradeSelected);
                    _selectedUpgrades.Add(_upgrades[innerVal]);
                } 
            }
            else if (val > 5 && val < 9 ){
                System.Random rnd = new System.Random();
                int innerVal = rnd.Next(5,9);
                if (!_selectedUpgrades.Contains(_upgrades[innerVal])){
                    _displays[i].Upgrade = _upgrades[innerVal];
                    _displays[i].AddOnClickListener(OnUpgradeSelected);
                    _selectedUpgrades.Add(_upgrades[innerVal]);
                } 
            }
            else {
                System.Random rnd = new System.Random();
                int innerVal = rnd.Next(9,11);
                if (!_selectedUpgrades.Contains(_upgrades[innerVal])){
                    _displays[i].Upgrade = _upgrades[innerVal];
                    _displays[i].AddOnClickListener(OnUpgradeSelected);
                    _selectedUpgrades.Add(_upgrades[innerVal]);
                }
            }
            }
            
        }
        }

    private void Update()
    {
        if (EventSystem.current.currentSelectedGameObject == null)
        {
            SelectFirstControl();
        }
    }

    public void Show()
    {
        gameObject.SetActive(true);
        SelectFirstControl();
    }

    private void SelectFirstControl()
    {
        _displays[0].SetSelected();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void OnUpgradeSelected()
    {
        _roomManager.CloseUpgradeMenu();
    }
}

    
