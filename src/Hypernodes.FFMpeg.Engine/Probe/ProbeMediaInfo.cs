using Newtonsoft.Json;

namespace Hypernodes.FFMpeg.Engine.Probe;

public record ProbeMediaInfo
{
    [JsonProperty("format")]
    public ProbeFormat Format { get; set; }

    [JsonProperty("streams")]
    public IEnumerable<ProbeStream> Streams { get; set; }
}

public record ProbeFormat
{
    [JsonProperty("filename")]
    public string FileName { get; set; }
    
    [JsonProperty("nb_streams")]
    public int NumberOfStreams { get; set; }
    
    [JsonProperty("nb_programs")]
    public int NumberOfPrograms { get; set; }
    
    [JsonProperty("format_name")]
    public string FormatName { get; set; }
    
    [JsonProperty("format_long_name")]
    public string FormatLongName { get; set; }
    
    [JsonProperty("start_time")]
    public double StartTime { get; set; }
    
    [JsonProperty("duration")]
    public double Duration { get; set; }
    
    [JsonProperty("size")]
    public int Size { get; set; }
    
    [JsonProperty("bit_rate")]
    public int Bitrate { get; set; }

    [JsonProperty("probe_score")]
    public int ProbeScore { get; set; }
    
    // TODO: Implement tags.
}

public record ProbeStream
{
    [JsonProperty("index")]
    public int Index { get; set; }
    
    [JsonProperty("codec_name")]
    public string CodecName { get; set; }
    
    [JsonProperty("codec_long_name")]
    public string CodecLongName { get; set; }
    
    [JsonProperty("codec_type")]
    public string CodecType { get; set; }
    
    [JsonProperty("codec_time_base")]
    public string CodecTimeBase { get; set; }
    
    [JsonProperty("codec_tag_string")]
    public string CodecTagString { get; set; }
    
    [JsonProperty("codec_tag")]
    public string CodecTag { get; set; }

    [JsonProperty("sample_fmt")]
    public string SampleFmt { get; set; }

    [JsonProperty("sample_rate")]
    public string SampleRate { get; set; }

    [JsonProperty("width")]
    public int Width { get; set; }
    
    [JsonProperty("height")]
    public int Height { get; set; }
    
    [JsonProperty("has_b_frames")]
    public bool HasBFrames { get; set; }

    [JsonProperty("display_aspect_ratio")]
    public string AspectRatio { get; set; }
    
    [JsonProperty("pix_fmt")]
    public string PixelFormat { get; set; }
    
    [JsonProperty("level")]
    public int Level { get; set; }

    [JsonProperty("field_order")]
    public string FieldOrder { get; set; }
    
    [JsonProperty("is_avc")]
    public bool IsAvc { get; set; }
    
    [JsonProperty("nal_lenght_size")]
    public int NalLenghtSize { get; set; }
    
    [JsonProperty("r_frame_rate")]
    public string RFrameRate { get; set; }
    
    [JsonProperty("avg_frame_rate")]
    public string AvgFrameRate { get; set; }
    
    [JsonProperty("time_base")]
    public string TimeBase { get; set; }
    
    [JsonProperty("start_time")]
    public double StartTime { get; set; }
    
    [JsonProperty("duration")]
    public double Duration { get; set; }
    
    [JsonProperty("bit_rate")]
    public int BitRate { get; set; }
    
    [JsonProperty("nb_frames")]
    public int NumberOfFrames { get; set; }

    [JsonProperty("channels")]
    public int NumberOfChannels { get; set; }

    [JsonProperty("channel_layout")]
    public string ChannelLayout { get; set; }
    
    // TODO: Implement tags.
}