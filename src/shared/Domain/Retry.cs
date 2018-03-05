using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Domain
{
    public static class Retry
    {
        public static void Execute(Action executeAction, TimeSpan retryInterval, Action onFailAction = null, int maxAttemptCount = 3)
        {
            Execute<object>(() =>
                {
                    executeAction();
                    return null;
                },
            retryInterval,
            () =>
                {
                    onFailAction();
                    return null;
                },
            maxAttemptCount);
        }

        public static T Execute<T>(Func<T> action, TimeSpan retryInterval, Func<T> onFailAction = null, int maxAttemptCount = 3)
        {
            var exceptions = new List<Exception>();

            for (int attempted = 0; attempted < maxAttemptCount; attempted++)
            {
                try
                {
                    if (attempted > 0)
                    {
                        Thread.Sleep(retryInterval);
                    }
                    return action();
                }
                catch (Exception ex)
                {
                    exceptions.Add(ex);
                }
            }
            throw new AggregateException(exceptions);
        }
    }
}
