using System.ComponentModel;

namespace NavtorTask.Model;

public class ContainerShip : Ship
{
    public int MaxContainerCapacity { get; set; }
    public double MaxLoadWeight { get; set; }
    public List<Container> Containers { get; set; } = new List<Container>();
    
    private int _lastContainerId = 0;

    public void LoadContainer(Container container)
    {
        double currentWeight = Containers.Sum(c => c.Weight);
        if (Containers.Count < MaxContainerCapacity && currentWeight + container.Weight <= MaxLoadWeight)
        {
            _lastContainerId++;
            container.ContainerId = _lastContainerId;
            Containers.Add(container);
        }
        else
            throw new InvalidOperationException("Cannot load container: Over capacity or weight limit.");
    }
    
    public void UnloadContainer(int containerId)
    {
        var container = Containers.FirstOrDefault(c => c.ContainerId == containerId);
        if (container != null)
        {
            Containers.Remove(container);
        }
        else
        {
            throw new KeyNotFoundException("Container not found.");
        }
    }
}