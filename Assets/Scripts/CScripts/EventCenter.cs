using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

namespace MessengerCenter {

    //public delegate void CallBack();
   
    //public delegate void CallBack<T>(T arg);
  
    //public delegate void CallBack<T, X>(T arg1,X arg2);
   
    //public delegate void CallBack<T, X, Y>(T arg1,X arg2,Y arg3);
  
    //public delegate void CallBack<T, X, Y, Z>(T arg1,X arg2,Y arg3,Z arg4);
    
    //public delegate void CallBack<T, X, Y, Z, W>(T arg1,X arg2,Y arg3,Z arg4,W arg5);

    public delegate void CallBack(params object[] param);

    public class EventCenter {
        private static Dictionary<int,Delegate> m_EventTable = new Dictionary<int,Delegate>();

        private static void OnListenerAdding(int eventType,Delegate callBack) {
            if (!m_EventTable.ContainsKey(eventType)) {
                m_EventTable.Add(eventType,null);
            }
            Delegate d = m_EventTable[eventType];
            if (d!=null&&d.GetType()!=callBack.GetType()) {
                throw new Exception(string.Format("尝试为事件{0}添加不同类型的委托，当前事件所对应的委托是{1}，要添加的委托类型为{2}",eventType,d.GetType(),callBack.GetType()));
            }
        }
        private static void OnListenerRemoving(int eventType,Delegate callBack) {
            if (m_EventTable.ContainsKey(eventType)) {
                Delegate d = m_EventTable[eventType];
                if (d==null) {
                    throw new Exception(string.Format("移除监听错误：事件{0}没有对应的委托",eventType));
                } else if (d.GetType()!=callBack.GetType()) {
                    throw new Exception(string.Format("移除监听错误：尝试为事件{0}移除不同类型的委托，当前委托类型为{1}，要移除的委托类型为{2}",eventType,d.GetType(),callBack.GetType()));
                }
            } else {
                throw new Exception(string.Format("移除监听错误：没有事件码{0}",eventType));
            }
        }
        private static void OnListenerRemoved(int eventType) {
            if (m_EventTable[eventType]==null) {
                m_EventTable.Remove(eventType);
            }
        }
        //no parameters
        //public static void AddListener(int eventType,CallBack callBack) {
        //    OnListenerAdding(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack)m_EventTable[eventType]+callBack;
        //}
        ////Single parameters
        //public static void AddListener<T>(int eventType,CallBack<T> callBack) {
        //    OnListenerAdding(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack<T>)m_EventTable[eventType]+callBack;
        //}
        ////two parameters
        //public static void AddListener<T, X>(int eventType,CallBack<T,X> callBack) {
        //    OnListenerAdding(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack<T,X>)m_EventTable[eventType]+callBack;
        //}
        ////three parameters
        //public static void AddListener<T, X, Y>(int eventType,CallBack<T,X,Y> callBack) {
        //    OnListenerAdding(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack<T,X,Y>)m_EventTable[eventType]+callBack;
        //}
        ////four parameters
        //public static void AddListener<T, X, Y, Z>(int eventType,CallBack<T,X,Y,Z> callBack) {
        //    OnListenerAdding(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack<T,X,Y,Z>)m_EventTable[eventType]+callBack;
        //}
        ////five parameters
        //public static void AddListener<T, X, Y, Z, W>(int eventType,CallBack<T,X,Y,Z,W> callBack) {
        //    OnListenerAdding(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack<T,X,Y,Z,W>)m_EventTable[eventType]+callBack;
        //}
        public static void AddListener(int eventType,CallBack callBack) {
            OnListenerAdding(eventType,callBack);
            m_EventTable[eventType]=(CallBack)m_EventTable[eventType]+callBack;
        }

        //no parameters
        //public static void RemoveListener(int eventType,CallBack callBack) {
        //    OnListenerRemoving(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack)m_EventTable[eventType]-callBack;
        //    OnListenerRemoved(eventType);
        //}
        ////single parameters
        //public static void RemoveListener<T>(int eventType,CallBack<T> callBack) {
        //    OnListenerRemoving(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack<T>)m_EventTable[eventType]-callBack;
        //    OnListenerRemoved(eventType);
        //}
        ////two parameters
        //public static void RemoveListener<T, X>(int eventType,CallBack<T,X> callBack) {
        //    OnListenerRemoving(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack<T,X>)m_EventTable[eventType]-callBack;
        //    OnListenerRemoved(eventType);
        //}
        ////three parameters
        //public static void RemoveListener<T, X, Y>(int eventType,CallBack<T,X,Y> callBack) {
        //    OnListenerRemoving(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack<T,X,Y>)m_EventTable[eventType]-callBack;
        //    OnListenerRemoved(eventType);
        //}
        ////four parameters
        //public static void RemoveListener<T, X, Y, Z>(int eventType,CallBack<T,X,Y,Z> callBack) {
        //    OnListenerRemoving(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack<T,X,Y,Z>)m_EventTable[eventType]-callBack;
        //    OnListenerRemoved(eventType);
        //}
        ////five parameters
        //public static void RemoveListener<T, X, Y, Z, W>(int eventType,CallBack<T,X,Y,Z,W> callBack) {
        //    OnListenerRemoving(eventType,callBack);
        //    m_EventTable[eventType]=(CallBack<T,X,Y,Z,W>)m_EventTable[eventType]-callBack;
        //    OnListenerRemoved(eventType);
        //}
        public static void RemoveListener(int eventType,CallBack callBack) {
            OnListenerRemoving(eventType,callBack);
            m_EventTable[eventType]=(CallBack)m_EventTable[eventType]-callBack;
            OnListenerRemoved(eventType);
        }

        //no parameters
        //public static void Broadcast(int eventType) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue(eventType,out d)) {
        //        CallBack callBack = d as CallBack;
        //        if (callBack!=null) {
        //            callBack();
        //        } else {
        //            //throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
        //single parameters
        //public static void Broadcast<T>(int eventType,T arg) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue(eventType,out d)) {
        //        CallBack<T> callBack = d as CallBack<T>;
        //        if (callBack!=null) {
        //            callBack(arg);
        //        } else {
        //            throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
        ////two parameters
        //public static void Broadcast<T, X>(int eventType,T arg1,X arg2) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue(eventType,out d)) {
        //        CallBack<T,X> callBack = d as CallBack<T,X>;
        //        if (callBack!=null) {
        //            callBack(arg1,arg2);
        //        } else {
        //            throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
        ////three parameters
        //public static void Broadcast<T, X, Y>(int eventType,T arg1,X arg2,Y arg3) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue(eventType,out d)) {
        //        CallBack<T,X,Y> callBack = d as CallBack<T,X,Y>;
        //        if (callBack!=null) {
        //            callBack(arg1,arg2,arg3);
        //        } else {
        //            throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
        ////four parameters
        //public static void Broadcast<T, X, Y, Z>(int eventType,T arg1,X arg2,Y arg3,Z arg4) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue(eventType,out d)) {
        //        CallBack<T,X,Y,Z> callBack = d as CallBack<T,X,Y,Z>;
        //        if (callBack!=null) {
        //            callBack(arg1,arg2,arg3,arg4);
        //        } else {
        //            throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
        ////five parameters
        //public static void Broadcast<T, X, Y, Z, W>(int eventType,T arg1,X arg2,Y arg3,Z arg4,W arg5) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue(eventType,out d)) {
        //        CallBack<T,X,Y,Z,W> callBack = d as CallBack<T,X,Y,Z,W>;
        //        if (callBack!=null) {
        //            callBack(arg1,arg2,arg3,arg4,arg5);
        //        } else {
        //            throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}

        public static void Broadcast(int eventType,params object[] param) {
            Delegate d;
            if (m_EventTable.TryGetValue(eventType,out d)) {
                CallBack callBack = d as CallBack;
                if (callBack!=null) {
                    callBack(param);
                } else {
                    //throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
                }
            }
        }

        //no parameters
        //public static void AddListener(CMSGID eventType,CallBack callBack) {
        //    OnListenerAdding((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack)m_EventTable[(int)eventType]+callBack;
        //}
        ////Single parameters
        //public static void AddListener<T>(CMSGID eventType,CallBack<T> callBack) {
        //    OnListenerAdding((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack<T>)m_EventTable[(int)eventType]+callBack;
        //}
        ////two parameters
        //public static void AddListener<T, X>(CMSGID eventType,CallBack<T,X> callBack) {
        //    OnListenerAdding((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack<T,X>)m_EventTable[(int)eventType]+callBack;
        //}
        ////three parameters
        //public static void AddListener<T, X, Y>(CMSGID eventType,CallBack<T,X,Y> callBack) {
        //    OnListenerAdding((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack<T,X,Y>)m_EventTable[(int)eventType]+callBack;
        //}
        ////four parameters
        //public static void AddListener<T, X, Y, Z>(CMSGID eventType,CallBack<T,X,Y,Z> callBack) {
        //    OnListenerAdding((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack<T,X,Y,Z>)m_EventTable[(int)eventType]+callBack;
        //}
        ////five parameters
        //public static void AddListener<T, X, Y, Z, W>(CMSGID eventType,CallBack<T,X,Y,Z,W> callBack) {
        //    OnListenerAdding((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack<T,X,Y,Z,W>)m_EventTable[(int)eventType]+callBack;
        //}
        //public static void AddParamsListener(CMSGID eventType,ParamsCallBack callBack) {
        //    OnListenerAdding((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(ParamsCallBack)m_EventTable[(int)eventType]+callBack;
        //}

        //no parameters
        //public static void RemoveListener(CMSGID eventType,CallBack callBack) {
        //    OnListenerRemoving((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack)m_EventTable[(int)eventType]-callBack;
        //    OnListenerRemoved((int)eventType);
        //}
        ////single parameters
        //public static void RemoveListener<T>(CMSGID eventType,CallBack<T> callBack) {
        //    OnListenerRemoving((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack<T>)m_EventTable[(int)eventType]-callBack;
        //    OnListenerRemoved((int)eventType);
        //}
        ////two parameters
        //public static void RemoveListener<T, X>(CMSGID eventType,CallBack<T,X> callBack) {
        //    OnListenerRemoving((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack<T,X>)m_EventTable[(int)eventType]-callBack;
        //    OnListenerRemoved((int)eventType);
        //}
        ////three parameters
        //public static void RemoveListener<T, X, Y>(CMSGID eventType,CallBack<T,X,Y> callBack) {
        //    OnListenerRemoving((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack<T,X,Y>)m_EventTable[(int)eventType]-callBack;
        //    OnListenerRemoved((int)eventType);
        //}
        ////four parameters
        //public static void RemoveListener<T, X, Y, Z>(CMSGID eventType,CallBack<T,X,Y,Z> callBack) {
        //    OnListenerRemoving((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack<T,X,Y,Z>)m_EventTable[(int)eventType]-callBack;
        //    OnListenerRemoved((int)eventType);
        //}
        ////five parameters
        //public static void RemoveListener<T, X, Y, Z, W>(CMSGID eventType,CallBack<T,X,Y,Z,W> callBack) {
        //    OnListenerRemoving((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(CallBack<T,X,Y,Z,W>)m_EventTable[(int)eventType]-callBack;
        //    OnListenerRemoved((int)eventType);
        //}
        //public static void RemoveParamsListener(CMSGID eventType,ParamsCallBack callBack) {
        //    OnListenerRemoving((int)eventType,callBack);
        //    m_EventTable[(int)eventType]=(ParamsCallBack)m_EventTable[(int)eventType]-callBack;
        //    OnListenerRemoved((int)eventType);
        //}

        //no parameters
        //public static void Broadcast(CMSGID eventType) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue((int)eventType,out d)) {
        //        CallBack callBack = d as CallBack;
        //        if (callBack!=null) {
        //            callBack();
        //        } else {
        //            //throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
        //single parameters
        //public static void Broadcast<T>(CMSGID eventType,T arg) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue((int)eventType,out d)) {
        //        CallBack<T> callBack = d as CallBack<T>;
        //        if (callBack!=null) {
        //            callBack(arg);
        //        } else {
        //            throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
        ////two parameters
        //public static void Broadcast<T, X>(CMSGID eventType,T arg1,X arg2) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue((int)eventType,out d)) {
        //        CallBack<T,X> callBack = d as CallBack<T,X>;
        //        if (callBack!=null) {
        //            callBack(arg1,arg2);
        //        } else {
        //            throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
        ////three parameters
        //public static void Broadcast<T, X, Y>(CMSGID eventType,T arg1,X arg2,Y arg3) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue((int)eventType,out d)) {
        //        CallBack<T,X,Y> callBack = d as CallBack<T,X,Y>;
        //        if (callBack!=null) {
        //            callBack(arg1,arg2,arg3);
        //        } else {
        //            throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
        ////four parameters
        //public static void Broadcast<T, X, Y, Z>(CMSGID eventType,T arg1,X arg2,Y arg3,Z arg4) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue((int)eventType,out d)) {
        //        CallBack<T,X,Y,Z> callBack = d as CallBack<T,X,Y,Z>;
        //        if (callBack!=null) {
        //            callBack(arg1,arg2,arg3,arg4);
        //        } else {
        //            throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
        ////five parameters
        //public static void Broadcast<T, X, Y, Z, W>(CMSGID eventType,T arg1,X arg2,Y arg3,Z arg4,W arg5) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue((int)eventType,out d)) {
        //        CallBack<T,X,Y,Z,W> callBack = d as CallBack<T,X,Y,Z,W>;
        //        if (callBack!=null) {
        //            callBack(arg1,arg2,arg3,arg4,arg5);
        //        } else {
        //            throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}

        //public static void BroadcastParams(CMSGID eventType,params object[] param) {
        //    Delegate d;
        //    if (m_EventTable.TryGetValue((int)eventType,out d)) {
        //        ParamsCallBack callBack = d as ParamsCallBack;
        //        if (callBack!=null) {
        //            callBack(param);
        //        } else {
        //            //throw new Exception(string.Format("广播事件错误：事件{0}对应委托具有不同的类型",eventType));
        //        }
        //    }
        //}
    }

}

