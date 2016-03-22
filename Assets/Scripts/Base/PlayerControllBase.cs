using UnityEngine;
using System.Collections;
using Controlls;

namespace Controlls
{
    public class PlayerControllBase : MonoBehaviour
    {
        public static PlayerCommands Player1Settings()
        {
            return new PlayerCommands(KeyCode.A, KeyCode.D, KeyCode.W, KeyCode.S, KeyCode.C,KeyCode.V,KeyCode.B);
        }

        public static PlayerCommands Player2Settings()
        {
            return new PlayerCommands(KeyCode.LeftArrow, KeyCode.RightArrow, KeyCode.UpArrow,KeyCode.DownArrow, KeyCode.Comma,KeyCode.Period, KeyCode.Slash);
        }
    }

  
}
[System.Serializable]
public class PlayerCommands
{
    public KeyCode left;
    public KeyCode right;
    public KeyCode up;
    public KeyCode down;

    public KeyCode punchAttack;
    public KeyCode kickAttack;
    public KeyCode kneeAttack;

    public PlayerCommands(KeyCode Left, KeyCode Right, KeyCode Up,KeyCode Down, KeyCode PunchAttack,KeyCode KickAttack,KeyCode KneeAttack)
    {
        this.left = Left;
        this.right = Right;
        this.up = Up;
        this.down = Down;

        this.punchAttack = PunchAttack;
        this.kickAttack = KickAttack;
        this.kneeAttack = KneeAttack;
    }

}