using Model.Common;

namespace MonoPraksaDay2.Model
{
    public class LastMissionViewModel : ILastMissionViewModel
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