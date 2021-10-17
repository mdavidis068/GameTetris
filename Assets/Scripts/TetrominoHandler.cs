using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TetrominoHandler : MonoBehaviour
{
    [SerializeField]
    private float fallSpeed = 1.0f;

    private float fall = 0.0f;

    private GameplayManager gameplayManager;

    private void Start()
    {
        gameplayManager = FindObjectOfType<GameplayManager>();
    }

    private void Update()
    {
        UpdateTetromino();
        InputKeyboardHandler();
    }

    private void UpdateTetromino()
    {
        if (Time.time - fall >= fallSpeed)
        {
            Handler("Down");
            fall = Time.time;
        }
    }

    private void InputKeyboardHandler()
    {
        if (Input.GetKeyDown(KeyCode.RightArrow))
            Handler("Right");
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
            Handler("Left");
        else if (Input.GetKeyDown(KeyCode.DownArrow))
            Handler("Down");
        else if (Input.GetKeyDown(KeyCode.UpArrow))
            Handler("Action");
    }

    private void Handler(string command)
    {
        switch (command)
        {
            case "Right":
                MoveHorizontal(Vector3.right);
                break;
            case "Left":
                MoveHorizontal(Vector3.left);
                break;
            case "Down":
                MoveVertical();
                break;
            case "Action":
                transform.Rotate(Vector3.forward * 90);
                break;
        }
    }

    private void MoveVertical()
    {
        transform.position += Vector3.down;

        if (!IsInValidPosition())
            transform.position += Vector3.up;
    }

    private void MoveHorizontal(Vector3 direction)
    {
        transform.position += direction;

        if (!IsInValidPosition())
            transform.position += direction * -1;
    }

    private bool IsInValidPosition()
    {
        foreach (Transform mino in transform)
        {
            Vector3 pos = gameplayManager.Round(mino.position);

            if (!gameplayManager.IsTetrominoInsideAGrid(pos))
                return false;
        }

        return true;
    }
}
