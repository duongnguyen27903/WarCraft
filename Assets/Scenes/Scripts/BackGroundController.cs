using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundController : MonoBehaviour
{
    [SerializeField] private Material NebulaBg;
    [SerializeField] private Material BigStar_Bg;
    [SerializeField] private Material MediumStar_Bg;
    [SerializeField] private float Nebula_scroll_speed;
    [SerializeField] private float BigStar_scroll_speed;
    [SerializeField] private float MediumStar_scroll_speed;

    private int MainTexID;
    void Start()
    {
        MainTexID = Shader.PropertyToID("_MainTex");
    }

    void Update()
    {
        Vector2 offset = BigStar_Bg.GetTextureOffset(MainTexID);
        offset += new Vector2(0,BigStar_scroll_speed * Time.deltaTime);
        BigStar_Bg.SetTextureOffset(MainTexID, offset);

        offset = MediumStar_Bg.GetTextureOffset(MainTexID);
        offset += new Vector2(0, MediumStar_scroll_speed * Time.deltaTime);
        MediumStar_Bg.SetTextureOffset(MainTexID, offset);

        offset = NebulaBg.GetTextureOffset(MainTexID);
        offset += new Vector2(0, Nebula_scroll_speed * Time.deltaTime);
        NebulaBg.SetTextureOffset(MainTexID, offset);
    }
}
