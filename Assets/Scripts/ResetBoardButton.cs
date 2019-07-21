using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetBoardButton : MonoBehaviour
{
    public GameObject player;
    public GameObject gameBoard;
    public Transform resetTransform;
    private void OnMouseDown()
    {
        gameBoard.transform.position = resetTransform.position;
        Vector3 yPlusOne = new Vector3 (0f, .1f, 0f);
        player.transform.position = gameBoard.transform.position + yPlusOne;
        gameBoard.SetActive(true);
    }
}
