using Microsoft.EntityFrameworkCore;

namespace infrastructure_ef.Entities;

[PrimaryKey(nameof(RoomKey), nameof(Floor), nameof(HotelId))]
public class Room
{
    public int RoomKey { get; set; }
    public int Floor { get; set; }
    public int HotelId { get; set; }

    public Guid Guid { get; set; }
}