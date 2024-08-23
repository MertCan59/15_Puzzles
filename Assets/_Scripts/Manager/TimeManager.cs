using TMPro;
using UnityEngine;

public class TimeManager : MonoBehaviour
{
    public int _seconds, _minutes;
    [SerializeField] private TextMeshProUGUI textMesh;
    private void Start()
    {
        IncreaseSecond();
    }

    private void IncreaseSecond()
    {
        _seconds++;
        if (_seconds > 59)
        {
            _minutes++;
            _seconds = 0;
        }

        textMesh.text = (_minutes<10?"0":"") + _minutes+ ":" + (_seconds<10?"0":"") + _seconds;
        Invoke(nameof(IncreaseSecond),1);
    }

    public void StopTimer()
    {
        CancelInvoke(nameof(IncreaseSecond));
    }
}
