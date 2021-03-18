using System;
using System.Threading.Tasks;
using MoraES.Model;

namespace MoraES
{
    public class MoraEventHandler
    {
        private readonly MoraQueue _moraQueue = new MoraQueue();

        public void Start()
        {

            Task.Run(() =>
            {
                for (; ; )
                {
                    if (_moraQueue.Count != 0)
                    {
                        var time = (ulong)DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                        var firstEvent = _moraQueue.Peek();
                        if (time >= firstEvent.timestamp)
                        {
                            var curr = _moraQueue.Dequeue();
                            MoraLogger.Info($"Firing {curr.timestamp} {curr.contentType}");
                        }
                    }
                }
            });
        }

        public void Schedule(MoraEvent moraEvent)
        {
            _moraQueue.Enqueue(moraEvent, moraEvent.timestamp);
        }
    }
}