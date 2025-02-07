using System;

public class RingBuffer<T>
{
    private T[] buffer;
    private int head = 0;
    private int count = 0;
    private int capacity;

    public RingBuffer(int size)
    {
        capacity = size;
        buffer = new T[size];
    }

    public void Add(T item)
    {
        buffer[head] = item;
        head = (head + 1) % capacity;
        if (count < capacity)
            count++;
    }

    public T Get(int index)
    {
        if (index < 0 || index >= count)
            throw new IndexOutOfRangeException("Index out of bounds");
        
        int actualIndex = (head - count + index + capacity) % capacity;
        return buffer[actualIndex];
    }

    public int Count => count;
    public int Capacity => capacity;
}