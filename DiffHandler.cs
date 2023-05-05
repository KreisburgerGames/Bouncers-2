using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiffHandler : MonoBehaviour
{
    public void SetDifficulty(string difficulty)
    {
        PlayerPrefs.SetString("diff", difficulty);
    }
}
