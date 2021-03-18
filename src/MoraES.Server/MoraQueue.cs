using System.Collections.Generic;
using MoraES.Model;

namespace MoraES
{
    public class MoraQueue : PriorityQueue<MoraEvent, ulong>
    {
        /// <summary>
        /// Peeks an entire range of timestamps
        /// </summary>
        /// <param name="start"></param>
        /// <param name="end"></param>
        /// <returns></returns>
        public IEnumerable<MoraEvent> PeekRange(ulong start, ulong end)
        {
            List<MoraEvent> range = new List<MoraEvent>();
            var enumerator = this.UnorderedItems.GetEnumerator();
            while (enumerator.MoveNext())
            {
                var curr = enumerator.Current;
                if (start < curr.Priority && curr.Priority < end)
                    range.Add(curr.Element);
            }
            return range.AsReadOnly();
        }

    }
}
