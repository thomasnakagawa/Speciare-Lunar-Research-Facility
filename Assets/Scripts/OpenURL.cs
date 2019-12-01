using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using OatsUtil;

[RequireComponent(typeof(Button))]
public class OpenURL : MonoBehaviour
{
    [SerializeField] private string URL = default;

    void Start()
    {
        this.RequireComponent<Button>().onClick.AddListener(() =>
        {
            //Application.OpenURL(URL);
            Application.ExternalEval("window.open(" + URL + ");");
        });
    }
}
