using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISubject<T, U>
{
    public List<IObserver<T,U>> Observers { get; set; }
    public void NotifyObservers(T index, U value);
    public void RegisterObserver(IObserver<T, U> observer);
    public void UnRegisterObserver(IObserver<T, U> observer);
}

public interface ISubject<T>
{
    public List<IObserver<T>> Observers { get; set; }
    public void NotifyObservers(T value);
    public void RegisterObserver(IObserver<T> observer);
    public void UnRegisterObserver(IObserver<T> observer);
}