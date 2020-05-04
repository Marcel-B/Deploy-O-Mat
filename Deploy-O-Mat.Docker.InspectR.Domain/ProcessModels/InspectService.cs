using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace com.b_velop.Deploy_O_Mat.Docker.InspectR.Domain.ProcessModels
{
    public class Version
    {
        public int Index { get; set; }
    }

    public class Mount
    {
        public string Type { get; set; }
        public string Source { get; set; }
        public string Target { get; set; }
    }

    public class File
    {
        public string Name { get; set; }
        public string UID { get; set; }
        public string GID { get; set; }
        public int Mode { get; set; }
    }
    public class Secret
    {
        public File File { get; set; }
        public string SecretName { get; set; }
        public string SecretID { get; set; }
    }
    public class ContainerSpec
    {
        public string Image { get; set; }
        public IDictionary<string, string> Labels { get; set; }
        public IEnumerable<string> Args { get; set; }
        public IEnumerable<string> Env { get; set; }
        public IEnumerable<Mount> Mounts { get; set; }
        public IDictionary<string, string> Privileges { get; set; }
        public long StopCracePeriod { get; set; }
        // "DNSConfig": {
        // },
        public IEnumerable<Secret> Secrets { get; set; }
        public string Isolation { get; set; }
    }

    public class RestartPolicy
    {
        public string Condition { get; set; }
        public long Delay { get; set; }
        public int MaxAttempts { get; set; }
    }

    public class Platform
    {
        public string Architecture { get; set; }
        public string OS { get; set; }
    }

    public class Placement
    {
        public IEnumerable<Platform> Platforms { get; set; }
        public IEnumerable<string> Constraints { get; set; }
    }

    public class Network
    {
        public string Target { get; set; }
        public IEnumerable<string> Aliases { get; set; }
    }

    public class TaskTemplate
    {
        public ContainerSpec ContainerSpec { get; set; }
        // "Resources": {
        // },
        public RestartPolicy RestartPolicy { get; set; }
        public Placement Placement { get; set; }
        public IEnumerable<Network> Networks { get; set; }
        public int ForceUpdate { get; set; }
        public string Runtime { get; set; }
    }

    public class Replicated
    {
        public int Replicas { get; set; }
    }
    public class Mode
    {
        public Replicated Replicated { get; set; }
    }

    public class Config
    {
        public int Parallelism { get; set; }
        public string FailureAction { get; set; }
        public long Monitor { get; set; }
        public int MaxFailureRatio { get; set; }
        public string Order { get; set; }
    }

    public class Spec
    {
        public string Name { get; set; }
        public IDictionary<string, string> Labels { get; set; }
        public TaskTemplate TaskTemplate { get; set; }
        public Mode Mode { get; set; }
        public Config UpdateConfig { get; set; }
        public Config RollbackConfig { get; set; }
        public EndpointSpec EndpointSpec { get; set; }
    }

    public class VirtualIp
    {
        [JsonPropertyName("NetworkID")]
        public string NetworkId { get; set; }

        [JsonPropertyName("Addr")] 
        public string Address { get; set; }
    }

    public class Port
    {
        public string Protocol { get; set; }
        public int TargetPort { get; set; }
        public int PublishedPort { get; set; }
        public string PublishMode { get; set; }
    }
    public class EndpointSpec
    {
        public string Mode { get; set; }
        public IEnumerable<Port> Ports { get; set; }
    }
    
    public class Endpoint
    {
        [JsonPropertyName("VirtualIPs")] 
        public IEnumerable<VirtualIp> VirtualIps { get; set; }
        public EndpointSpec Spec { get; set; }
        public IEnumerable<Port> Ports { get; set; }
    }


    public class InspectService
    {
        [JsonPropertyName("ID")] 
        public string Id { get; set; }
        public Version Type { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }
        public Spec Spec { get; set; }
        public Endpoint Endpoint { get; set; }
    }
}