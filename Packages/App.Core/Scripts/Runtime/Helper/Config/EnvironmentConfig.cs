/* *
 * ===============================================
 * author      : Josh@win
 * e-mail      : shijun_z@163.com
 * create time : 2026年3月16 14:51
 * function    : 
 * ===============================================
 * */

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace  App.Runtime.Helper
{
    [CreateAssetMenu(fileName = "EnvironmentConfig", menuName = "App/EnvironmentConfig")]
    [Serializable]
    public class EnvironmentConfig : ScriptableObject
    {
        public DevelopmentEnvironment Test;
        public DevelopmentEnvironment Sandbox;
        public DevelopmentEnvironment Release;
        public DevelopmentEnvironment Local;
    }
    
    [Serializable]
    public class DevelopmentEnvironment
    {
        public string HttpServer;
        public string SocketServer;
        public string CdnServer;
        public int SocketPort;
    }
}
