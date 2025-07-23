using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    public bool dieTile;
    public bool atkCool = false;

    private string tileName;
    public Define.TileType tileType;
    public float point; 
    private float hp;
    public float curHp;

    private string path;

    private MeshRenderer meshRenderer;
    public Tile GetSetTile(string name, Define.TileType type,  float hp)
    {
        tileName = name;
        tileType = type;
        this.hp = hp;
        curHp = hp;



        meshRenderer = GetComponent<MeshRenderer>();

        return this;
    }
    
    public void OnDamage(CreatureController creature, float damage)
    {
        if(atkCool)
            return;

        atkCool = true;
        curHp -= damage;
        CheckShader(curHp);

        StartCoroutine(CoolTime(0.1f));

        if (curHp <= 0)
            OnDie();
    }
    public void OnDie()
    {
        ChangeItemMode();
    }
    private void CheckShader(float exithp)
    {
        float persent = exithp / hp * 100;
        Debug.Log(persent);
        if (persent <= 20)
            ChangeMater(Manager.Resources.Load<Material>("Material/Cleave4"));
        else if (persent <= 40)
            ChangeMater(Manager.Resources.Load<Material>("Material/Cleave3"));
        
        else if(persent <= 60)
            ChangeMater(Manager.Resources.Load<Material>("Material/Cleave2"));
        
        else if(persent <= 80)
            ChangeMater(Manager.Resources.Load<Material>("Material/Cleave1"));
    }
    public void SetDie()
    {
        dieTile = true;
    }

    private IEnumerator CoolTime(float time)
    {
        yield return new WaitForSeconds(time);
        atkCool = false;
    }
    private void ChangeMater(Material mat)
    {
        Material[] newMats = new Material[2];
        newMats[0] = meshRenderer.materials[0];
        newMats[1] = mat;

        meshRenderer.materials = newMats;
    }
    private void ResetMaterial()
    {
        Material[] newMats = new Material[1];
        newMats[0] = meshRenderer.materials[0];

        meshRenderer.materials = newMats;
    }
    private void ChangeItemMode()
    {
        transform.localScale = Vector3.one;

        gameObject.AddComponent<Rigidbody>();
        Item item = gameObject.AddComponent<Item>();
        item.SetType(tileType, tileName);

        //머터리얼 초기화
        ResetMaterial();

        Destroy(this);
    }
}