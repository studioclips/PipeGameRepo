using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using static GotoSceneManager;

public class PipeView : MonoBehaviour
{
    public enum ArmType
    {
        SArm = 0,
        LArm = 1,
        IArm = 2,
        TArm = 3,
        EArm = 4,
        None = 5,
    }
    //  道を示す腕のカラー変更用
    [SerializeField] private List<SpriteRenderer> _armImages = new List<SpriteRenderer>();
    [SerializeField] private ArmType _armType = ArmType.LArm;
    private List<bool> _arms = new List<bool>() { false, false, false, false };
    private Dictionary<ArmType, List<bool>> _armTypes = new Dictionary<ArmType, List<bool>>()
    {
        {ArmType.LArm, new List<bool>(){true, true, false, false } },
        {ArmType.IArm, new List<bool>(){true, false, true, false } },
        {ArmType.TArm, new List<bool>(){true, true, true, false } },
    };
    private int _rotatePos = 0;
    private readonly int _rotateMax = 4;

    private float _startAngle = 0f;
    private float _endAngle = 0f;

    public bool IsAction { get; private set; } = false;

    // Start is called before the first frame update
    void Start()
    {
        foreach (var armStat in _armTypes[_armType].Select((arm, index) => new {arm, index}))
        {
            _arms[armStat.index] = armStat.arm;
        }
    }

    public void SetRotate(bool isLeft)
    {
        IsAction = true;
        _startAngle = _rotatePos * 90f;
        if (isLeft)
            _rotatePos++;
        else
            _rotatePos += _rotateMax - 1;
        _endAngle = _rotatePos * 90f;
        _rotatePos %= _rotateMax;
        StartCoroutine(RotateAction());
    }

    private IEnumerator RotateAction()
    {
        var timer = 0f;
        while (timer < 1f)
        {
            timer += Time.deltaTime;
            if (timer > 1f) timer = 1f;
            var rotate = Mathf.Lerp(_startAngle, _endAngle, timer);
            transform.localRotation = Quaternion.Euler(0, 0, rotate);
            yield return null;
        }
        IsAction = false;
    }

    public bool IsConnect (PipeArmDirection pipeArmDirection)
    {
        return _arms[(int)pipeArmDirection];
    }
}
