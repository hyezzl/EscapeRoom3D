using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIDGenerator : MonoBehaviour
{
    private static int currentUID = 0;

    public static int Generate() {
        return currentUID++;
    }

    // 게임종료 이후 다시 불러오기
    public static void SetStartValue(int loadUID) {
        currentUID = loadUID;
    }
}
