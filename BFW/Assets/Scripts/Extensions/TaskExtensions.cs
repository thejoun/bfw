﻿using System;
using System.Threading.Tasks;

namespace Extensions
{
    public static class TaskExtensions
    {
        public static Task<T> WithCallback<T>(this Task<T> task, Action<T> callback)
        {
            return task.ContinueWith(t =>
            {
                callback(t.Result);
                return t.Result;
            });
        }
    }
}