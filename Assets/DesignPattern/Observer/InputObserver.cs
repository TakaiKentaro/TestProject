using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using InputDataObservable = IObservable<InputObserver.InputData>;
using InputDataObserver = IObserver<InputObserver.InputData>;

/// <summary>
/// クラス説明
/// </summary>
public class InputObserver : InputDataObservable
{
    public enum InputType
    {
        Start,
        Move,
        Finish,
    }

    public class InputData
    {
        public InputType Type;
    }

    static InputData[] StableInputData =
    {
        new InputData(){Type =InputType.Start},
        new InputData(){Type =InputType.Move},
        new InputData(){Type =InputType.Finish},
    };

    List<InputDataObserver> Subscriber = new List<InputDataObserver>();

    public void AddObserver(InputDataObserver target)
    {
        Subscriber.Add(target);
    }

    public void DeleteObserver(InputDataObserver target)
    {
        Subscriber.Remove(target);
    }

    public void NotifyObserver(InputData data)
    {
        Subscriber.ForEach(s => s.NotifyUpdate(data));
    }

    static public InputData CreateInput(string ButtonName)
    {
        switch (ButtonName)
        {
            case "Start": return StableInputData[(int)InputType.Start];
            case "Move": return StableInputData[(int)InputType.Move];
            case "Finish": return StableInputData[(int)InputType.Finish];
            default: return null;
        }
    }
}
