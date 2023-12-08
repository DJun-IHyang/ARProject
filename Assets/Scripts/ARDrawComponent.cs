using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ARDrawComponent : MonoBehaviour
{
    //생성한 센터포인트를 연동할 트랜스폼 변수
    public Transform _pivotPoint;
    //Prefab으로 저장했던 Line을 연동하기 위한 변수
    public GameObject _lineRendererprefabs;
    //현재 제작되서 사용될 LineRenderer 변수
    private LineRenderer _lineRendere;
    //추가 기능 구현을 위한 List형태 선언
    public List<LineRenderer> _lineList = new List<LineRenderer>();
    //실제로 라인오브젝트가 위치하고 있을 오브젝트 Pool변수를 선언
    public Transform _linePool;

    //해당 코드가 사용되고 있는지 확인 하는 변수
    public bool _use;
    //LineRenderer가 사용중인지 확인하는 변수
    public bool _startLine;

    public List<Color> _colorList = new List<Color>();
    public Color _nowColor;
    public int _colorNumber;
    public Image _colorButton;

    bool pickerOnOff = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(_use)
        {
            if(_startLine)
            {
                DrawLineContinue();
            }
        }
    }

    public void MakeLineRendere()
    {
        GameObject tLine = Instantiate(_lineRendererprefabs);
        tLine.transform.SetParent(_linePool);
        tLine.transform.position = Vector3.zero;
        tLine.transform.localScale = new Vector3(1, 1, 1);

        _lineRendere = tLine.GetComponent<LineRenderer>();
        _lineRendere.positionCount = 1;
        _lineRendere.SetPosition(0, _pivotPoint.position);

        _lineRendere.startColor = ColorPalette.Instance.selectedColor;
        _lineRendere.endColor = ColorPalette.Instance.selectedColor;

        _startLine = true;
        _lineList.Add(_lineRendere);
 
    }

    public void DrawLineContinue()
    {
        _lineRendere.positionCount = _lineRendere.positionCount + 1;
        _lineRendere.SetPosition(_lineRendere.positionCount - 1, _pivotPoint.position);
    }

    public void StartDrawLine()
    {
        _use = true;
        if(!_startLine)
        {
            MakeLineRendere();
        }
    }

    public void StopDrawLine()
    {
        _use = false;
        _startLine = false;
        _lineRendere = null;
    }

    public void ChangeColor()
    {
        if (ColorPalette.Instance.gameObject.activeSelf == false)
        {
            pickerOnOff = true;
            ColorPalette.Instance.gameObject.SetActive(true);

            //ColorPalette.Instance.linkedObject = this.gameObject;
        }
        else
        {
            /*if (ColorPalette.Instance.linkedObject.name != this.gameObject.name)
                return;*/
            pickerOnOff = false;
            ColorPalette.Instance.gameObject.SetActive(false);
            //ColorPalette.Instance.linkedObject = null;
        }

        //_colorNumber++;

        if(_colorNumber >= _colorList.Count) _colorNumber = 0;

        //ColorPalette.Instance.selectedColor = _colorList[_colorNumber];
        _colorButton.color = ColorPalette.Instance.selectedColor;
    }
}
