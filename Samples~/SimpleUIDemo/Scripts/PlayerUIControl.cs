using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace POC
{
    public class PlayerUIControl : MonoBehaviour
    {
        [SerializeField]
        private KeyCode uiKey = KeyCode.A;
        [SerializeField]
        private GameObject ui;
  

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKey(uiKey))
            {
                ui.SetActive(true);
            }
        }
    }
}
