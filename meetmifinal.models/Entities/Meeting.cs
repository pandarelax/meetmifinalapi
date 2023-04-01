using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace meetmifinal.models.Entities
{
    public class Meeting
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public User? Creator { get; set; }
    }
}
