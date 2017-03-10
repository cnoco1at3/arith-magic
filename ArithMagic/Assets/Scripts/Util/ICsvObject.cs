
public interface ICsvObject {
    bool IsHeader(string[] text);
    void StartHeader(string[] header);
    void EndHeader();
    void AddElement(string[] element);
}
