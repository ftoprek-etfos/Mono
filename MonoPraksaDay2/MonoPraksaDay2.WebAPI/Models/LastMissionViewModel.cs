using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoPraksaDay2.WebAPI.Models
{
    public class LastMissionViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }

        public LastMissionViewModel(string name, int duration)
        {
            this.Name = name;
            this.Duration = duration;
        }
    }
}