using System.ComponentModel.DataAnnotations.Schema;

namespace API.Entities
{
  // as we need Entity to be called as Photos in the database
  // entity framework add AppUserid as a Foreign key in the table because it recognises that there is relationship between appuser and photo Entity 
  [Table("Photos")]
  public class Photo
  {
    public int Id { get; set; }
    public string Url { get; set; }
    public bool IsMainPhoto { get; set; }

    public string PublicId { get; set; }

    // we need to tell about AppUser class to fully define a relationship
    public AppUser AppUser { get; set; }

    public int AppUserId { get; set; }
  }
}