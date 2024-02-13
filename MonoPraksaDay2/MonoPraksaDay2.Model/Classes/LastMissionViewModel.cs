using Model.Common;
using System;

namespace MonoPraksaDay2.Model
{
    public class LastMissionViewModel : ILastMissionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }

        public LastMissionViewModel(Guid Id, string name, int duration)
        {
            this.Id = Id;
            this.Name = name;
            this.Duration = duration;
        }
    }
}