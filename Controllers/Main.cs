using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GeekBrains.Controllers.lesson3
{
    public class Main : MonoBehaviour
    {
        [SerializeField] public LayerMask ClickMask;
        [SerializeField] public Material MaterialLine;

        private GameObject _allControllers;

        public static Main Instance { get; private set; }

        public ClickController ClickController { get; private set; }

        public UiController UiController { get; set; }


        private void Awake()
        {
            if (!Instance)
            {
                Instance = this;

                _allControllers = new GameObject("All Controllers");

                ClickController = _allControllers.AddComponent<ClickController>();
                UiController = _allControllers.AddComponent<UiController>();
                _allControllers.AddComponent<ViewcameraController>();

                ClickController.ClickMask = ClickMask;
                ClickController.MaterialLine = MaterialLine;
            }
        }
        
    }
}
