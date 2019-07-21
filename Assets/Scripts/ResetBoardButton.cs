using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoardButton : MonoBehaviour
{
    public GameObject gameBoard;
    public Transform resetTransform;
    private void OnMouseDown()
    {
        gameBoard.transform.position = resetTransform.position;
        gameBoard.SetActive(true);
    }
}
