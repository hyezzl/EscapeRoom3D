using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemUIDGenerator : MonoBehaviour
{
    private static int currentUID = 0;

    public static int Generate() {
        return currentUID++;
    }

    // �������� ���� �ٽ� �ҷ�����
    public static void SetStartValue(int loadUID) {
        currentUID = loadUID;
    }
}
