using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 通知を発行する
/// </summary>
/// <typeparam name="T"></typeparam>
public interface IObservable<T>
{
    void AddObserver(IObserver<T> target);
    void DeleteObserver(IObserver<T> target);
    void NotifyObserver(T obj);
}