﻿using System;
using System.Threading.Tasks;

namespace NQuandl.Services.TaskQueue
{
    public interface ITaskQueue
    {
        Task Enqueue(Func<Task> taskGenerator);
        Task<T> Enqueue<T>(Func<Task<T>> taskGenerator);
    }
}