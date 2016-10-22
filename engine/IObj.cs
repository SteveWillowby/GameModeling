public interface IObj
{
    string GetType();
    bool In(IObj o);
    bool InType(string t);
    bool Contains(IObj o);
    bool ContainsType(string t);
    void PutIn(IObj o);
    void TakeIn(IObj o);
    void RemoveFrom(IObj o);
    void ThrowOut(IObj o);
}
