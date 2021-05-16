using System.Collections;
using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(fileName = "Countdown", menuName = "Garden Guards/Countdown")]
public class Countdown: ScriptableObject
{

    public int initialValue;
    private int _currentValue;
    public UnityEvent<int> valueChanged;

    public IEnumerator Run()
    {
        _currentValue = initialValue;
        valueChanged.Invoke(_currentValue);
        while (_currentValue > 0)
        {
            yield return new WaitForSeconds(1);
            _currentValue--;
            valueChanged.Invoke(_currentValue);
        }
    }
}