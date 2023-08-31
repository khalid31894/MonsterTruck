using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class Finsh_Gate : MonoBehaviour
{
    public Select_Trigger select_Trigger;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (select_Trigger == Select_Trigger.Finsh_Line)
        {
            if (collision.gameObject.CompareTag("CoinTrigger") /*&& gameObject.CompareTag("Finsh_Gate")*/)
            {
                Car_Handler.instance.controller.finishGateReached = true;
                Debug.Log("End Trigger");
                PaintSecUI.isPainted = false;
            }
        }
        if (select_Trigger == Select_Trigger.Reduce_Speed)
        {
            if (collision.gameObject.CompareTag("CoinTrigger"))
            {
                Car_Handler.instance.controller.TargetAchieved = true;
                Debug.Log("Reduce Speed Trigger");
            }
        }
        if (select_Trigger == Select_Trigger.Stop_Line)
        {
            if (collision.gameObject.CompareTag("CoinTrigger"))
            {
                Car_Handler.instance.controller.Stopcar = true;
            }
        }
    }
}

public enum Select_Trigger
{
    Finsh_Line,
    Stop_Line,
    Reduce_Speed
}
