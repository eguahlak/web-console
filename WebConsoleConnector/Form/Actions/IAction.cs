using System;
using JsonKnownTypes;
using Newtonsoft.Json;

namespace WebConsoleConnector.Form.Actions
{
    [JsonConverter(typeof(JsonKnownTypesConverter<IAction>))]
    public interface IAction
    {
        Guid Id { get; }
    }

    public abstract class ActionBase : IAction
    {
        public Guid Id { get; set; }

        public ActionBase() { }

        public ActionBase(Guid id)
        {
            Id = id;
        }
    }

}
