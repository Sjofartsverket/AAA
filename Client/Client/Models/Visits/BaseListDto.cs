using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Client.Models.Visits
{
    public class BaseListDto
    {
        public List<BaseDto.LinkDto> Links { get; set; }
        [JsonPropertyName("meta")]
        public MetaDto Meta { get; set; }
    }
}
