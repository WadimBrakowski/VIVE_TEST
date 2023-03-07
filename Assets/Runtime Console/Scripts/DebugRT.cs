using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace RuntimeConsole
{
    public static class DebugRT
    {
        //[SerializeField] private static TextMeshProUGUI debugTextField;
        private static string debugText;

        private static List<string> debugList;

        public static TextMeshProUGUI TextMeshProUGUI { get; set; }


        public static void Log(string text)
        {
            TextMeshProUGUI.text = text;
        }
    }
}
