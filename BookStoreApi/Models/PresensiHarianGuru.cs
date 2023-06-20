using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using System.ComponentModel.DataAnnotations;

namespace BookStoreApi.Models;

public class PresensiHarianGuru
{
    [BsonId]
    [BsonRepresentation(BsonType.ObjectId)]
    public string? Id { get; set; }

    [Required]
    [MinLength(7)]
    public string NIP { get; set; } = null!;

    [Required]
    public string tgl { get; set; } = null!;

    [Required]
    public bool Kehadiran { get; set; }
}