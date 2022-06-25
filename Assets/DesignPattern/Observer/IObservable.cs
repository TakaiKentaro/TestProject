using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/// <summary>
/// 通知する
/// </summary>
public interface IObservable<T>
{
    void AddObserver(IObserver<T> target);
    void DeleteObserver(IObserver<T> target);
    void NotifyObserver(T obj);
}