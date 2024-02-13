using System;

namespace Model.Common
{
    public interface ILastMissionViewModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Duration { get; set; }
    }
}
