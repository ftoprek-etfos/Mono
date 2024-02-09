using System;

namespace Model.Common
{
    public interface IExperienceViewModel
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public int? Duration { get; set; }
    }
}
