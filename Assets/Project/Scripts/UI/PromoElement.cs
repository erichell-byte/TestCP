using System;
using System.Collections;
using RedPanda.Project.Data;
using RedPanda.Project.Interfaces;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;


namespace RedPanda.Project.UI
{
    public class PromoElement : MonoBehaviour
    {
        [SerializeField]
        private Button button;

        [SerializeField] 
        private TextMeshProUGUI title;
        
        [SerializeField] 
        private TextMeshProUGUI price;
        
        [SerializeField]
        private Image icon;
        
        [Header("Rarity"), Space]
        [SerializeField] 
        private GameObject epicRarityImage;
        [SerializeField] 
        private GameObject rareRarityImage;
        [SerializeField] 
        private GameObject commonRarityImage;

        private Vector3 _originalScale;
        
        public void Init(IPromoModel data)
        {
            title.text = data.Title;
            price.text = "x" + data.Cost;
            SetIcon(data.GetIcon());
            SetRarity(data.Rarity);
            _originalScale = transform.localScale;
        }

        public void OnButtonClick(UnityAction buttonClickHandler)
        {
            button.onClick.AddListener(buttonClickHandler);
            button.onClick.AddListener(OnButtonClick);
        }

        private void SetIcon(string iconName)
        {
            icon.sprite = Resources.Load<Sprite>($"UI/Promo/{iconName}");
        }

        private void SetRarity(PromoRarity promoRarity)
        {
            epicRarityImage.SetActive(promoRarity == PromoRarity.Epic);
            rareRarityImage.SetActive(promoRarity == PromoRarity.Rare);
            commonRarityImage.SetActive(promoRarity == PromoRarity.Common);
        }
        
        public void OnButtonClick()
        {
            transform.localScale = _originalScale * 1.1f;
            
            StartCoroutine(ReturnToOriginalSize());
        }

        private IEnumerator ReturnToOriginalSize()
        {
            yield return new WaitForSeconds(0.1f);
            transform.localScale = _originalScale;
        }
    }
}