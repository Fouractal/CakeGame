using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomColor : MonoBehaviour
{
    //자주 쓰는 Color를 해당 클래스에서 관리합니다.
    public static Dictionary<string, Color> colorDict = new Dictionary<string, Color>()
    {
        //map
        {"Red", new Color(1f, 0f, 0f)},
        {"Yellow", new Color(1f, 1f, 0f)},
        {"Blue", new Color(0f, 191/255f, 255/255f)},
        {"Orange", new Color(255/255f, 165/255f, 0f)},
        {"Green", new Color(50/255f, 205/255f, 50/255f)},
        {"Purple", new Color(186/255f, 85/255f, 211/255f)},
        {"Pink", new Color(255/255f, 20/255f, 147/255f)},

        {"White", new Color(1f, 1f, 1f)},
        {"Black", new Color(0f, 0f, 0f)}
    };

    public static Color GetColor(string name)
    {
        return colorDict[name];
    }
}
