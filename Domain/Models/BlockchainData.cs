using System.Text.Json.Serialization;

namespace Domain.Models;

public class BlockchainData
{
    public required string Name { get; init; }
    public required long Height { get; init; }
    public required string Hash { get; init; }
    public required DateTime Time { get; init; }
    public required string LatestUrl { get; init; }
    public required string PreviousHash { get; init; }
    public required string PreviousUrl { get; init; }
    public required long PeerCount { get; init; }
    public required long UnconfirmedCount { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? HighGasPrice { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? MediumGasPrice { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? LowGasPrice { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? HighPriorityFee { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? MediumPriorityFee { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? LowPriorityFee { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? BaseFee { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? HighFeePerKb { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? MediumFeePerKb { get; init; }
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public long? LowFeePerKb { get; init; }
    public required long LastForkHeight { get; init; }
    public required string LastForkHash { get; init; }

    public override bool Equals(object? obj)
    {
        if (obj == null || GetType() != obj.GetType()) return false;
        var data = (BlockchainData) obj;

        return Name == data.Name &&
               Height == data.Height &&
               Hash == data.Hash &&
               Time == data.Time &&
               LatestUrl == data.LatestUrl &&
               PreviousHash == data.PreviousHash &&
               PreviousUrl == data.PreviousUrl &&
               PeerCount == data.PeerCount &&
               UnconfirmedCount == data.UnconfirmedCount &&
               HighGasPrice == data.HighGasPrice &&
               MediumGasPrice == data.MediumGasPrice &&
               LowGasPrice == data.LowGasPrice &&
               HighPriorityFee == data.HighPriorityFee &&
               MediumPriorityFee == data.MediumPriorityFee &&
               LowPriorityFee == data.LowPriorityFee &&
               BaseFee == data.BaseFee &&
               HighFeePerKb == data.HighFeePerKb &&
               MediumFeePerKb == data.MediumFeePerKb &&
               LowFeePerKb == data.LowFeePerKb &&
               LastForkHeight == data.LastForkHeight &&
               LastForkHash == data.LastForkHash;
    }

    protected bool Equals(BlockchainData other)
    {
        return Name == other.Name && Height == other.Height && Hash == other.Hash && Time.Equals(other.Time) &&
               LatestUrl == other.LatestUrl && PreviousHash == other.PreviousHash && PreviousUrl == other.PreviousUrl
               && PeerCount == other.PeerCount && UnconfirmedCount == other.UnconfirmedCount &&
               HighGasPrice == other.HighGasPrice && MediumGasPrice == other.MediumGasPrice &&
               LowGasPrice == other.LowGasPrice && HighPriorityFee == other.HighPriorityFee &&
               MediumPriorityFee == other.MediumPriorityFee && LowPriorityFee == other.LowPriorityFee &&
               BaseFee == other.BaseFee && HighFeePerKb == other.HighFeePerKb && MediumFeePerKb == other.MediumFeePerKb &&
               LowFeePerKb == other.LowFeePerKb && LastForkHeight == other.LastForkHeight && LastForkHash == other.LastForkHash;
    }

    public override int GetHashCode()
    {
        var hashCode = new HashCode();
        hashCode.Add(Name);
        hashCode.Add(Height);
        hashCode.Add(Hash);
        hashCode.Add(Time);
        hashCode.Add(LatestUrl);
        hashCode.Add(PreviousHash);
        hashCode.Add(PreviousUrl);
        hashCode.Add(PeerCount);
        hashCode.Add(UnconfirmedCount);
        hashCode.Add(HighGasPrice);
        hashCode.Add(MediumGasPrice);
        hashCode.Add(LowGasPrice);
        hashCode.Add(HighPriorityFee);
        hashCode.Add(MediumPriorityFee);
        hashCode.Add(LowPriorityFee);
        hashCode.Add(BaseFee);
        hashCode.Add(LastForkHeight);
        hashCode.Add(LastForkHash);
        return hashCode.ToHashCode();
    }
}