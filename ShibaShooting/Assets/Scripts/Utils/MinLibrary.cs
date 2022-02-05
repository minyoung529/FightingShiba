using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

namespace MinLibrary
{
    public class MinConvert
    {
        public static List<string> TextAssetToStringList(TextAsset textAsset, params char[] seperator)
        {
            return textAsset.ToString().Split(seperator).ToList();
        }
    }
}
