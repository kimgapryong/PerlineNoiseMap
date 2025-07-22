using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreatureController : BaseController
{
    public CreatureData _data;
    private Define.State _state;
    public Define.State State
    {
        get { return _state; }
        set
        {
            _state = value;
            AnimateChange(value);
        }
    }

    public Action<float, float> hpAction;
    public Rigidbody rb;
    public Collider coll;
    private Coroutine _coroutine;
    public float MaxHp { get; private set; }
    public float CurHp { get { return CurHp; } set { CurHp = value; hpAction.Invoke(value, MaxHp); } }
    public float Speed { get; private set; }
    public float Damage {  get; private set; }  
    public float Defence { get; private set; }

    protected bool jumpCool;
    protected bool attackCool;

    public override bool Init()
    {
        if(base.Init() == false)
            return false;
        
        State = Define.State.Idle;
        rb = GetComponent<Rigidbody>();
        coll = GetComponent<Collider>();

        MaxHp = _data.hp;
        CurHp = MaxHp;
        Speed = _data.speed;
        Damage = _data.damage;
        Defence = _data.defence;

        return true;
    }

    protected virtual void UpdateMethod()
    {
        switch (_state)
        {
            case Define.State.Walk:
                Walk();
                break;
            case Define.State.Run:
                Run();
                break;
            case Define.State.Dig:
                Dig();
                break;
            case Define.State.Idle:
                Idle();
                break;
            case Define.State.Attack:
                Attack();
                break;
            case Define.State.Jump:
                Jump();
                break;
        }
    }

    protected virtual void Update()
    {
        UpdateMethod();
    }

    protected virtual void Walk() { }
    protected virtual void Run() { }
    protected virtual void Dig() { }
    protected virtual void Idle() { }
    protected virtual void Attack() { }
    protected virtual void Jump() { }

    protected virtual void AnimateChange(Define.State state) { }
    protected virtual void OnDamage(CreatureController attker, float dmamage)
    {
        CurHp -= dmamage;

        if (CurHp <= 0)
            OnDie();
    }
    protected virtual void OnDie()
    {
        Debug.Log("µÚÁ®¶ó ´×°Õ");
    }

    public void WaitCool(float time, Action onComplete)
    {
        if (_coroutine == null)
            _coroutine = StartCoroutine(Wait(time, onComplete));
    }

    private IEnumerator Wait(float time, Action onComplete)
    {
        yield return new WaitForSeconds(time);
        _coroutine = null;  
        onComplete?.Invoke();
    }
}
