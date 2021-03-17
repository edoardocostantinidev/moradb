using System.Collections.Generic;

public class MoraQueue : PriorityQueue<string, ulong>
{
    /// <summary>
    /// Peeks an entire range of timestamps
    /// </summary>
    /// <param name="start"></param>
    /// <param name="end"></param>
    /// <returns></returns>
    public IEnumerable<string> PeekRange(ulong start, ulong end)
    {
        List<string> range = new List<string>();
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
