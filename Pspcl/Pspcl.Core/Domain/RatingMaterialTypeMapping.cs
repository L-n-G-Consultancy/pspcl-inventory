
using Microsoft.EntityFrameworkCore;

namespace Pspcl.Core.Domain
{
    [Keyless]
    public class RatingMaterialTypeMapping
    {
        public int MaterialTypeId { get; set; }
        public int RatingId { get; set; }
    }
}
