using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Client.Models.Pilotages
{
    public class BaseListDto
    {
        public List<BaseDto.LinkDto> Links { get; set; }
        [JsonPropertyName("meta")]
        public MetaDto Meta { get; set; }
    }
}