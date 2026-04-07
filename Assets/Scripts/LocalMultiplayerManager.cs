using NUnit.Framework;
using UnityEngine;
using UnityEngine.InputSystem;
using System.Collections.Generic;
using UnityEditor;

public class LocalMultiplayerManager : MonoBehaviour
{


    public List<Sprite> playerSprites;
    public List<PlayerInput> players;

    public void OnPlayerJoined(PlayerInput player)
    {
        players.Add(player);

        SpriteRenderer sr = player.GetComponent<SpriteRenderer>();
        sr.sprite = playerSprites[player.playerIndex];

        LocalMultiplayerController controller = player.GetComponent<LocalMultiplayerController>();
        controller.manager = this;
    }

    public void PlayerAttacking(PlayerInput attackPlayer)
    {
        for (int i = 0; i < players.Count; i++)
        {
            if (attackPlayer == players[i]) continue;

            if (Vector2.Distance(attackPlayer.transform.position, players[i].transform.position) < 0.5)
            {
            Debug.Log("Player " + attackPlayer.playerIndex + " hit player " + players[i].playerIndex);
            }
        }
    }
}
            
