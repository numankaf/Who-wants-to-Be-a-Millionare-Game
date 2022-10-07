using System.Collections;
using System.Collections.Generic;
using System.Runtime.ExceptionServices;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
public class PhoneCall : MonoBehaviour
{
    public Button phone;
    public Sprite deadPhone;
    public GameObject call;
    private bool pr=true;
    public void calling()
    {
        call.SetActive(pr);
        pr = !pr;

    }
}
