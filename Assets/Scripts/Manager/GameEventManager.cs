using System;
using System.Collections.Generic;
using Unilts.Tools.Singleton;
using UnityEngine;

namespace Manager
{
     public class GameEventManager: SingletonNonMono<GameEventManager>
     {
          private interface IEventHelp
          {
          
          }

          private class EventHelp : IEventHelp
          {
               private event Action Action;

               public EventHelp(Action action)
               {
                    Action = action;
               }

               public void AddCall(Action action)
               {
                    Action += action;
               }

               public void Call()
               {
                    Action?.Invoke();
               }

               public void Remove(Action action)
               {
                    Action -= action;
               }
          }

          private class EventHelp<T> : IEventHelp
          {
               private event Action<T> Action;

               public EventHelp(Action<T> action)
               {
                    Action = action;
               }

               public void AddCall(Action<T> action)
               {
                    Action += action;
               }

               public void Call(T value)
               {
                    Action?.Invoke(value);
               }

               public void Remove(Action<T> action)
               {
                    Action -= action;
               }
          }
          
          private class EventHelp<T1 , T2> : IEventHelp
          {
               private event Action<T1 , T2> Action;

               public EventHelp(Action<T1 , T2> action)
               {
                    Action = action;
               }

               public void AddCall(Action<T1 , T2> action)
               {
                    Action += action;
               }

               public void Call(T1 value1, T2 value2)
               {
                    Action?.Invoke(value1 , value2);
               }

               public void Remove(Action<T1 , T2> action)
               {
                    Action -= action;
               }
          }

          private Dictionary<string, IEventHelp> _eventCenter = new Dictionary<string, IEventHelp>();

          /// <summary>
          /// 添加事件
          /// </summary>
          /// <param name="eventName"></param>
          /// <param name="action"></param>
          public void AddEventListening(string eventName , Action action)
          {
               if (_eventCenter.TryGetValue(eventName, out var e))
               {
                    (e as EventHelp)?.AddCall(action);
               }
               else
               {
                    _eventCenter.Add(eventName , new EventHelp(action));
               }
          }
          public void AddEventListening<T>(string eventName , Action<T> action)
          {
               if (_eventCenter.TryGetValue(eventName, out var e))
               {
                    (e as EventHelp<T>)?.AddCall(action);
               }
               else
               {
                    _eventCenter.Add(eventName , new EventHelp<T>(action));
               }
          }
          public void AddEventListening<T1 , T2>(string eventName , Action<T1 , T2> action)
          {
               if (_eventCenter.TryGetValue(eventName, out var e))
               {
                    (e as EventHelp<T1 , T2>)?.AddCall(action);
               }
               else
               {
                    _eventCenter.Add(eventName , new EventHelp<T1 , T2>(action));
               }
          }

          public void CallEvent(string eventName)
          {
               if (_eventCenter.TryGetValue(eventName, out var e))
               {
                    (e as EventHelp)?.Call();
               }
               else
               {
                    Debug.Log("没有名字的事件" + eventName);
               }
          }
          
          public void CallEvent<T>(string eventName , T value)
          {
               if (_eventCenter.TryGetValue(eventName, out var e))
               {
                    (e as EventHelp<T>)?.Call(value);
               }
               else
               {
                    Debug.Log("没有名字的事件" + eventName);
               }
          }
          
          public void CallEvent<T1 , T2>(string eventName , T1 value1 , T2 value2)
          {
               if (_eventCenter.TryGetValue(eventName, out var e))
               {
                    (e as EventHelp<T1 , T2>)?.Call(value1 , value2);
               }
               else
               {
                    Debug.Log("没有名字的事件" + eventName);
               }
          }

          public void RemoveEvent(string eventName, Action action)
          {
               if (_eventCenter.TryGetValue(eventName, out var e))
               {
                    (e as EventHelp)?.Remove(action);
               }
               else
               {
                    Debug.LogError("无当前名字的事件");
               }
          }
          
          public void RemoveEvent<T>(string eventName, Action<T> action)
          {
               if (_eventCenter.TryGetValue(eventName, out var e))
               {
                    (e as EventHelp<T>)?.Remove(action);
               }
               else
               {
                    Debug.LogError("无当前名字的事件");
               }
          }
          
          public void RemoveEvent<T1 , T2>(string eventName, Action<T1 , T2> action)
          {
               if (_eventCenter.TryGetValue(eventName, out var e))
               {
                    (e as EventHelp<T1 , T2>)?.Remove(action);
               }
               else
               {
                    Debug.LogError("无当前名字的事件");
               }
          }
     }
}
