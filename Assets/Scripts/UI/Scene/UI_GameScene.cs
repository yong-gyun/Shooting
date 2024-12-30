using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class UI_GameScene : MonoBehaviour
{
    [SerializeField] private Slider _hpBar;
    [SerializeField] private Slider _fuelBar;
    [SerializeField] private TMP_Text _hpText;
    [SerializeField] private TMP_Text _fuelText;
    [SerializeField] private TMP_Text _timeText;
    [SerializeField] private TMP_Text _scoreText;
    
    private PlayerStatus _status = null;


    private void Update()
    {
        int m = (int) Managers.Stage.PlayTime / 60;
        int s = (int) Managers.Stage.PlayTime % 60;
        
        _timeText.text = string.Format("{0:00} : {1:00}", m, s);
        _scoreText.text = Managers.Stage.Score.ToString();

        if (Managers.Object.Player != null)
        {
            if (_status == null)
                _status = Managers.Object.Player.Status as PlayerStatus;

            _hpBar.value = (float)_status.Hp / _status.MaxHp;
            _fuelBar.value = (float)_status.Fuel / _status.MaxFuel;

            _hpText.text = $"{_status.Hp} / {_status.MaxHp}";
            _fuelText.text = $"{_status.Fuel} / {_status.MaxFuel}";
        }
    }
}
