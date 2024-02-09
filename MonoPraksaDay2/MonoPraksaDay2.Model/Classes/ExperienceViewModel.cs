using System;
using Model.Common;

namespace MonoPraksaDay2.Model
{
    public class ExperienceViewModel : IExperienceViewModel
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