/* *
 * ===============================================
 * author      : Josh@win
 * e-mail      : shijun_z@163.com
 * create time : 2026年3月17 14:47
 * function    :
 * ===============================================
 * */

using System.Collections;
using System.Collections.Generic;
using App.Core.Master;
using UnityEngine;

public class TestClass : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        XRMaster.Instance.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
    }
}