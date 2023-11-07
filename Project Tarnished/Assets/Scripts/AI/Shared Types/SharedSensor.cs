using BehaviorDesigner.Runtime;
using Micosmo.SensorToolkit;

namespace ProjectTarnished.AI.SharedTypes
{
    [System.Serializable]
    public class SharedSensor : SharedVariable<Sensor>
    {
        public static implicit operator SharedSensor(Sensor value) => new() { Value = value };
    }
}