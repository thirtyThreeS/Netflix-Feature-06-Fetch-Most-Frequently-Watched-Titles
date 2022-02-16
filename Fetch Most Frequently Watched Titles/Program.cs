// Built upon Fetch Most Recently Watched Titles but I am retyping it again for memory purposes

using Fetch_Most_Frequently_Watched_Titles;

class LFUCache
{
    int capacity;
    int size;
    int minFreq;

    // LinkedListNode holds key and value pairs
    private readonly Dictionary<int, LinkedListNode> keyDict;
    private readonly Dictionary<int, MyLinkedList> freqDict;

    public LFUCache(int capacity)
    {
        this.capacity = capacity;
        this.size = 0;
        this.minFreq = 0;
        keyDict = new Dictionary<int, LinkedListNode>(capacity);
        freqDict = new Dictionary<int, MyLinkedList>(capacity);
    }

    LinkedListNode? Get(int key)
    {
        if (!keyDict.ContainsKey(key)) return null;

        LinkedListNode temp = this.keyDict[key];
        this.freqDict[temp.freq].DeleteNode(temp);

        if (this.freqDict[this.keyDict[key].freq] == null)
        {
            this.freqDict.Remove(this.keyDict[key].freq);

            if (this.minFreq == this.keyDict[key].freq)
            {
                this.minFreq += 1;
            }
        }
        this.keyDict[key].freq += 1;
        if (!this.freqDict.ContainsKey(this.keyDict[key].freq))
        {
            this.freqDict[this.keyDict[key].freq] = new MyLinkedList();
        }
        this.freqDict[this.keyDict[key].freq].Append(this.keyDict[key]);
        return this.keyDict[key];
    }

    void Set(int key, int value)
    {
        if (this.Get(key) != null)
        {
            this.keyDict[key].val = value;
            return;
        }

        if (this.size == this.capacity)
        {
            this.keyDict.Remove(this.freqDict[this.minFreq].head.key);
            this.freqDict[this.minFreq].DeleteNode(this.freqDict[this.minFreq].head);

            if (this.freqDict[this.minFreq] == null)
            {
                this.freqDict.Remove(this.minFreq);
            }
            this.size -= 1;
        }
        this.minFreq = 1;
        this.keyDict[key] = new LinkedListNode(key, value, this.minFreq);
        if (!this.freqDict.ContainsKey(this.keyDict[key].freq))
        {
            this.freqDict[this.keyDict[key].freq] = new MyLinkedList();
        }
        this.freqDict[this.keyDict[key].freq].Append(this.keyDict[key]);
        this.size++;
    }

    void Print()
    {
        foreach (var entry in keyDict)
        {
            Console.Write($"({entry.Key}, {entry.Value.val})");
        }
        Console.WriteLine("");
    }

    public static void Main()
    {
        LFUCache cache = new(2);
        Console.WriteLine("The most frequently watched titles are: (key, value)");

        cache.Set(1, 1);
        cache.Set(2, 2);
        cache.Print();

        cache.Get(1);
        cache.Set(3, 3);
        cache.Print();

        cache.Get(2);
        cache.Set(4, 4);
        cache.Get(1);
        cache.Get(3);
        cache.Get(4);
        cache.Print();
    }
}