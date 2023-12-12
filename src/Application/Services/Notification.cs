namespace Application.Services;
using System.Collections.Generic;
using System.Linq;

public sealed class Notification<OutputPort>
{
    private readonly IDictionary<string, IList<string>> _errorMessages = new Dictionary<string, IList<string>>();
    public OutputPort? Output { get; set; }
    public IDictionary<string, string[]> Errors
    {
        get
        {
            var modelState = _errorMessages
                .ToDictionary(item => item.Key, item => item.Value.ToArray());

            return modelState;
        }
    }
    public bool HasErrors => _errorMessages.Count > 0;
    public void SetOutputPort(OutputPort output) => Output = output;
    public void Add(string key, string message)
    {
        if (!_errorMessages.ContainsKey(key))
        {
            _errorMessages[key] = new List<string>();
        }

        _errorMessages[key].Add(message);
    }
}
