﻿using System.Collections;
using Extensions;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tools.UI.Card
{
    public class UiPlayerHandUtils : MonoBehaviour
    {
        //--------------------------------------------------------------------------------------------------------------

        #region Fields

        private int Count { get; set; }

        [SerializeField] [Tooltip("Prefab of the Card C#")]
        private GameObject cardPrefabCs;

        [SerializeField] [Tooltip("World point where the deck is positioned")]
        private Transform deckPosition;

        [SerializeField] [Tooltip("Game view transform")]
        private Transform gameView;

        private IUiPlayerHand PlayerHand { get; set; }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Unitycallbacks

        private void Awake()
        {
            PlayerHand = transform.parent.GetComponentInChildren<IUiPlayerHand>();
        }

        private IEnumerator Start()
        {
            //starting cards
            for (var i = 0; i < 5; i++)
            {
                yield return new WaitForSeconds(0.2f);
                DrawCard();
            }
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------

        #region Operations

        [Button]
        public void DrawCard()
        {
            //TODO: Consider replace Instantiate by an Object Pool Pattern
            var cardGo = Instantiate(cardPrefabCs, gameView);
            cardGo.name = "Card_" + Count;
            var card = cardGo.GetComponent<IUiCard>();
            card.transform.position = deckPosition.position;
            Count++;
            PlayerHand.AddCard(card);
        }

        [Button]
        public void PlayCard()
        {
            if (PlayerHand.Cards.Count > 0)
            {
                var randomCard = PlayerHand.Cards.RandomItem();
                PlayerHand.PlayCard(randomCard);
            }
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Tab)) DrawCard();
            if (Input.GetKeyDown(KeyCode.Space)) PlayCard();
            if (Input.GetKeyDown(KeyCode.Escape)) Restart();
        }

        public void Restart()
        {
            SceneManager.LoadScene(0);
        }

        #endregion

        //--------------------------------------------------------------------------------------------------------------
    }
}