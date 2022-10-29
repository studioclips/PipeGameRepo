using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class GotoSceneManager : MonoBehaviour
{
    public enum PipeArmDirection
    {
        Up = 0,
        Right = 1,
        Down = 2,
        Left = 3,
    }
    [SerializeField] Transform _parentContent = null;
    [SerializeField] List<GameObject> _armPrefabs = new List<GameObject>();

    private List<List<PipeView.ArmType>> _map = new List<List<PipeView.ArmType>>()
    {
        new List<PipeView.ArmType>(){PipeView.ArmType.EArm, PipeView.ArmType.None, PipeView.ArmType.None, PipeView.ArmType.None, PipeView.ArmType.None},
        new List<PipeView.ArmType>(){PipeView.ArmType.LArm, PipeView.ArmType.TArm, PipeView.ArmType.IArm, PipeView.ArmType.LArm, PipeView.ArmType.None},
        new List<PipeView.ArmType>(){PipeView.ArmType.LArm, PipeView.ArmType.TArm, PipeView.ArmType.LArm, PipeView.ArmType.None, PipeView.ArmType.None},
        new List<PipeView.ArmType>(){PipeView.ArmType.IArm, PipeView.ArmType.LArm, PipeView.ArmType.LArm, PipeView.ArmType.IArm, PipeView.ArmType.LArm},
        new List<PipeView.ArmType>(){PipeView.ArmType.LArm, PipeView.ArmType.IArm, PipeView.ArmType.IArm, PipeView.ArmType.None, PipeView.ArmType.SArm},
    };

    private List<PipeView> _pipeViewList = new List<PipeView>();
    // Start is called before the first frame update
    void Start()
    {
        DispMap();
    }

    private void DispMap()
    {
        foreach (int y in Enumerable.Range(0, 5))
        {
            foreach(int x in Enumerable.Range(0, 5))
            {
                var mapNum = _map[y][x];
                if (PipeView.ArmType.None == mapNum)
                    continue;
                var pipe =  Instantiate(_armPrefabs[(int)mapNum], _parentContent);
                pipe.transform.localPosition = new Vector3(-400 + x * 200, 400 - y * 200, 0);
                _pipeViewList.Add(pipe.GetComponent<PipeView>());
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            _pipeViewList[1].GetComponent<PipeView>().SetRotate(true);
        }
    }
}
