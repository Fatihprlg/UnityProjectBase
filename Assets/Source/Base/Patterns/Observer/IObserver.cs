using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IObserver<T, U>
{
    public void Notify(T index, U value);
}
public interface IObserver<T>
{
    public void Notify(T value);
}
