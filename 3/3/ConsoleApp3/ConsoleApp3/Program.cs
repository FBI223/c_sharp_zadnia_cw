
using System.Collections;

/*

class Queue : ArrayList
{
    public void Enqueue(Object value)
    {
        this.Add(value);
    }

    public Object Dequeue()
    {
        Object head_obj;
        if (this.Count > 0)
        {
            head_obj = this[0] as Object;
            this.RemoveAt(0);
        }
        else
        {
            throw new InvalidOperationException("Kolejka jest pusta.");
        }
        return head_obj;
    }
}

*/

class Queue
{
    private ArrayList queue = new ArrayList();
    
    public void Enqueue(Object value)
    {
        queue.Add(value);
    }

    public Object Dequeue()
    {
        Object headObj = queue[0];
        queue.RemoveAt(0);
        return headObj;
    }
    
}


/*
 aby zaimplementować kolejkę na podstawie tablicy (stałego rozmiaru),
  trzeba użyć zawijania indeksów przy dodawaniu i usuwaniu elementów.
   W tym celu najlepiej zastosować operację modulo i dwa wskaźniki:
    head (indeks początku kolejki) oraz tail (indeks końca kolejki).
     Taka struktura to klasyczna kolejka cykliczna (circular queue),
      która pozwala efektywnie korzystać z pamięci.
 */


class FunnyQueue
{
    private Object[] queue;     // Tablica na elementy kolejki
    private int head;           // Indeks początku kolejki
    private int tail;           // Indeks końca kolejki
    private int size;           // Rozmiar bieżący kolejki
    private int capacity;       // Maksymalna pojemność kolejki

    public FunnyQueue(int capacity)
    {
        this.capacity = capacity;
        this.queue = new Object[capacity];
        this.head = 0;
        this.tail = 0;
        this.size = 0;
    }

    public void Enqueue(Object value)
    {
        if (size == capacity)
        {
            throw new InvalidOperationException("Kolejka jest pełna.");
        }
        
        queue[tail] = value;
        tail = (tail + 1) % capacity;  // Zawijanie indeksu tail
        size++;
    }

    public Object Dequeue()
    {
        if (size == 0)
        {
            throw new InvalidOperationException("Kolejka jest pusta.");
        }
        
        Object headObj = queue[head];
        head = (head + 1) % capacity;  // Zawijanie indeksu head
        size--;
        return headObj;
    }

    public bool IsEmpty()
    {
        return size == 0;
    }

    public bool IsFull()
    {
        return size == capacity;
    }
}
