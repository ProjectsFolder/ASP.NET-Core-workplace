namespace Application.Interfaces;

public interface IKafka
{
    void SendAsync(string topic, object message);
}
