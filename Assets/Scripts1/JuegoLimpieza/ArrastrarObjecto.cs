using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrastrarObjecto : MonoBehaviour
{
        private Vector3 offset;
        private bool arrastrando = false;

        void OnMouseDown()
        {
            offset = transform.position - Camera.main.ScreenToWorldPoint(Input.mousePosition);
            arrastrando = true;
        }

        void OnMouseDrag()
        {
            if (arrastrando)
            {
                Vector3 pos = Camera.main.ScreenToWorldPoint(Input.mousePosition) + offset;
                pos.z = 0;
                transform.position = pos;
            }
        }

        void OnMouseUp()
        {
            arrastrando = false;
        }
    }