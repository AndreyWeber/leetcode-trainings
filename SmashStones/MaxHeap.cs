public class MaxHeap
{
    private int[] Heap;
    private int MaxSize;
    public int Size { get; private set; }

    public MaxHeap(int maxHeapSize)
    {
        this.MaxSize = maxHeapSize;
        this.Size = 0;
        Heap = new int[this.MaxSize + 1];
        Heap[0] = int.MaxValue;
    }

    private int Parent(int pos) { return pos / 2; }
    private int LeftChild(int pos) { return (2 * pos); }
    private int RightChild(int pos) { return (2 * pos) + 1; }

    private bool IsLeaf(int pos) { return pos > Size / 2 && pos <= Size; }

    private void Swap(int pos1, int pos2)
    {
        int tmp;
        tmp = Heap[pos1];
        Heap[pos1] = Heap[pos2];
        Heap[pos2] = tmp;
    }

    private void MaxHeapify(int pos)
    {
        if (!IsLeaf(pos))
        {
            if (LeftChild(pos) <= Size && Heap[pos] < Heap[LeftChild(pos)] ||
                RightChild(pos) <= Size && Heap[pos] < Heap[RightChild(pos)])
            {
                if (RightChild(pos) <= Size && Heap[LeftChild(pos)] < Heap[RightChild(pos)])
                {
                    Swap(pos, RightChild(pos));
                    MaxHeapify(RightChild(pos));
                }
                else
                {
                    Swap(pos, LeftChild(pos));
                    MaxHeapify(LeftChild(pos));
                }
            }
        }
    }

    public void Insert(int element)
    {
        Heap[++Size] = element;
        int current = Size;

        while (Heap[current] > Heap[Parent(current)])
        {
            Swap(current, Parent(current));
            current = Parent(current);
        }
    }

    public int RemoveMax()
    {
        int popped = Heap[1];
        Heap[1] = Heap[Size--];
        MaxHeapify(1);
        return popped;
    }

    public bool IsEmpty() => Size == 0;
}