using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PapersList : MonoBehaviour
{
    public Paper GetTopPaper()
    {
        return _papersList[_papersList.Count - 1];
    }

    [SerializeField] private List<Paper> _papersList;
}
