using System;
using RedPanda.Project.Services.Interfaces;
using TMPro;
using UnityEngine;

namespace RedPanda.Project.UI
{
    public class Wallet : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI text;

        private IUserService _userService;
        
        public void Init(IUserService userService)
        {
            _userService = userService;
            _userService.OnCurrencyChanged += OnCurrencyChanged;
        }

        private void OnDisable()
        {
            _userService.OnCurrencyChanged -= OnCurrencyChanged;
        }

        private void OnCurrencyChanged(int newCurrency)
        {
            text.text = newCurrency.ToString();
        }
    }
}