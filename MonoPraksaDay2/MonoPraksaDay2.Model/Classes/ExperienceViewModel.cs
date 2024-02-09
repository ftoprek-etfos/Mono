using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MonoPraksaDay2.WebAPI.Models
{
    public class ExperienceViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? Duration { get; set; }

        public ExperienceViewModel(Guid id, string title, int? duration)
        {
            Id = id;
            Title = title;
            Duration = duration;
        }
    }
}