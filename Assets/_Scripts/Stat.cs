
using System;

[Serializable]
public class Stat {
    public int value;
    public int maxValue;
    public event Action onValueChange;
    public int GetValue() {
        return value;
    }
    public void IncreaseValue(int value) {
        this.value += value;
    }
    public void DecreaseValue(int value) {
        this.value -= value;
    }
}
